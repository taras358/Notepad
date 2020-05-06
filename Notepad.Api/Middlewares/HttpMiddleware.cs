using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Notepad.Core.Constants;
using Notepad.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace Notepad.Api.Middlewares
{
    public class HttpMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpMiddleware> _logger;

        public HttpMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<HttpMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppCustomException exception)
            {
                await context.Response.WriteAsync(exception.Message);
            }
            catch (UnauthorizeException exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers.Add(ExceptionConstants.InvalidRefresh, "true");
                context.Response.ContentType = @"application/json";
                await context.Response.WriteAsync(exception.Message);
            }
            catch (Exception exception)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }
                _logger.LogCritical($"Message={exception.Message}/StackTrace={exception.StackTrace}");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = @"application/json";
            }
        }
    }
}
