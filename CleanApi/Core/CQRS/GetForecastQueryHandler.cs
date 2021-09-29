using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanApi.Core.Contracts;
using CleanApi.Core.Exceptions;
using CleanApi.Core.Models;
using MediatR;

namespace CleanApi.Core.CQRS
{
    public class GetForecastQueryHandler : IRequestHandler<GetForecastQuery, Forecast>
    {
        private readonly IClimateRepository _climateRepository;
        private readonly IRandomizer _randomizer;
        private readonly INowProvider _nowProvider;

        public GetForecastQueryHandler(
            IClimateRepository climateRepository,
            IRandomizer randomizer,
            INowProvider nowProvider)
        {
            _climateRepository = climateRepository;
            _randomizer = randomizer;
            _nowProvider = nowProvider;
        }
        
        public async Task<Forecast> Handle(GetForecastQuery request, CancellationToken cancellationToken)
        {
            var temperatureRange = await _climateRepository.GetTemperaturesAsync(request.Location, cancellationToken);
            if (temperatureRange is null)
                throw new InvalidLocationError(request.Location);

            var startDate = _nowProvider.GetTodayMidDay();
            
            var details = Enumerable.Range(1, request.Days)
                .Select(index => GenerateForecastForDay(startDate.AddDays(index),temperatureRange))
                .ToArray();
            
            return new Forecast(request.Location, details);
        }

        private ForecastDetails GenerateForecastForDay(DateTime day, TemperatureRange temperatureRange)
        {
            var temperatureAvg = _randomizer.GenInRange(temperatureRange.Low, temperatureRange.High+1);
            return new ForecastDetails
            {
                Date = day,
                TemperatureAvg = temperatureAvg,
                TemperatureDay = Math.Min(temperatureAvg + 5, temperatureRange.High),
                TemperatureNight = Math.Max(temperatureAvg - 5, temperatureRange.Low),
                Summary = Summaries[_randomizer.GenInt(Summaries.Length)]
            };
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    }
}