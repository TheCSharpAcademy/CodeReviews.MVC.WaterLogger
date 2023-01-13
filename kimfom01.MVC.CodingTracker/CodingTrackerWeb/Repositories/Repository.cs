using CodingTrackerWeb.Context;
using Microsoft.EntityFrameworkCore;

namespace CodingTrackerWeb.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DatabaseContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public virtual void DeleteRecord(int id)
    {
        TEntity entity = GetById(id);
        _dbSet.Remove(entity);
    }

    public virtual List<TEntity> GetAllRecords()
    {
        return _dbSet.AsNoTracking<TEntity>().ToList();
    }

    public virtual TEntity GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public virtual void InsertRecord(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void UpdateRecord(int id, TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }
}
