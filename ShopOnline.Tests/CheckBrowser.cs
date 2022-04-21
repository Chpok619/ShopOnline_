using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopBackend.Middlewares;
using Xunit;

namespace ShopOnline.Tests;

public class CheckBrowser
{
    [Fact]
    public async Task Check_browser_is_Edge_with_answer_Yes()
    {
        bool pass = false;

        var useBrowserCheck = new CheckBrowserMiddleware(
            new RequestDelegate(
                context =>
                {
                    pass = true;
                    return Task.CompletedTask;
                })
        );
        
        var httpContext = new DefaultHttpContext();
        var userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.88 Safari/537.36 Edg/99.0.1150.36";
        
        httpContext.Request.Headers.UserAgent = userAgent;
        await useBrowserCheck.InvokeAsync(httpContext);

        Assert.True(pass);
    }
}