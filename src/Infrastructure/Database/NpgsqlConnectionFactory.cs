using System.Data;
using Npgsql;

namespace Infrastructure.Database;

public class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly string? _connectionString;

    public NpgsqlConnectionFactory(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(token);
        return connection;
    }
}