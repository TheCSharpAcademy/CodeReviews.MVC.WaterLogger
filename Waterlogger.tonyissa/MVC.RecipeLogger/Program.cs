using Microsoft.EntityFrameworkCore;
using MVC.RecipeLogger.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<RecipeContext>(options =>
{
    string dbPath = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new Exception("ConfigurationValueMissingException: Please make sure to add a valid connection string to the config");

    options.UseSqlServer(dbPath);
});

var app = builder.Build();

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