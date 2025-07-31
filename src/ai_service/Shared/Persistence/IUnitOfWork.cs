using System.Data;

namespace AI_service.Shared.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        IDbTransaction BeginTransaction(IDbConnection connection);
        IDbTransaction Transaction { get; }
    }
}
