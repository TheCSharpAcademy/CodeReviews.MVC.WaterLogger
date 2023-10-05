using ExerciseTrackerMVCCarDioLogic.Model;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTrackerMVCCarDioLogic;

public class Context : DbContext
{
    public DbSet<ExerciseSession> Sessions { get; set; }
    public DbSet<ExerciseType> ExerciseTypes { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {

    }
}
