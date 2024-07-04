using WaterLogger.Data;
using WaterLogger.DTOs;
using WaterLogger.Models;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Repositories;

public class HabitUnitRepository : IHabitUnitRepository
{
    private readonly HabitUnitDbContext _context;

    public HabitUnitRepository(HabitUnitDbContext context)
    {
        _context = context;
    }

    public void Add(HabitUnitAddDTO record) => _context.Add(record);

    public void Delete(int id) => _context.Delete(id);

    public void Update(HabitUnit record) => _context.Update(record);

    public List<HabitUnit> GetAll() => _context.GetAll();

    public HabitUnit GetById(int id) => _context.GetById(id);

}