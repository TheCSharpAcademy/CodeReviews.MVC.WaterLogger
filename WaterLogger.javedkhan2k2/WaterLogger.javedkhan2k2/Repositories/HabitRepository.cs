using WaterLogger.Data;
using WaterLogger.DTOs;
using WaterLogger.Models;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Repositories;

public class HabitRepository : IHabitRepository
{
    private readonly HabitDbContext _context;

    public HabitRepository(HabitDbContext context)
    {
        _context = context;
    }

    public void Add(HabitAddDTO record) => _context.Add(record);

    public void Delete(int id) => _context.Delete(id);

    public void Update(HabitUpdateDTO record) => _context.Update(record);

    public List<Habit> GetAll() => _context.GetAll();

    public Habit GetById(int id) => _context.GetById(id);

    public HabitUpdateDTO GetByIdForUpdate(int id) => _context.GetByIdForUpdate(id);

}