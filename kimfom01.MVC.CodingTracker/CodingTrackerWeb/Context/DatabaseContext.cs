using CodingTrackerWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTrackerWeb.Context;

public class DatabaseContext : DbContext
{
    public DbSet<CodingHour> CodingHours { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}