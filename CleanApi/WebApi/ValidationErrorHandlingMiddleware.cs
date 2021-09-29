using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

/*
 * Add packages:
        dotnet add package MediatR
        dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
        dotnet add package FluentValidation
        dotnet add package FluentValidation.DependencyInjectionExtensions 
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

namespace CleanApi.WebApi 
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
    
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any()) 
                return await next();
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();
            if (failures.Count != 0)
                throw new ValidationException(failures);
            return await next();
        }
    }
}