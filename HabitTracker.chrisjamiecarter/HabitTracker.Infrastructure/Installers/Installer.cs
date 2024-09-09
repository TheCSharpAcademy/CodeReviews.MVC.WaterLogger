using HabitTracker.Application.Repositories;
using HabitTracker.Infrastructure.Contexts;
using HabitTracker.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HabitTracker.Infrastructure.Installers;

/// <summary>
/// Installs all dependancies for the infrastructure project.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // NOTE: In this app, the DbContext constructor:
        // - Reads the ConnectionString from appsettings.
        // - Ensures the database file and schema is created.
        // This can be a singleton object to reduce database calls.
        services.AddSingleton<IDbContext, DbContext>();
        services.AddScoped<IHabitRepository, HabitRepository>();
        services.AddScoped<IHabitLogRepository, HabitLogRepository>();

        return services;
    }
}
