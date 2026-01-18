using HabitGains.Domain.Habits;

namespace HabitGains.Infrastructure.Database.Seeding.Fakers;

public static class HabitFaker
{
    public static List<Habit> Generate()
    {
        return new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Walking",
                Measurement = "Kilometers",
                Favorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Drink water",
                Measurement = "Glasses",
                Favorite = true,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Reading",
                Measurement = "Hours",
                Favorite = false,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Meditation",
                Measurement = "Hours",
                Favorite = false,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Running",
                Measurement = "Kilometers",
                Favorite = false,
                CreatedAt = DateTime.UtcNow.AddDays(-4),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Stretching",
                Measurement = "Hours",
                Favorite = false,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Sleep",
                Measurement = "Hours",
                Favorite = true,
                CreatedAt = DateTime.UtcNow.AddDays(-6),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Coding",
                Measurement = "Hours",
                Favorite = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Cooking",
                Measurement = "Hours",
                Favorite = true,
                CreatedAt = DateTime.UtcNow.AddDays(-8),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Cleaning",
                Measurement = "Hours",
                Favorite = true,
                CreatedAt = DateTime.UtcNow.AddDays(-9),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Journaling",
                Measurement = "Hours",
                Favorite = false,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Yoga",
                Measurement = "Hours",
                Favorite = false,
                CreatedAt = DateTime.UtcNow.AddDays(-11),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Shopping",
                Measurement = "Hours",
                Favorite = true,
                CreatedAt = DateTime.UtcNow.AddDays(-12),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Praying",
                Measurement = "Hours",
                Favorite = false,
                CreatedAt = DateTime.UtcNow.AddDays(-13),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Swimming",
                Measurement = "Hours",
                Favorite = false,
                CreatedAt = DateTime.UtcNow.AddDays(-14),
            },
        };
    }
}
