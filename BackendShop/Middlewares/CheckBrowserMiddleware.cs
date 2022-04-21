namespace ShopBackend.Middlewares;

public class CheckBrowserMiddleware
{
    private readonly RequestDelegate _next;

    public CheckBrowserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.UserAgent.ToString().Contains("Edg"))
        {
            return;
        }
        await _next(context);
    }
}