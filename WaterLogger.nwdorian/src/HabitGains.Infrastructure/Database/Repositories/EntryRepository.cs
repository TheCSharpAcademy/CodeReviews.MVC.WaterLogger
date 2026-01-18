using System.Data.Common;
using System.Globalization;
using System.Text;
using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Application.Core.Pagination;
using HabitGains.Application.Core.Pagination.Entries;
using HabitGains.Domain.Entries;
using HabitGains.Infrastructure.Database.ConnectionFactory;
using Microsoft.Data.Sqlite;

namespace HabitGains.Infrastructure.Database.Repositories;

public class EntryRepository(IDbConnectionFactory connectionFactory) : IEntryRepository
{
    public async Task<IReadOnlyList<Entry>> GetEntriesPageByHabitId(
        Guid habitId,
        EntryFilter filter,
        EntrySorting sorting,
        Paging paging,
        CancellationToken cancellationToken
    )
    {
        using SqliteConnection connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        using SqliteCommand command = connection.CreateCommand();

        StringBuilder query = new(
            """
            SELECT Id, HabitId, Date, Quantity, CreatedAt, UpdatedAt
            FROM entry
            WHERE HabitId = @HabitId
            """
        );

        ApplyEntryFilter(query, command, filter);
        ApplyEntrySorting(query, sorting);
        ApplyPaging(query, command, paging);

        command.CommandText = query.ToString();

        command.Parameters.AddWithValue("@HabitId", habitId);

        using SqliteDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        List<Entry> entries = new();

        while (await reader.ReadAsync(cancellationToken))
        {
            Entry entry = new()
            {
                Id = reader.GetGuid(0),
                HabitId = reader.GetGuid(1),
                Date = reader.GetDateTime(2),
                Quantity = reader.GetDecimal(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = await reader.IsDBNullAsync(5, cancellationToken) ? null : reader.GetDateTime(5),
            };

            entries.Add(entry);
        }

        return entries;
    }

    public async Task<int> CountEntriesByHabitId(Guid habitId, EntryFilter filter, CancellationToken cancellationToken)
    {
        using SqliteConnection connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        using SqliteCommand command = connection.CreateCommand();

        StringBuilder query = new(
            """
            SELECT Count(*)
            FROM entry
            WHERE HabitId = @HabitId
            """
        );

        ApplyEntryFilter(query, command, filter);

        command.CommandText = query.ToString();

        command.Parameters.AddWithValue("@HabitId", habitId);

        using SqliteDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        await reader.ReadAsync(cancellationToken);
        return reader.GetInt32(0);
    }

    public async Task<decimal> GetTotalQuantityByHabitId(
        Guid habitId,
        EntryFilter filter,
        CancellationToken cancellationToken
    )
    {
        using SqliteConnection connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        using SqliteCommand command = connection.CreateCommand();

        StringBuilder query = new(
            """
            SELECT SUM(Quantity)
            FROM entry
            WHERE HabitId = @HabitId
            """
        );

        ApplyEntryFilter(query, command, filter);

        command.CommandText = query.ToString();

        command.Parameters.AddWithValue("@HabitId", habitId);

        using SqliteDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        await reader.ReadAsync(cancellationToken);
        return await reader.IsDBNullAsync(0, cancellationToken) ? 0 : reader.GetDecimal(0);
    }

    public async Task<IReadOnlyList<Entry>> GetEntriesForChart(
        Guid habitId,
        EntryFilter filter,
        CancellationToken cancellationToken
    )
    {
        using SqliteConnection connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        using SqliteCommand command = connection.CreateCommand();

        StringBuilder query = new(
            """
                SELECT Id, HabitId, Date, Quantity, CreatedAt, UpdatedAt
                FROM entry
                WHERE HabitId = @HabitId
            """
        );

        ApplyEntryFilter(query, command, filter);

        command.CommandText = query.ToString();

        command.Parameters.AddWithValue("@HabitId", habitId);

        using SqliteDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        List<Entry> entries = new();

        while (await reader.ReadAsync(cancellationToken))
        {
            Entry entry = new()
            {
                Id = reader.GetGuid(0),
                HabitId = reader.GetGuid(1),
                Date = reader.GetDateTime(2),
                Quantity = reader.GetDecimal(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = await reader.IsDBNullAsync(5, cancellationToken) ? null : reader.GetDateTime(5),
            };

            entries.Add(entry);
        }

        return entries;
    }

    public async Task<Entry?> GetById(Guid id, CancellationToken cancellationToken)
    {
        using SqliteConnection connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        using SqliteCommand command = connection.CreateCommand();

        command.CommandText = """
                SELECT Id, HabitId, Date, Quantity, CreatedAt, UpdatedAt
                FROM entry
                WHERE Id = @Id
            """;

        command.Parameters.AddWithValue("@Id", id);

        using SqliteDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new Entry()
        {
            Id = reader.GetGuid(0),
            HabitId = reader.GetGuid(1),
            Date = reader.GetDateTime(2),
            Quantity = reader.GetDecimal(3),
            CreatedAt = reader.GetDateTime(4),
            UpdatedAt = await reader.IsDBNullAsync(5, cancellationToken) ? null : reader.GetDateTime(5),
        };
    }

    public async Task Create(Entry entry, CancellationToken cancellationToken)
    {
        using SqliteConnection connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        using SqliteCommand command = connection.CreateCommand();

        command.CommandText = """
                INSERT INTO entry (Id, HabitId, Date, Quantity, CreatedAt, UpdatedAt)
                VALUES (@Id, @HabitId, @Date, @Quantity, @CreatedAt, @UpdatedAt)
            """;

        command.Parameters.AddWithValue("@Id", entry.Id);
        command.Parameters.AddWithValue("@HabitId", entry.HabitId);
        command.Parameters.AddWithValue("@Date", entry.Date);
        command.Parameters.AddWithValue("@Quantity", entry.Quantity);
        command.Parameters.AddWithValue("@CreatedAt", entry.CreatedAt);
        command.Parameters.AddWithValue("@UpdatedAt", entry.UpdatedAt is null ? DBNull.Value : entry.UpdatedAt);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task Update(Entry entry, CancellationToken cancellationToken)
    {
        using SqliteConnection connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        using SqliteCommand command = connection.CreateCommand();

        command.CommandText = """
                UPDATE entry
                SET Date = @Date, Quantity = @Quantity, UpdatedAt = @UpdatedAt
                WHERE Id = @Id
            """;

        command.Parameters.AddWithValue("@Id", entry.Id);
        command.Parameters.AddWithValue("@Date", entry.Date);
        command.Parameters.AddWithValue("@Quantity", entry.Quantity);
        command.Parameters.AddWithValue("@UpdatedAt", entry.UpdatedAt);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        using SqliteConnection connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        using SqliteCommand command = connection.CreateCommand();

        command.CommandText = "DELETE FROM entry WHERE Id = @Id";

        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task BulkInsert(List<Entry> entries)
    {
        using SqliteConnection connection = await connectionFactory.CreateConnectionAsync();
        using DbTransaction transaction = await connection.BeginTransactionAsync();

        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
                INSERT INTO entry (Id, HabitId, Date, Quantity, CreatedAt, UpdatedAt)
                VALUES (@Id, @HabitId, @Date, @Quantity, @CreatedAt, @UpdatedAt)
            """;

        foreach (Entry entry in entries)
        {
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Id", entry.Id);
            command.Parameters.AddWithValue("@HabitId", entry.HabitId);
            command.Parameters.AddWithValue("@Date", entry.Date);
            command.Parameters.AddWithValue("@Quantity", entry.Quantity);
            command.Parameters.AddWithValue("@CreatedAt", entry.CreatedAt);
            command.Parameters.AddWithValue("@UpdatedAt", entry.UpdatedAt is null ? DBNull.Value : entry.UpdatedAt);

            await command.ExecuteNonQueryAsync();
        }

        await transaction.CommitAsync();
    }

    public async Task<bool> Any()
    {
        using SqliteConnection connection = await connectionFactory.CreateConnectionAsync();

        using SqliteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT 1 FROM entry LIMIT 1;";

        using SqliteDataReader reader = await command.ExecuteReaderAsync();

        return reader.HasRows;
    }

    private static void ApplyEntryFilter(StringBuilder query, SqliteCommand command, EntryFilter filter)
    {
        if (filter.QuantityFrom is not null)
        {
            query.Append(" AND Quantity > @QuantityFrom");
            command.Parameters.AddWithValue("QuantityFrom", filter.QuantityFrom);
        }

        if (filter.QuantityTo is not null)
        {
            query.Append(" AND Quantity < @QuantityTo");
            command.Parameters.AddWithValue("QuantityTo", filter.QuantityTo);
        }

        if (filter.DateFrom is not null)
        {
            query.Append(" AND Date > @DateFrom");
            command.Parameters.AddWithValue("@DateFrom", filter.DateFrom);
        }

        if (filter.DateTo is not null)
        {
            query.Append(" AND Date < @DateTo");
            command.Parameters.AddWithValue("@DateTo", filter.DateTo);
        }
    }

    private static void ApplyEntrySorting(StringBuilder query, EntrySorting sorting)
    {
        query.Append(CultureInfo.InvariantCulture, $" ORDER BY {sorting.SortColumn} {sorting.SortOrder}");
    }

    private static void ApplyPaging(StringBuilder query, SqliteCommand command, Paging paging)
    {
        query.Append(" LIMIT @PageSize OFFSET @Offset");
        command.Parameters.AddWithValue("@PageSize", paging.PageSize);
        command.Parameters.AddWithValue("@Offset", (paging.Page - 1) * paging.PageSize);
    }
}
