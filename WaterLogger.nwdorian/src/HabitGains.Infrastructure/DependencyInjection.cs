using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Domain.Core.Abstractions;
using HabitGains.Infrastructure.Database.ConnectionFactory;
using HabitGains.Infrastructure.Database.Initializer;
using HabitGains.Infrastructure.Database.Repositories;
using HabitGains.Infrastructure.Database.Seeding;
using HabitGains.Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HabitGains.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddServices();

        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IDbSeeder, DbSeeder>();
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString =
            configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Invalid connection string.");

        services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionString));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IHabitRepository, HabitRepository>();
        services.AddScoped<IEntryRepository, EntryRepository>();
    }
}
