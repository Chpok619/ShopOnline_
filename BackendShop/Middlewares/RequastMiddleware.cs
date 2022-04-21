namespace ShopBackend.Middlewares;

public class RequastMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequastMiddleware> _logger;

    public RequastMiddleware(RequestDelegate next, ILogger<RequastMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();

        if (context.Request.Headers.Count > 0)
            _logger.LogInformation("Request Header: {Header}", context.Request.Headers);
        else
            _logger.LogInformation("Request Header: none");

        if (context.Request.Body.Length > 0)
        {
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            _logger.LogInformation("Request Body: {Body}", body);
            context.Request.Body.Position = 0;
        }
        else
            _logger.LogInformation("Request Body: none");

        await _next(context);
    }
}