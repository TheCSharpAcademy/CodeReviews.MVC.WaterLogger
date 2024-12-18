using WaterLogger.Dejmenek.Models;

namespace WaterLogger.Dejmenek.Repositories;

public interface IMeasureRepository
{
    List<Measure> GetAllMeasures();
}
