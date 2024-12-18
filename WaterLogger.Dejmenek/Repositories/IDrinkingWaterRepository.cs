using WaterLogger.Dejmenek.Models;

namespace WaterLogger.Dejmenek.Repositories;

public interface IDrinkingWaterRepository
{
    DrinkingWater GetById(int id);
    void Create(DrinkingWater drinkingWater);
    void Update(DrinkingWater drinkingWater);
    void Delete(int id);
    List<DrinkingWater> GetAllRecords();
}
