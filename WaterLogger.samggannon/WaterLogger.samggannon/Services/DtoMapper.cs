﻿using WaterLogger.samggannon.Data;
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
    }
}
