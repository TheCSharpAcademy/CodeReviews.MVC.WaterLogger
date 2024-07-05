
using WaterLogger.DTOs;
using WaterLogger.Models;

namespace WaterLogger.Repositories.Interfaces;

public interface IHabitRepository
{
    List<Habit> GetAll();
    Habit GetById(int id);
    void Add(HabitAddDTO record);
    void Update(HabitUpdateDTO record);
    void Delete(int id);
    HabitUpdateDTO GetByIdForUpdate(int id);
}
