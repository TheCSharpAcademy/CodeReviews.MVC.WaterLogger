namespace GameTracker.Forser.Repositories
{
    public interface IGameSessionsRepository : IGenericRepository<GameSession>
    {
        Task<List<GameSession>> GetAllGameSessionsAsync();
    }
}