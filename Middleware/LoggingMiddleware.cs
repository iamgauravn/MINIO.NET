public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task  InvokeAsync(HttpContext context
        , ILogger<LoggingMiddleware> _logger)
    {
        context.Request.EnableBuffering();

        // Log the request
        // string requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        // context.Request.Body.Position = 0;
        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} ");

        // Replace the response body
        // var originalResponseBody = context.Response.Body;
        // using var newResponseBody = new MemoryStream();
        // context.Response.Body = newResponseBody;

        // Call the next middleware in the pipeline
        await _next(context);

        // Log the response
        // newResponseBody.Position = 0;
        // string responseBody = await new StreamReader(newResponseBody).ReadToEndAsync();
        // newResponseBody.Position = 0;
        // await newResponseBody.CopyToAsync(originalResponseBody);
        // context.Response.Body = originalResponseBody;
        _logger.LogInformation($"Response: {context.Request.Method} {context.Request.Path} {context.Response.StatusCode} ");
    }
}