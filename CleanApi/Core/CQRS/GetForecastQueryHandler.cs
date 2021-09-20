using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanApi.Core.Models;
using MediatR;

namespace CleanApi.Core.CQRS
{
    public class GetForecastQueryHandler : IRequestHandler<GetForecastQuery, Forecast>
    {
        public async Task<Forecast> Handle(GetForecastQuery request, CancellationToken cancellationToken)
        {
            var rng = new Random();
            var details = Enumerable.Range(1, request.Days)
                .Select(index => new ForecastDetails(
                    date: DateTime.Now.AddDays(index),
                    temperature: rng.Next(-20, 55),
                    summary: Summaries[rng.Next(Summaries.Length)]
                ))
                .ToArray();
            await Task.Delay(0,cancellationToken);
            return new Forecast(request.Location, details);
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    }
}