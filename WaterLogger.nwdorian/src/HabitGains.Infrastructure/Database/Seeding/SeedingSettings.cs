using System.ComponentModel.DataAnnotations;

namespace HabitGains.Infrastructure.Database.Seeding;

public sealed class SeedingSettings
{
    public const string ConfigurationSection = "Seeding";

    public bool SeedOnStartup { get; init; }

    [Range(10, 100, ErrorMessage = "EntriesPerHabit must be between 10 and 100.")]
    public int EntriesPerHabit { get; init; }
}
