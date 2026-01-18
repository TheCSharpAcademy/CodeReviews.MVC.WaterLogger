using Bogus;
using HabitGains.Domain.Entries;

namespace HabitGains.Infrastructure.Database.Seeding.Fakers;

public sealed class HabitEntryFaker : Faker<Entry>
{
    public HabitEntryFaker()
    {
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.Date, f => f.Date.Recent(100));
        RuleFor(x => x.Quantity, f => f.Random.Number(2) + Math.Round(f.Random.Decimal(), 1));
        RuleFor(x => x.CreatedAt, f => DateTime.UtcNow);
        RuleFor(x => x.UpdatedAt, f => null);
    }
}
