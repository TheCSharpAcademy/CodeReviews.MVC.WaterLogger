using RunningLogger.Database;
using RunningLogger.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Database>();
builder.Services.AddSingleton<LogsRepository>();
builder.Services.AddSingleton<UnitsRepository>();
builder.Services.AddRazorPages();

var app = builder.Build();

app.Services.GetRequiredService<Database>().InitDatabase();


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
