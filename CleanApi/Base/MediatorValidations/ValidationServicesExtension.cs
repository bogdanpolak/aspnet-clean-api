using System.Linq;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

/*
 * Startup.cs
 * services.AddValidationPipeline();
 * services.AddMediatR_And_Validations(typeof(..AnyDomainType..).Assembly);
 */

namespace CleanApi.Base.MediatorValidations
{
    public static class ValidationServicesExtension
    {
        public static IServiceCollection AddValidationPipeline(
            this IServiceCollection services)
        {
            return services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        }

        public static IServiceCollection AddMediatR_And_Validations(
            this IServiceCollection services, Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
            assemblies.ToList().ForEach(assembly => services.AddValidatorsFromAssembly(assembly));
            return services;
        }

        public static IServiceCollection AddMediatR_And_Validations(
            this IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}