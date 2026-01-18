using FluentValidation;
using HabitGains.Application.Entries.Create;
using HabitGains.Application.Entries.Delete;
using HabitGains.Application.Entries.GetByHabitId;
using HabitGains.Application.Entries.GetById;
using HabitGains.Application.Entries.GetForChart;
using HabitGains.Application.Entries.Update;
using HabitGains.Application.Habits.Create;
using HabitGains.Application.Habits.Delete;
using HabitGains.Application.Habits.Get;
using HabitGains.Application.Habits.GetById;
using HabitGains.Application.Habits.Update;
using Microsoft.Extensions.DependencyInjection;

namespace HabitGains.Application.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHabitUseCases();
        services.AddEntryUseCases();
        services.AddFluentValidation();

        return services;
    }

    private static void AddHabitUseCases(this IServiceCollection services)
    {
        services.AddScoped<GetHabitsHandler>();
        services.AddScoped<GetHabitByIdHandler>();
        services.AddScoped<CreateHabitHandler>();
        services.AddScoped<DeleteHabitHandler>();
        services.AddScoped<UpdateHabitHandler>();
    }

    private static void AddEntryUseCases(this IServiceCollection services)
    {
        services.AddScoped<GetEntriesByHabitIdHandler>();
        services.AddScoped<GetEntriesForChartHandler>();
        services.AddScoped<GetEntryByIdHandler>();
        services.AddScoped<UpdateEntryHandler>();
        services.AddScoped<CreateEntryHandler>();
        services.AddScoped<DeleteEntryHandler>();
    }

    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
    }
}
