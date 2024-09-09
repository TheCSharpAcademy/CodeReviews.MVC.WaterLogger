namespace HabitTracker.Infrastructure.Queries;

/// <summary>
/// Contains all queries specific to the HabitLog table.
/// </summary>
internal static class HabitLogQueries
{
    #region Constants

    public static readonly string AddHabitLog =
        @"
        INSERT INTO HabitLog
        (
             Id
            ,HabitId
            ,Date
            ,Quantity
        )
        VALUES
        (
             $Id
            ,$HabitId
            ,$Date
            ,$Quantity
        );
        ";

    public static readonly string DeleteHabitLog =
        @"
        DELETE FROM
            HabitLog
        WHERE
            Id = $Id
        ;";

    public static readonly string GetHabitLog =
        @"
        SELECT
            * 
        FROM
            HabitLog
        WHERE
            Id = $Id
        ;";

    public static readonly string GetHabitLogByDate =
        @"
        SELECT
            *
        FROM
            HabitLog
        WHERE
            HabitId = $HabitId
            AND Date = $Date
        ;";

    public static readonly string GetHabitLogs =
        @"
        SELECT
            *
        FROM
            HabitLog
        ;";

    public static readonly string GetHabitLogsByDateRange =
        @"
        SELECT
            *
        FROM
            HabitLog
        WHERE
            Date >= $DateFrom 
            AND Date <= $DateTo
        ;";

    public static readonly string GetHabitLogsByHabitId =
        @"
        SELECT
            *
        FROM
            HabitLog
        WHERE
            HabitId = $HabitId
        ;";

    public static readonly string GetHabitLogsByHabitIdAndDateRange =
        @"
        SELECT
            *
        FROM
            HabitLog
        WHERE
            HabitId = $HabitId
            AND (Date >= $DateFrom AND Date <= $DateTo)
        ;";

    public static readonly string UpdateHabitLog =
        @"
        UPDATE
            HabitLog
        SET
             Date = $Date
            ,Quantity = $Quantity
        WHERE
            Id = $Id
        ;";

    #endregion
}
