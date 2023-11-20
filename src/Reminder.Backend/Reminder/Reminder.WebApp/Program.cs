using Reminder.Application;
using Reminder.Persistence;
using Reminder.WebApp;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomConfiguration();
builder.Services.AddApplication().AddPersistence(builder.Environment.EnvironmentName);

var app = builder.Build();

app.MapGet("/", () => "Hello world");

app.Run();