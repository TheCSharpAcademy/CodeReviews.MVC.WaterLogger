using System;

namespace WaterLogger.DTOs;


public class DailyExpenseAddDTO
{
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string ExpenseType { get; set; }
    
}
