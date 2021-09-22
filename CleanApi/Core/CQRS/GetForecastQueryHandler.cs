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

        public GetForecastQueryHandler(
            IClimateRepository climateRepository)
        {
            _climateRepository = climateRepository;
        }
        
        public async Task<Forecast> Handle(GetForecastQuery request, CancellationToken cancellationToken)
        {
            var temperatureRange = await _climateRepository.GetTemperaturesAsync(request.Location, cancellationToken);
            if (temperatureRange is null)
                throw new InvalidLocationError(request.Location);

            var startDate = DateTime.Now.Date.AddHours(12);
            var details = GenerateForecastDetails(startDate, request.Days, temperatureRange);
            return new Forecast(request.Location, details);
        }

        private static IEnumerable<ForecastDetails> GenerateForecastDetails(DateTime startDate, int days, 
            TemperatureRange temperatureRange)
        {
            var rng = new Random();
            var details = Enumerable.Range(1, days)
                .Select(index => new ForecastDetails(
                    date: startDate.AddDays(index),
                    temperature: rng.Next(temperatureRange.Low, temperatureRange.High),
                    summary: Summaries[rng.Next(Summaries.Length)]
                ))
                .ToArray();
            return details;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    }
}