namespace GameTracker.Forser.Repositories
{
    public class GameInfoRepository : GenericRepository<GameInfo>, IGameInfoRepository
    {
        private readonly GTContext _context;

        public GameInfoRepository(GTContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<GameInfo>> GetAllGamesAsync()
        {
            try
            {
                var games = await _context.GameInfo.ToListAsync();
                return games;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GameInfo> GetGameAsync(int? id)
        {
            try
            {
                var game = await _context.GameInfo.FirstOrDefaultAsync(x => x.Id == id);
                return game;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteGameAsync(GameInfo game)
        {
            try
            {
                GameInfo exists = await _context.GameInfo.FindAsync(game.Id);
                _context.Remove(exists);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GameInfo GetSelectedGame(int? id)
        {
            GameInfo selectedGame = null;

            if (id == null)
            {
                return selectedGame;
            }

            try
            {
                selectedGame = _context.GameInfo
                    .Where(g => g.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return selectedGame;
        }
    }
}