using WaterLogger.samggannon.Data;
using WaterLogger.samggannon.Models;

namespace WaterLogger.samggannon.Services
{
    public static class DtoMapper
    {
        public static DrinkingWaterModel MapToModel(DrinkingWaterDto dto)
        {
            return new DrinkingWaterModel
            {
                Id = dto.Id,
                Date = dto.Date,
                Quantity = dto.Quantity,
            };
        }

        internal static DrinkingWaterDto MapToDto(DrinkingWaterModel drinkingWater)
        {
            return new DrinkingWaterDto
            {
                Id = drinkingWater.Id,
                Date = drinkingWater.Date,
                Quantity = drinkingWater.Quantity,
            };
        }
    }
}
