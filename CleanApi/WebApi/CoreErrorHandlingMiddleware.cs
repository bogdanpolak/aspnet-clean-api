using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using CleanApi.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanApi.WebApi
{
    public class CoreErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public CoreErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CoreValidationError ex)
            {
                await Handle_ProblemDetailsAsync(context,
                    type: "https://example.com/problems/validation-error",
                    title: "Invalid API parameters was provided",
                    details: $"Invalid parameter: {ex.PropertyName}",
                    errors: new []{ ex.Message }
                    // instance: "/account/12345/messages/abc", more info: https://datatracker.ietf.org/doc/html/rfc7807
                );
            }
            catch (CoreException ex)
            {
                await Handle_ProblemDetailsAsync(context,
                    type: "https://example.com/problems/generic-domain-error",
                    title: "Generic domain error",
                    details: ex.Message,
                    errors: new []{ ex.Message }
                    // instance: "/account/12345/messages/abc", more info: https://datatracker.ietf.org/doc/html/rfc7807
                );
            }
        }

        private static Task Handle_ProblemDetailsAsync(HttpContext context, 
            string type, 
            string title, 
            string details,
            object errors,
            string instance = null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";
            
            var problemDetails = new ProblemDetails
            {
                Type = type,
                Title = title,
                Detail = details,
                Instance = instance
            };
            problemDetails.Extensions.Add("errors",errors);
            
            var msg = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(msg);
        }
    }
}