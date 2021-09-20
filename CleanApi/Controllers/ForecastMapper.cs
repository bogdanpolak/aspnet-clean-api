using System.Collections.Generic;
using System.Linq;
using CleanApi.Core.Models;

namespace CleanApi.Controllers
{
    public static class ForecastMapper
    {
        public static IEnumerable<WeatherForecastDto> MapToDto(Forecast forecast)
        {
            return forecast.Details.Select(det => new WeatherForecastDto
            {
                Location = forecast.Location,
                Date = det.Date, 
                TemperatureC = det.Temperature, 
                Summary = det.Summary
            });
        }
        
    }
}