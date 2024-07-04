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

    public void AddDailyExpense(DailyExpenseAddDTO record) => _context.Add(record);

    public void DeleteDailyExpense(int id) => _context.Delete(id);

    public void UpdateDailyExpense(DailyExpense record) => _context.Update(record);

    public List<DailyExpense> GetAllDailyExpense() => _context.GetAll();

    public DailyExpense GetDailyExpenseById(int id) => _context.GetById(id);

}