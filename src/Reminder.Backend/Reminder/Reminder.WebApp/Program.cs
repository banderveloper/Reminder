using Reminder.Application;
using Reminder.Application.Configurations;
using Reminder.Persistence;
using Reminder.WebApp;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomConfiguration();
builder.Services.AddApplication().AddPersistence(builder.Environment.EnvironmentName);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .WithOrigins("null", "http://localhost:5000", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();

var scope = builder.Services.BuildServiceProvider().CreateScope();
var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
DatabaseInitializer.Initialize(applicationDbContext);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = scope.ServiceProvider.GetRequiredService<RedisConfiguration>().ConnectionString;
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/", () => DateTime.Now);
app.MapControllers();

Console.WriteLine("Server started!");
app.Run();