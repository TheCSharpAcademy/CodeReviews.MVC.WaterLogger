namespace CodingTrackerWeb.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    public void InsertRecord(TEntity codingHour);
    public void DeleteRecord(int id);
    public List<TEntity> GetAllRecords();
    public void UpdateRecord(int id, TEntity codingHour);
    public TEntity GetById(int id);
}
