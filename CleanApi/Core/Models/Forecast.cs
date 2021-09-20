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
        public DateTime Date { get; }
        public int Temperature { get; }
        public string Summary { get; }

        public ForecastDetails(DateTime date, int temperature, string summary)
        {
            Date = date;
            Temperature = temperature;
            Summary = summary;
        }
    }
}