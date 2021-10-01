using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanApi.Core.Contracts;
using CleanApi.Core.CQRS;
using CleanApi.Core.Exceptions;
using CleanApi.Core.Models;
using FluentAssertions;
using Moq;
using UnitTests.TestDoubles;
using Xunit;

namespace UnitTests.Core
{
    public class GetForecastQueryHandlerTests
    {
        private readonly GetForecastQueryHandler _sut;
        private readonly FakeRandomizer _fakeRandomizer;
        private readonly Mock<IClimateRepository> _climateRepositoryMock;
        private readonly FakeNowProvider _fakeNowProvider;
        
        // ReSharper disable once InconsistentNaming
        private readonly DateTime Date_2021_09_15_2215 = new DateTime(2021,09,15,22,15,00);
        // ReSharper disable once InconsistentNaming
        private readonly DateTime Date_2021_09_16_1200 = new DateTime(2021,09,16,12,00,00);
        
        public GetForecastQueryHandlerTests()
        {
            _climateRepositoryMock = new Mock<IClimateRepository>();
            _fakeRandomizer = new FakeRandomizer();
            _fakeNowProvider = new FakeNowProvider();
            _sut = new GetForecastQueryHandler(_climateRepositoryMock.Object, _fakeRandomizer, _fakeNowProvider);
        }
        
        [Fact] 
        public async Task Handle_Generates_10xDaysForecast()
        {
            var request = new GetForecastQuery("country/city", 10);
            SetupTemperatureRangeForLocation(request.Location);

            var forecast = await _sut.Handle(request,CancellationToken.None);

            forecast.Location.Should().Be(request.Location);
            forecast.Details.Should().HaveCount(10);
        }

        [Fact] 
        public async Task Handler_Generates_3xFollowingDays()
        {
            var request = new GetForecastQuery("country/city", 3);
            SetupTemperatureRangeForLocation(request.Location);
            _fakeNowProvider.Now = Date_2021_09_15_2215;

            var forecast = await _sut.Handle(request,CancellationToken.None);
            var forecastDates = forecast.Details.Select(x => x.Date);
            
            forecastDates.Should().Equal(new[]
            {
                Date_2021_09_16_1200, 
                Date_2021_09_16_1200.AddDays(1), 
                Date_2021_09_16_1200.AddDays(2)
            });
        }

        [Fact] 
        public async Task Handler_Generates_CorrectTemperatures()
        {
            var request = new GetForecastQuery("country/city", 3);
            SetupTemperatureRangeForLocation(request.Location, -6, 6);
            _fakeRandomizer.CurrentStrategy = FakeRandomizer.Strategy.GenAverage;
            _fakeNowProvider.Now = Date_2021_09_15_2215;

            var forecast = await _sut.Handle(request,CancellationToken.None);
            var averageTemperatures = forecast.Details.Select(x => x.TemperatureAvg);
            var nightTemperatures = forecast.Details.Select(x => x.TemperatureNight);
            var dayTemperatures = forecast.Details.Select(x => x.TemperatureDay);

            averageTemperatures.Should().Equal(0, 0, 0);
            nightTemperatures.Should().Equal(-5, -5, -5);
            dayTemperatures.Should().Equal(5, 5, 5);
        }

        [Fact]
        public async Task Handler_Generates_CorrectSummaries()
        {
            var request = new GetForecastQuery("country/city", 2);
            SetupTemperatureRangeForLocation(request.Location);
            _fakeRandomizer.CurrentStrategy = FakeRandomizer.Strategy.GenMinimal;

            var forecast = await _sut.Handle(request, CancellationToken.None);
            var forecastSummaries = forecast.Details.Select(x => x.Summary);

            forecastSummaries.Should().Equal("Freezing", "Freezing");
        }

        [Fact]
        public async Task Handler_InvalidLocation_WillFail()
        {
            var request = new GetForecastQuery("country/city", 3);

            var locationError = await Assert.ThrowsAsync<InvalidLocationError>(
                () => _sut.Handle(request, CancellationToken.None));

            locationError.PropertyName.Should().Be(nameof(request.Location));
            locationError.Message.Should().StartWith("Location must be one of the following:");
        }

        private void SetupTemperatureRangeForLocation(string location, int lowTemperature = -5, int highTemperature = 10)
        {
            _climateRepositoryMock
                .Setup(x => x.GetTemperaturesAsync(location, CancellationToken.None))
                .ReturnsAsync(new TemperatureRange(lowTemperature, highTemperature));
        }

    }
}