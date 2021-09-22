using System;
using System.Collections.Generic;

namespace CleanApi.Controllers
{
    public class ForecastResponse
    {
        public string Location { get; init; }
        public DateTimeOffset Generated { get; }
        public IEnumerable<WeatherForecastDto> Details { get; init; }

        public ForecastResponse()
        {
            Generated = DateTimeOffset.Now;
        }
    }
}