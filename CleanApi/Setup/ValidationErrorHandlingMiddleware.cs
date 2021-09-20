using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

/*
 * Startup.cs:
 * app.UseMiddleware<ValidationErrorHandlingMiddleware>();
 */

namespace CleanApi.Setup 
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception.GetType() != typeof(ValidationException))
                return Task.CompletedTask;
            var failures = ((ValidationException)exception).Errors;
            var msg = JsonSerializer.Serialize(
                new {
                    Errors = failures.Select(err => err.ErrorMessage)
                });
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(msg);
        }

    }
}