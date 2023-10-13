namespace GameTracker.Forser.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly GTContext _context;
        private readonly DbSet<T> _entities;

        public GenericRepository(GTContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _entities.ToListAsync();
        public async Task Create(T entity) => await _context.AddAsync(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}