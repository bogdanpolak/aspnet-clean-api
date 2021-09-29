using CleanApi.Core.Contracts;
using CleanApi.WebApi.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CleanApi.WebApi
{
    public static class WebApiServicesExtension
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            services.AddTransient<IRandomizer, Randomizer>();
            services.AddTransient<INowProvider, NowProvider>();
            return services;
        }
    }
}