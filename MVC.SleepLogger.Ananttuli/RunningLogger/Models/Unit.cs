namespace RunningLogger.Models;

public class Unit
{
    public int UnitId { get; set; }
    public string Name { get; set; } = "";

    public readonly static string[] RUNNING_UNITS = {
            "Kilometers",
            "Meters",
            "Miles",
            "Yards"
        };

    public static decimal StandardiseLogQuantityToUnit(decimal fromQuantity, string fromUnit, string targetUnit)
    {
        return targetUnit switch
        {
            RunningUnit.Kilometers => StandardiseRunningUnitsToKilometers(fromQuantity, fromUnit),
            RunningUnit.Meters => StandardiseRunningUnitsToKilometers(fromQuantity, fromUnit) * 1000m,
            RunningUnit.Miles => StandardiseRunningUnitsToKilometers(fromQuantity, fromUnit) * 0.621371m,
            RunningUnit.Yards => StandardiseRunningUnitsToKilometers(fromQuantity, fromUnit) * 1093.61m,
            _ => fromQuantity,
        };
    }

    public static decimal StandardiseRunningUnitsToKilometers(decimal fromQuantity, string fromUnit)
    {
        return fromUnit switch
        {
            RunningUnit.Kilometers => fromQuantity,
            RunningUnit.Meters => fromQuantity * 0.001m,
            RunningUnit.Miles => fromQuantity * 1.609344m,
            RunningUnit.Yards => fromQuantity * 0.0009144m,
            _ => fromQuantity,
        };
    }
}

public static class RunningUnit
{
    public const string Kilometers = "Kilometers";
    public const string Meters = "Meters";
    public const string Miles = "Miles";
    public const string Yards = "Yards";
}