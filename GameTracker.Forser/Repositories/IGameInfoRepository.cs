namespace GameTracker.Forser.Repositories
{
    public interface IGameInfoRepository : IGenericRepository<GameInfo>
    {
        GameInfo GetSelectedGame(int? id);
    }
}