using System.Diagnostics;
using WaterLogger.Data;
using WaterLogger.Repositories;
using WaterLogger.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



builder.Services.AddSingleton<DailyExpensesDbContext>(provider => 
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnectionString");
    return new DailyExpensesDbContext(connectionString);
});

builder.Services.AddSingleton<HabitUnitDbContext>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnectionString");
    return new HabitUnitDbContext(connectionString);
});

builder.Services.AddSingleton<HabitDbContext>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnectionString");
    return new HabitDbContext(connectionString);
});

builder.Services.AddSingleton<MyLogDbContext>(providers =>
{
    var configuration = providers.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnectionString");
    return new MyLogDbContext(connectionString);
});

builder.Services.AddScoped<IDailyExpenseRepository, DailyExpenseRepository>();
builder.Services.AddScoped<IHabitUnitRepository, HabitUnitRepository>();
builder.Services.AddScoped<IHabitRepository, HabitRepository>();
builder.Services.AddScoped<IMyLogRepository, MyLogRepository>();

var app = builder.Build();

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
