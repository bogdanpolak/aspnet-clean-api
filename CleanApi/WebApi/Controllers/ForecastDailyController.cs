using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanApi.Core.CQRS;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanApi.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ForecastDailyController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ForecastAverageController> _logger;

        public ForecastDailyController(
                IMediator mediator,
                ILogger<ForecastAverageController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ForecastDailyResponse> Get(string location, int days)
        {
            _logger.LogInformation("[{request}] location='{location}' days={days}", 
                "GET ForecastAverage", location, days);
            var forecast = await _mediator.Send(new GetForecastQuery(location, days));
            return ForecastMapper.MapForecastToDailyDto(forecast); 
        }
    }

    public class ForecastDailyResponse
    {
        public string Location { get; init; }
        public IEnumerable<ForecastDailyDetailsDto> Details { get; init; }
    }
    
    public class ForecastDailyDetailsDto
    {
        public string Location { get; init; }
        public DateTime Date { get; init; }
        public int TemperatureDayC { get; init; }
        public int TemperatureNightC { get; init; }
        public int TemperatureDayF => 32 + (int)(TemperatureDayC / 0.5556);
        public int TemperatureNightF => 32 + (int)(TemperatureNightC / 0.5556);
        public string Summary { get; init; }
    }
}