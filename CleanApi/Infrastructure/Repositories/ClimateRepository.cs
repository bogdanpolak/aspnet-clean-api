using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanApi.Core.Contracts;
using CleanApi.Core.Models;

namespace CleanApi.Infrastructure.Repositories
{
    public class ClimateRepository : IClimateRepository
    {
        public async Task<TemperatureRange> GetTemperaturesAsync(string location, CancellationToken cancellationToken = default)
        {
            var climates =
                from climateDto in DatabaseInMemory.LocationClimates
                where climateDto.Location == location
                select new TemperatureRange(climateDto.LowTemperature, climateDto.HighTemperature);
            // simulate real database response delay and async processing
            await Task.Delay(2, cancellationToken);
            return climates.FirstOrDefault();
        }
    }
}
