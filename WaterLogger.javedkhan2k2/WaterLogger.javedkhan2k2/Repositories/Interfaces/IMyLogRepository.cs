
using WaterLogger.DTOs;
using WaterLogger.Models;

namespace WaterLogger.Repositories.Interfaces;

public interface IMyLogRepository
{
    List<MyLog> GetAll();
    MyLog GetById(int id);
    void Add(MyLogAddDTO record);
    void Update(MyLogUpdateDTO record);
    void Delete(int id);
    MyLogUpdateDTO GetByIdForUpdate(int id);
}
