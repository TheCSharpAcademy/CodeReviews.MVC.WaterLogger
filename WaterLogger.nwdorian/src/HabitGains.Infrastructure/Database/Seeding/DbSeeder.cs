using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Domain.Entries;
using HabitGains.Domain.Habits;
using HabitGains.Infrastructure.Database.Seeding.Fakers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HabitGains.Infrastructure.Database.Seeding;

public class DbSeeder(
    IHabitRepository habitRepository,
    IEntryRepository entryRepository,
    IOptions<SeedingSettings> settings,
    ILogger<DbSeeder> logger
) : IDbSeeder
{
    public async Task RunAsync()
    {
        logger.LogInformation("Starting database seeding...");

        if (!settings.Value.SeedOnStartup)
        {
            logger.LogInformation("Terminating process. Seeding is disabled in application settings.");
            return;
        }

        if (await habitRepository.Any())
        {
            logger.LogInformation("Terminating process. Data already exists in habits table.");
            return;
        }

        if (await entryRepository.Any())
        {
            logger.LogInformation("Terminating process. Data already exists in entries table.");
            return;
        }

        logger.LogInformation("Generating fake data.");
        List<Habit> habits = HabitFaker.Generate();
        List<Entry> entries = new();

        foreach (Habit habit in habits)
        {
            entries.AddRange(
                new HabitEntryFaker()
                    .FinishWith((_, y) => y.HabitId = habit.Id)
                    .Generate(settings.Value.EntriesPerHabit)
            );
        }

        await habitRepository.BulkInsert(habits);
        logger.LogInformation("Inserted {HabitCount} habits in the database.", habits.Count);

        await entryRepository.BulkInsert(entries);
        logger.LogInformation("Inserted {EntriesCount} entries in the database.", entries.Count);
    }
}
