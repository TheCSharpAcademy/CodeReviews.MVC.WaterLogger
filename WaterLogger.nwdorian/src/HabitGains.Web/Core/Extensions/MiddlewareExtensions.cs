using HabitGains.Infrastructure.Database.Initializer;
using HabitGains.Infrastructure.Database.Seeding;
using Serilog;

namespace HabitGains.Web.Core.Extensions;

public static class MiddlewareExtensions
{
    public static async Task<IApplicationBuilder> UseWebApplicationMiddleware(this WebApplication app)
    {
        app.UseExceptionHandler();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCustomSerilogRequestLogging();

        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapRazorPages().WithStaticAssets();

        await app.InitializeDatabaseAsync();
        await app.SeedDatabaseAsync();

        return app;
    }

    private static void UseCustomSerilogRequestLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging(opts =>
        {
            opts.GetLevel = (httpContext, elapsed, ex) =>
            {
                PathString path = httpContext.Request.Path;

                if (
                    path.StartsWithSegments("/css")
                    || path.StartsWithSegments("/js")
                    || path.StartsWithSegments("/lib")
                    || path.StartsWithSegments("/favicon.ico")
                )
                {
                    return Serilog.Events.LogEventLevel.Debug;
                }

                return Serilog.Events.LogEventLevel.Information;
            };

            opts.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("CorrelationId", httpContext.TraceIdentifier);
                diagnosticContext.Set("RequestMethod", httpContext.Request.Method);
                diagnosticContext.Set("RequestPath", httpContext.Request.Path);
                diagnosticContext.Set("Endpoint", httpContext.GetEndpoint()?.DisplayName);
            };

            opts.MessageTemplate = "Handled {RequestMethod} {RequestPath} in {Elapsed:0.0000} ms";
        });
    }

    private static async Task InitializeDatabaseAsync(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        IDbInitializer initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

        await initializer.RunAsync();
    }

    private static async Task SeedDatabaseAsync(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        IDbSeeder seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();

        await seeder.RunAsync();
    }
}
