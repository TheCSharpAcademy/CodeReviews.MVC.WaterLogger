using WaterLogger.DTOs;
using WaterLogger.Models;

namespace WaterLogger.Repositories.Interfaces;

public interface IDailyExpenseRepository
{
    List<DailyExpense> GetAll();
    DailyExpense GetById(int id);
    void Add(DailyExpenseAddDTO record);
    void Update(DailyExpense record);
    void Delete(int id);
}