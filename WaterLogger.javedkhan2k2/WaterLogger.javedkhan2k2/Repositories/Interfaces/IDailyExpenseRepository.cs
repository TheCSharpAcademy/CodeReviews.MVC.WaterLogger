using WaterLogger.DTOs;
using WaterLogger.Models;

namespace WaterLogger.Repositories.Interfaces;

public interface IDailyExpenseRepository
{
    List<DailyExpense> GetAllDailyExpense();
    DailyExpense GetDailyExpenseById(int id);
    void AddDailyExpense(DailyExpenseAddDTO record);
    void UpdateDailyExpense(DailyExpense record);
    void DeleteDailyExpense(int id);
}