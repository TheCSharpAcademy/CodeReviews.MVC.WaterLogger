using HabitTracker.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HabitTracker.Application.Installers;

/// <summary>
/// Installs all dependancies for the application project.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IHabitService, HabitService>();
        services.AddScoped<IHabitLogService, HabitLogService>();

        return services;
    }
}
