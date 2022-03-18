using Microsoft.EntityFrameworkCore;
using ShopBackend;
using ShopBackend.Repositories;

var builder = WebApplication.CreateBuilder(args);

var dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlite($"Data Source={dbPath}")
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors(policy =>
{
    policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("https://localhost:7087")
        .AllowCredentials();
});
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.Run();