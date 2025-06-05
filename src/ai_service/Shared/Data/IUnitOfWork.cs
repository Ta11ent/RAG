using System.Data;

namespace AI_service.Shared.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void Rollback();
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
    }
}
