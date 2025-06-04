using Npgsql;
using System.Data;

namespace AI_service.Shared.DbContext
{
    internal sealed class PostgreDbContext : IDbContext
    {
        private readonly string _connectionString;

        internal PostgreDbContext(string connectionString)
            => _connectionString = connectionString;
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);
    }
}
