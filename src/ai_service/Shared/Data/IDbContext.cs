using System.Data;

namespace AI_service.Shared.DbContext
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}
