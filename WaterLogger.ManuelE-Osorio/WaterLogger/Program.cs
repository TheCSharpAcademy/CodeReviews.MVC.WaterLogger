using Microsoft.EntityFrameworkCore;
using WaterLogger.Data;
using WaterLogger.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<DrinkingWaterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DrinkingWaterContext") ?? throw new InvalidOperationException("Connection string 'DrinkingWaterContext' not found.")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
