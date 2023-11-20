using Reminder.Application;
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

var scope = builder.Services.BuildServiceProvider().CreateScope();
var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
DatabaseInitializer.Initialize(applicationDbContext);


var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/", () => "Hello world");

Console.WriteLine("Server started!");
app.Run();