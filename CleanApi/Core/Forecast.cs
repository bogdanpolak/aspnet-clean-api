using System;
using System.Collections.Generic;

namespace CleanApi.Core
{
    public class Forecast
    {
        public string Location;
        public IEnumerable<ForecastDetails> Details;
    }

    public class ForecastDetails
    {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
        public string Summary { get; set; }
    }
}