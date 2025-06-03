using Npgsql;
using System.Data;

namespace AI_service.Shared.DbContext
{
    internal sealed class PostgreDbContext : IDbContext
    {
        private readonly string _connectionString;

        internal PostgreDbContext(IConfiguration configuration)
            => _connectionString = configuration.GetConnectionString("PostgreSQL") 
                ?? throw new ArgumentNullException(nameof(_connectionString));
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);
    }
}
