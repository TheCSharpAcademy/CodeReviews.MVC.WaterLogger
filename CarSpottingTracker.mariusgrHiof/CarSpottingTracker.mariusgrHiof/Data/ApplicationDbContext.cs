using CarSpottingTracker.mariusgrHiof.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSpottingTracker.mariusgrHiof.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CarSpottedModel> CarSpotters { get; set; }
    }
}
