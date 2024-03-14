using Microsoft.EntityFrameworkCore;
using WaterLogger.Models;

namespace WaterLogger.Data;

public class DrinkingWaterContext(DbContextOptions<DrinkingWaterContext> options) : DbContext(options)
{
    public DbSet<DrinkingWater> DrinkingWater { get; set; } = default!;
    public DbSet<DailyCaloriesModel> DailyCalories { get; set; } = default!;
}
