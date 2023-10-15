namespace GameTracker.Forser.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task Create(T entity);
        Task SaveAsync();
        Task DeleteAsync(T entity);
    }
}