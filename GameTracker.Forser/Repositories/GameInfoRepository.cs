namespace GameTracker.Forser.Repositories
{
    public class GameInfoRepository : GenericRepository<GameInfo>, IGameInfoRepository
    {
        private readonly GTContext _context;

        public GameInfoRepository(GTContext context) : base(context) 
        {
            _context = context;
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