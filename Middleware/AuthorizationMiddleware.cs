using minio.Interface;

namespace minio.Middleware;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    private readonly ILogger<AuthorizationMiddleware> _logger;

    public AuthorizationMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<AuthorizationMiddleware> logger)
    {
        _next = next;
        _env = env;
        _logger = logger;   
    }

    public async Task InvokeAsync(HttpContext context, ISQLORMService sQLORMService)
    {
        _logger.LogInformation("AuthorizationMiddleware invoked");
        // Try to extract the JWT from the Authorization header
        string authorizationHeader = context.Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            // No JWT found, proceed to the next middleware
            await _next(context);
            _logger.LogInformation("No JWT found");
            return;
        }

        if (!authorizationHeader.Contains(' '))
        {
            // Invalid JWT format, return 401
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid JWT format");
            _logger.LogInformation("Invalid JWT format");
            return;
        }

        // Extract the JWT token
        string token = authorizationHeader.Split(' ')[1];

        await _next(context);
    }
                    
}


