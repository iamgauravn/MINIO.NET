using minio.Model.Response;
using System.Net;

namespace minio.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext
        , ILogger<ExceptionMiddleware> _logger)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex, Guid.NewGuid().ToString());
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception, string trace_id)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new DotNetError()
        {
            status_code = context.Response.StatusCode,
            message = exception.Message,
            trace_id = trace_id,
        }.ToString());
    }
}