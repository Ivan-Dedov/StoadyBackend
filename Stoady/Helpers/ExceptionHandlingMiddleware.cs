using System;
using System.Text.Json;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Stoady.Helpers
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(
            HttpContext context,
            RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext httpContext,
            Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var response = new
            {
                title = GetTitle(exception),
                code = statusCode,
                source = exception.Source,
                message = GetMessage(exception),
                stackTrace = exception.StackTrace,
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static string GetMessage(
            Exception exception)
        {
            if (exception is ValidationException ex)
            {
                return "Validation error(s) occurred:\r\n" + string.Join("\r\n", ex.Message);
            }

            return exception.Message;
        }

        private static string GetTitle(Exception exception)
        {
            return exception switch
            {
                ApplicationException => Startup.ApplicationName,
                _ => "Server Error"
            };
        }

        private static int GetStatusCode(
            Exception exception)
        {
            return exception switch
            {
                ApplicationException or ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
