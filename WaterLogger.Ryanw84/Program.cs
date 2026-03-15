using Microsoft.EntityFrameworkCore;
using WaterLogger.Ryanw84.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Register the DbContext with the SQLite provider.
// "ConnectionString" matches the key in appsettings.json — no change needed there.
builder.Services.AddDbContext<WaterLoggerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ConnectionString"))
);

var app = builder.Build();

// EnsureCreated() replaces your manual CREATE TABLE IF NOT EXISTS SQL.
// It creates the database/tables from your model if they don't already exist.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WaterLoggerContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseRouting();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
