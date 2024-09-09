using System.Data;

namespace HabitTracker.Infrastructure.Extensions;

/// <summary>
/// Custom extensions for the System.Data.IDataReader class.
/// </summary>
internal static class IDataReaderExtensions
{
    public static bool GetBoolean(this IDataReader reader, string columnName)
    {
        return reader.GetBoolean(reader.GetOrdinal(columnName));
    }

    public static DateTime GetDateTime(this IDataReader reader, string columnName)
    {
        return reader.GetDateTime(reader.GetOrdinal(columnName));
    }

    public static Guid GetGuid(this IDataReader reader, string columnName)
    {
        return reader.GetGuid(reader.GetOrdinal(columnName));
    }

    public static int GetInt32(this IDataReader reader, string columnName)
    {
        return reader.GetInt32(reader.GetOrdinal(columnName));
    }

    public static string GetString(this IDataReader reader, string columnName)
    {
        return reader.GetString(reader.GetOrdinal(columnName));
    }
}
