using System.Linq;
using CleanApi.Core.Models;

namespace CleanApi.Controllers
{
    public static class ForecastMapper
    {
        public static ForecastResponse MapToDto(Forecast forecast)
        {
            return new ForecastResponse
            {
                Location = forecast.Location,
                Details = forecast.Details.Select(det => new WeatherForecastDto
                {
                    Location = forecast.Location,
                    Date = det.Date,
                    TemperatureC = det.Temperature,
                    Summary = det.Summary
                })
            };
        }
    }
}