using Microsoft.EntityFrameworkCore;
using MVC.RecipeLogger.Models;

namespace MVC.RecipeLogger.Context;

public class RecipeContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>()
            .HasMany(i => i.Ingredients)
            .WithOne(r => r.Recipe)
            .HasForeignKey(r => r.RecipeId)
            .IsRequired();
    }
}