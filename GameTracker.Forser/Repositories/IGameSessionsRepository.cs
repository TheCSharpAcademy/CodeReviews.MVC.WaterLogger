namespace GameTracker.Forser.Repositories
{
    public interface IGameSessionsRepository : IGenericRepository<GameSession>
    {
        Task<List<GameSession>> GetAllGameSessionsAsync();
        Task<GameSession> GetGameSessionAsync(int? id);
        Task DeleteGameSessionAsync(GameSession session);
    }
}