using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using CleanApi.Core.Exceptions;
using Microsoft.AspNetCore.Http;

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
            catch (CoreException ex)
            {
                await Handle_CoreExceptionAsync(context, ex);
            }
        }

        private static async Task Handle_CoreExceptionAsync(HttpContext context, CoreException coreException)
        {
            var msg = JsonSerializer.Serialize(
                new {
                    Error = coreException.Message
                });
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(msg);
        }
    }
}