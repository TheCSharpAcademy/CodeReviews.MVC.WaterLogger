namespace GameTracker.Forser.Repositories
{
    public interface IGameInfoRepository : IGenericRepository<GameInfo>
    {
        Task<List<GameInfo>> GetAllGamesAsync();
        Task<GameInfo> GetGameAsync(int? id);
        GameInfo GetSelectedGame(int? id);
        Task DeleteGameAsync(GameInfo game);
    }
}