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
    public class ForecastAverageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ForecastAverageController> _logger;

        public ForecastAverageController(
            IMediator mediator,
            ILogger<ForecastAverageController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ForecastAverageResponse> Get(string location, int days)
        {
            _logger.LogInformation("[{request}] location='{location}' days={days}", 
                "GET ForecastAverage", location, days);
            var forecast = await _mediator.Send(new GetForecastQuery(location, days));
            return ForecastMapper.MapForecastToAverageDto(forecast); 
        }
    }
    
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
