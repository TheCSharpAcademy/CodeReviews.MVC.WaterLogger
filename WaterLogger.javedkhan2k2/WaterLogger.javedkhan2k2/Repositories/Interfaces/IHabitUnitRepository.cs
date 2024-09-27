
using WaterLogger.DTOs;
using WaterLogger.Models;

namespace WaterLogger.Repositories.Interfaces;

public interface IHabitUnitRepository
{
    List<HabitUnit> GetAll();
    HabitUnit GetById(int id);
    void Add(HabitUnitAddDTO record);
    void Update(HabitUnit record);
    void Delete(int id);
}
