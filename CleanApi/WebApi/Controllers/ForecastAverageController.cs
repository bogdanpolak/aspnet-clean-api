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
                "GET WeatherForecast", location, days);
            var forecast = await _mediator.Send(new GetForecastQuery(location, days));
            return ForecastMapper.MapForecastToAverageDto(forecast); 
        }
    }
}
