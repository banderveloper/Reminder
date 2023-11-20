var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var ts = new TimeSpan(1, 0, 0, 0);

app.MapGet("/", () => "Hello World!");

app.Run();