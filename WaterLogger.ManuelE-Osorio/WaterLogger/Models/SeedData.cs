using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WaterLogger.Data;
using System;
using System.Linq;

namespace WaterLogger.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DrinkingWaterContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<DrinkingWaterContext>>()))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (!context.DrinkingWater.Any())
            {
                AddDrinkingWater(context);   // DB has been seeded
            }
            
            if (!context.DailyCalories.Any())
            {
                AddDailyCalories(context);
            }

            return;

        }
    }

    private static void AddDailyCalories(DrinkingWaterContext context)
    {
        context.DailyCalories.AddRange(
            new DailyCaloriesModel
            {
                Date = new DateTime(2024, 03, 14, 0, 0, 0, 0),
                Quantity = 1700
            },
            new DailyCaloriesModel
            {
                Date = new DateTime(2024, 03, 13, 0, 0, 0, 0),
                Quantity = 1600
            },
            new DailyCaloriesModel
            {
                Date = new DateTime(2024, 03, 12, 0, 0, 0, 0),
                Quantity = 1800
            },
            new DailyCaloriesModel
            {
                Date = new DateTime(2024, 03, 11, 0, 0, 0, 0),
                Quantity = 1650
            }
        );
        context.SaveChanges();
    }
    

    public static void AddDrinkingWater(DrinkingWaterContext context)
    {
        context.DrinkingWater.AddRange(
            new DrinkingWater
            {
                Date = new DateTime(2024, 03, 14, 0, 0, 0, 0),
                Quantity = 15
            },
            new DrinkingWater
            {
                Date = new DateTime(2024, 03, 13, 0, 0, 0, 0),
                Quantity = 11
            },
            new DrinkingWater
            {
                Date = new DateTime(2024, 03, 12, 0, 0, 0, 0),
                Quantity = 9
            },
            new DrinkingWater
            {
                Date = new DateTime(2024, 03, 11, 0, 0, 0, 0),
                Quantity = 12
            }
        );
        context.SaveChanges();
    }
}
