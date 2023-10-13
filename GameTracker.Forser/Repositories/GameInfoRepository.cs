namespace GameTracker.Forser.Repositories
{
    public class GameInfoRepository : GenericRepository<GameInfo>, IGameInfoRepository
    {
        private readonly GTContext _context;

        public GameInfoRepository(GTContext context) : base(context) 
        {
            _context = context;
        }
    }
}