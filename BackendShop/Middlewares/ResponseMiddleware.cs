namespace ShopBackend.Middlewares;

public class ResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ResponseMiddleware> _logger;

    public ResponseMiddleware(RequestDelegate next, ILogger<ResponseMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Stream originalBody = context.Response.Body;

        using var memoryStream = new MemoryStream(); 
        context.Response.Body = memoryStream;

        await _next(context);

        if (context.Response.Headers.Count > 0)
            _logger.LogInformation("Response Header: {Header}", context.Response.Headers);
        else
            _logger.LogInformation("Response Header: none");

        if (memoryStream.Length > 0)
        {
            memoryStream.Position = 0;

            var body = await new StreamReader(memoryStream).ReadToEndAsync();
            _logger.LogInformation("Response Body: {Body}", body);

            memoryStream.Position = 0;

            await memoryStream.CopyToAsync(originalBody);
        }
        else
            _logger.LogInformation("Response Body: none");
        context.Response.Body = originalBody;
    }
}