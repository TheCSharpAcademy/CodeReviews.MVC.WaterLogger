using Microsoft.EntityFrameworkCore;
using WaterLogger.Ryanw84.Models;

namespace WaterLogger.Ryanw84.Data
{
    // DbContext is the EF Core equivalent of manually opening a SqliteConnection.
    // It manages the connection, translates LINQ to SQL, and tracks changes.
    public class WaterLoggerContext(DbContextOptions<WaterLoggerContext> options) : DbContext(options)
    {
        // DbSet<T> maps to a table. You query it with LINQ instead of writing SQL strings.
        public DbSet<DrinkingWaterModel> DrinkingWater { get; set; }
    }
}
