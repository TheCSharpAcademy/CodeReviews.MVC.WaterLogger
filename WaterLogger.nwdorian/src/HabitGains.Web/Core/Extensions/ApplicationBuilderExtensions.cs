using System.Globalization;
using Serilog;
using Serilog.Formatting.Json;

namespace HabitGains.Web.Core.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog(
            (context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);

                config
                    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
                    .WriteTo.File(
                        new JsonFormatter(renderMessage: true),
                        "../../logs/log-.txt",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        shared: true
                    );

                config.Enrich.FromLogContext();
            }
        );

        return builder;
    }
}
