using System;
using System.Collections.Generic;

namespace CleanApi.WebApi.Controllers
{
    public class ForecastAverageResponse
    {
        public string Location { get; init; }
        public DateTimeOffset Generated { get; }
        public IEnumerable<ForecastAverageDetailsDto> Details { get; init; }

        public ForecastAverageResponse()
        {
            Generated = DateTimeOffset.Now;
        }
    }
    
    public class ForecastAverageDetailsDto
    {
        public string Location { get; set; }
        
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }

}