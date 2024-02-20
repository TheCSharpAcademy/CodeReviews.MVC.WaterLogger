using Microsoft.EntityFrameworkCore;
namespace ReadToKidsTracker.Models;

public class ReadContext : DbContext
{
    public ReadContext(DbContextOptions<ReadContext> options) : base(options) { }
    public DbSet<ReadSession> ReadSessions { get; set; }
}
