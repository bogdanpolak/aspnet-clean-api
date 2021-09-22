using CleanApi.Core.Contracts;
using CleanApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanApi.Infrastructure
{
    public static class InfrastructureServicesExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services.AddScoped<IClimateRepository, ClimateRepository>();
        }
    }
}
