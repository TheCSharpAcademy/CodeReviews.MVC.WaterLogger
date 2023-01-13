using CodingTrackerWeb.Context;
using CodingTrackerWeb.Helper;
using CodingTrackerWeb.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DatabaseContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("LocalPostgreSQL"));
    });
}
else
{
    builder.Services.AddDbContext<DatabaseContext>(options =>
    {
        options.UseNpgsql(ExternalDbConnectionHelper.GetConnectionString());
    });
}

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICodingHourRepository, CodingHourRepository>();

var app = builder.Build();

// Apply pending migrations on database
var scope = app.Services.CreateScope();
await DataHelper.ManageDataAsync(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
