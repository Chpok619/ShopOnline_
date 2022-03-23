using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorShop;
using HttpApiClient;
using Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

builder.Services.AddSingleton(_ => new ApiClient("https://localhost:7088", new HttpClient()));
builder.Services.AddSingleton<CountProducts>();
builder.Services.AddSingleton<ICartForShop, CartForShop>();

await builder.Build().RunAsync();