using HabitGains.Application.Core;
using HabitGains.Infrastructure;
using HabitGains.Infrastructure.Database.Seeding;
using HabitGains.Web.Core.Infrastructure;

namespace HabitGains.Web.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddRazorPages();

        services.AddInfrastructure(configuration);
        services.AddApplication();

        services.AddCustomExceptionHandler();
        services.AddOptions();

        return services;
    }

    private static void AddCustomExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails(configure =>
        {
            configure.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
            };
        });
    }

    private static void AddOptions(this IServiceCollection services)
    {
        services
            .AddOptions<SeedingSettings>()
            .BindConfiguration(SeedingSettings.ConfigurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
