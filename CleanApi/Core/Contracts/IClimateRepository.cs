using System.Threading;
using System.Threading.Tasks;
using CleanApi.Core.Models;

namespace CleanApi.Core.Contracts
{
    public interface IClimateRepository
    {
        Task<TemperatureRange> GetTemperaturesAsync(string location, CancellationToken cancellationToken = default);
    }
}