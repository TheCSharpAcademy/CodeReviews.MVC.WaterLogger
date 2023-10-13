namespace GameTracker.Forser.Data
{
    public class GTContext : DbContext
    {
        public DbSet<GameSession>? Sessions { get; set; }
        public DbSet<GameInfo>? GameInfo { get; set; }
        public GTContext(DbContextOptions<GTContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameSession>()
                .HasData(
                    new GameSession { Id = 1, SessionStart = DateTime.Now, SessionEnd = DateTime.Now.AddHours(2.0), GameId = 3 },
                    new GameSession { Id = 2, SessionStart = DateTime.Now.AddDays(-1), SessionEnd = DateTime.Now.AddHours(2.0).AddHours(-1), GameId = 2 },
                    new GameSession { Id = 3, SessionStart = DateTime.Now.AddHours(4.0), SessionEnd = DateTime.Now.AddHours(8.0), GameId = 1 }
                );

            modelBuilder.Entity<GameInfo>()
                .HasData(
                    new GameInfo { Id = 1, GameTitle = "Grand Theft Auto V", GameDescription = "Grand Theft Auto V for PC offers players the option to explore the award-winning world of Los Santos and Blaine County in resolutions of up to 4k and beyond, as well as the chance to experience the game running at 60 frames per second." },
                    new GameInfo { Id = 2, GameTitle = "CyberPunk 2077", GameDescription = "Cyberpunk 2077 is an open-world, action-adventure RPG set in the dark future of Night City — a dangerous megalopolis obsessed with power, glamor, and ceaseless body modification." },
                    new GameInfo { Id = 3, GameTitle = "Honkai: Star Rail", GameDescription = "Honkai: Star Rail is a new HoYoverse space fantasy RPG. Hop aboard the Astral Express and experience the galaxy's infinite wonders on this journey filled with adventure and thrills." }
                );
        }
    }
}