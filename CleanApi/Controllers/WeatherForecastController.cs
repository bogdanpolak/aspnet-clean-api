﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            IMediator mediator,
            ILogger<WeatherForecastController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecastDto>> Get(string location, int days)
        {
            _logger.LogInformation("[GET] WeatherForecast location='{location}' days={days}", 
                location, days);
            var forecast = await _mediator.Send(new GetForecastQuery.Request(location, days));
            return MapForecastToDto(forecast); 
        }

        private static IEnumerable<WeatherForecastDto> MapForecastToDto(Forecast forecast)
        {
            return forecast.Details.Select(det => new WeatherForecastDto
            {
                Date = det.Date, TemperatureC = det.Temperature, Summary = det.Summary
            });
        }
    }
}