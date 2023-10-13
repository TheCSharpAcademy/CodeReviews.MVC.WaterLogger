namespace GameTracker.Forser.Repositories
{
    public class GameSessionRepository : GenericRepository<GameSession>, IGameSessionsRepository
    {
        private readonly GTContext _context;

        public GameSessionRepository(GTContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<GameSession>> GetAllGameSessionsAsync()
        {
            try
            {
                var gameSessions = await _context.Sessions.Include(sg => sg.Game).ToListAsync();

                return gameSessions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}