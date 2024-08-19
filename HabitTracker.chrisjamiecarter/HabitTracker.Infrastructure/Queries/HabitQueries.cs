namespace HabitTracker.Infrastructure.Queries;

/// <summary>
/// Contains all queries specific to the Habit table.
/// </summary>
internal static class HabitQueries
{
    #region Constants

    public static readonly string AddHabit =
    @"
        INSERT INTO Habit
        (
             Id
            ,Name
            ,Measure
        )
        VALUES
        (
             $Id
            ,$Name
            ,$Measure
        )
        ;";

    public static readonly string GetHabit =
        @"
        SELECT
            * 
        FROM
            Habit
        WHERE
            Id = $Id
        ;";

    public static readonly string GetHabitByName =
        @"
        SELECT 
            * 
        FROM
            Habit
        WHERE
            Name = $Name
        ;";

    public static readonly string GetHabits =
        @"
        SELECT
            * 
        FROM
            Habit
        ;";

    public static readonly string GetHabitsByIsActive =
        @"
        SELECT 
            * 
        FROM
            Habit
        WHERE
            IsActive = $IsActive
        ;";

    public static readonly string UpdateHabit =
        @"
        UPDATE
            Habit
        SET
             Name = $Name
            ,Measure = $Measure
            ,IsActive = $IsActive
        WHERE
            Id = $Id
        ;";

    #endregion
}
