namespace HabitTracker.Infrastructure.Queries;

/// <summary>
/// Contains all queries specific to table definitions.
/// </summary>
internal static class TableQueries
{
    #region Constants

    public static readonly string CreateHabitTable =
        @"
        CREATE TABLE IF NOT EXISTS Habit
        (
             Id TEXT PRIMARY KEY
            ,Name TEXT NOT NULL UNIQUE
            ,Measure TEXT NOT NULL
            ,IsActive INTEGER DEFAULT 1
        );
        ";


    public static readonly string CreateHabitLogTable =
        @"
        CREATE TABLE IF NOT EXISTS HabitLog
        (
             Id Text PRIMARY KEY
            ,HabitId TEXT NOT NULL
            ,Date TEXT NOT NULL
            ,Quantity INTEGER NOT NULL
            ,FOREIGN KEY(HabitId) REFERENCES Habit(Id)
        );
        CREATE INDEX IF NOT EXISTS IX_HabitLog_HabitId ON HabitLog(HabitId);
        ";

    #endregion
}
