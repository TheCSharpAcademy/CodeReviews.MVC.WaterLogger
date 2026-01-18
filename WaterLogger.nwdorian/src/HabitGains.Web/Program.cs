using HabitGains.Web.Core.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();
builder.Services.AddWebApplicationServices(builder.Configuration);

WebApplication app = builder.Build();

await app.UseWebApplicationMiddleware();

await app.RunAsync();
