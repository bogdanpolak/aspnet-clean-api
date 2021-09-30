using System.Linq;
using AutoFixture.Xunit2;
using CleanApi.Core.CQRS;
using FluentAssertions;
using Xunit;

namespace UnitTests.Core
{
    public class GetForecastQueryValidatorTests
    {
        private readonly GetForecastQueryValidator _sut;

        public GetForecastQueryValidatorTests()
        {
            _sut = new GetForecastQueryValidator();
        }
        
        [Fact]
        public void Validate_RequestWithNulls()
        {
            var request = new GetForecastQuery(null, null);
            
            var validationResult = _sut.Validate(request);
            var validationMessages = validationResult.Errors.Select(x => x.ErrorMessage);

            validationMessages.Should().Equal(
                "'Location' must not be empty.", 
                "'Days' must not be empty.");
        }

        [Theory]
        [AutoData]
        public void Validate_InvalidLocation(string location)
        {
            var request = new GetForecastQuery(location, 1);
            
            var validationResult = _sut.Validate(request);
            var validationMessages = validationResult.Errors.Select(x => x.ErrorMessage);

            validationMessages.Should().Equal("Location has incorrect format, expected: country/city");
        }
        
        [Theory]
        [InlineData(-99,false)]
        [InlineData(-1,false)]
        [InlineData(0,false)]
        [InlineData(1,true)]
        [InlineData(14,true)]
        [InlineData(15,false)]
        [InlineData(999,false)]
        public void Validate_DaysOutOfRange(int days, bool expectedIsValid)
        {
            var request = new GetForecastQuery("country/city", days);
            
            var validationResult = _sut.Validate(request);

            validationResult.IsValid.Should().Be(expectedIsValid);
        }

        [Fact]
        public void Validate_CorrectRequest()
        {
            var request = new GetForecastQuery("country/city", 12);
            
            var validationResult = _sut.Validate(request);

            validationResult.IsValid.Should().BeTrue();
        }
    }
}