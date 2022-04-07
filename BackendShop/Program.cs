using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopBackend;
using ShopBackend.Repositories;
using ShopBackend.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

var dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlite($"Data Source={dbPath}")
);

JWTKey jwtKey = builder.Configuration.GetSection("JwtConfig").Get<JWTKey>();
builder.Services.AddSingleton(jwtKey);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkEF>();
builder.Services.AddSingleton<GenerateTokenService>();
builder.Services.AddSingleton<IPasswordHasher<AccountDTO>, PasswordHasher<AccountDTO>>();

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(jwtKey.SigningKeyBytes),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,

            ValidateIssuer = true,
            ValidIssuer = jwtKey.Issuer
        };
    });
builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.Run();