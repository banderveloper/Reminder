using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Reminder.Application;
using Reminder.Application.Configurations;
using Reminder.Application.Converters;
using Reminder.Application.Interfaces.Providers;
using Reminder.Persistence;
using Reminder.WebApp;

var builder = WebApplication.CreateBuilder(args);

// DI method with injecting custom configuration classes
builder.AddCustomConfiguration();

// Injecting other layers
builder.Services.AddApplication().AddPersistence(builder.Environment.EnvironmentName);

var scope = builder.Services.BuildServiceProvider().CreateScope();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("null", "http://localhost:5000", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // ErrorCode enum int value to snake_case_string in response (ex: not 1, but username_already_exists)
    options.JsonSerializerOptions.Converters.Add(new SnakeCaseStringEnumConverter<ErrorCode>());
});

// Add bearer auth schema
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters =
            scope.ServiceProvider.GetRequiredService<IJwtProvider>().ValidationParameters;
    });

// Initialize database if it is not exists
var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
DatabaseInitializer.Initialize(applicationDbContext);

// Inject redis with IDistributesCache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = scope.ServiceProvider.GetRequiredService<RedisConfiguration>().ConnectionString;
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => DateTime.Now);
app.MapControllers();

Console.WriteLine("Server started!");
app.Run();