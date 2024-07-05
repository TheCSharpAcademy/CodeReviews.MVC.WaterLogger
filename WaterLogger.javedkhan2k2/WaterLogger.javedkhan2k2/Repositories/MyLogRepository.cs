using WaterLogger.Data;
using WaterLogger.DTOs;
using WaterLogger.Models;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Repositories;

public class MyLogRepository : IMyLogRepository
{
    private readonly MyLogDbContext _context;

    public MyLogRepository(MyLogDbContext context)
    {
        _context = context;
    }

    public void Add(MyLogAddDTO record) => _context.Add(record);

    public void Delete(int id) => _context.Delete(id);

    public void Update(MyLogUpdateDTO record) => _context.Update(record);

    public List<MyLog> GetAll() => _context.GetAll();

    public MyLog GetById(int id) => _context.GetById(id);

    public MyLogUpdateDTO GetByIdForUpdate(int id) => _context.GetByIdForUpdate(id);

}