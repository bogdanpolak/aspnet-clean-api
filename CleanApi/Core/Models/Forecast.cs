using System;
using System.Collections.Generic;

namespace CleanApi.Core.Models
{
    public class Forecast
    {
        public string Location { get; }
        public IEnumerable<ForecastDetails> Details { get; }

        public Forecast(string location, IEnumerable<ForecastDetails> details)
        {
            Location = location;
            Details = details;
        }
    }

    public class ForecastDetails
    {
        public DateTime Date { get; init;  }
        public int TemperatureAvg { get; init;  }
        public int TemperatureDay { get; init;  }
        public int TemperatureNight { get; init;  }
        public string Summary { get; init; }
    }
}