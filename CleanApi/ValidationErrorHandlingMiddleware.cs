using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

/*
 * Add packages:
        dotnet add package MediatR
        dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
        dotnet add package FluentValidation
        dotnet add package FluentValidation.DependencyInjectionExtensions 
 * Copy:
     - ValidationPipelineBehavior.cs
 * Startup.cs:
     - Register:
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            var assembly = typeof(Startup).Assembly;
            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);
     - Configure :
            app.UseMiddleware<ValidationErrorHandlingMiddleware>();
 * Add Mediator request, handler (rename Forecast to your domain subject)
        public class GetForecastQuery : IRequest<Forecast> { ... }
        public class GetForecastQueryHandler : IRequestHandler<GetForecastQuery, Forecast> { ... }
 * Add Fluent Validator
        public class GetForecastQueryValidator : AbstractValidator<GetForecastQuery> { ... }
 */

namespace CleanApi 
{
    public class ValidationErrorHandlingMiddleware 
    {
                private readonly RequestDelegate _next;

        public ValidationErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            var msg = JsonSerializer.Serialize(
                new {
                    Errors = exception.Errors.Select(err => err.ErrorMessage)
                });
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(msg);
        }
    }
}