using Microsoft.EntityFrameworkCore;
using MVC.RecipeLogger.Context;
using MVC.RecipeLogger.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<RecipeContext>(options =>
{
    string dbPath = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new Exception("ConfigurationValueMissingException: Please make sure to add a valid connection string to the config");

    options.UseSqlServer(dbPath);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RecipeContext>();
    context.Database.EnsureCreated();
    SeedData.Initialize(context);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();