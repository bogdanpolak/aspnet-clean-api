using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace CleanApi.Core
{
    public class GetForecastQuery
    {
        public class Request : IRequest<Forecast>
        {
            public string Location;
            public int Days;

            public Request(string location, int days)
            {
                Location = location;
                Days = days;
            }
        }
        
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Location)
                    .NotEmpty()
                    .Matches(@"\w+\/\w+")
                    .WithMessage("{PropertyName} has incorrect format, expected: country/city");
                RuleFor(x => x.Days)
                    .InclusiveBetween(1, 14);
            }
        }
        
        public class Handler : IRequestHandler<Request, Forecast>
        {
            public async Task<Forecast> Handle(Request request, CancellationToken cancellationToken)
            {
                var rng = new Random();
                await Task.Delay(0,cancellationToken);
                return new Forecast
                {
                    Location = request.Location,
                    Details = Enumerable.Range(1, request.Days).Select(index => new ForecastDetails
                    {
                        Date = DateTime.Now.AddDays(index),
                        Temperature = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    })
                };
            }

            private static readonly string[] Summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
        }

    }
}