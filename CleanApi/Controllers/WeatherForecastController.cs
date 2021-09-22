using System.Threading.Tasks;
using CleanApi.Core.CQRS;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
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
        public async Task<ForecastResponse> Get(string location, int days)
        {
            _logger.LogInformation("[{request}] location='{location}' days={days}", 
                "GET WeatherForecast", location, days);
            var forecast = await _mediator.Send(new GetForecastQuery(location, days));
            return ForecastMapper.MapToDto(forecast); 
        }
    }
}
