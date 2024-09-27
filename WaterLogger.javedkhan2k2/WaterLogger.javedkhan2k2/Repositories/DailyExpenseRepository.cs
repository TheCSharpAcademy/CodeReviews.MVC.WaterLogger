using WaterLogger.Data;
using WaterLogger.DTOs;
using WaterLogger.Models;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Repositories;

public class DailyExpenseRepository : IDailyExpenseRepository
{
    private readonly DailyExpensesDbContext _context;

    public DailyExpenseRepository(DailyExpensesDbContext context)
    {
        _context = context;
    }

    public void Add(DailyExpenseAddDTO record) => _context.Add(record);

    public void Delete(int id) => _context.Delete(id);

    public void Update(DailyExpense record) => _context.Update(record);

    public List<DailyExpense> GetAll() => _context.GetAll();

    public DailyExpense GetById(int id) => _context.GetById(id);

}