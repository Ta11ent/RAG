using AI_service.Shared.DbContext;
using System.Data;

namespace AI_service.Shared.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction? _transaction;
        private bool _disposed = false;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbConnection = dbContext.CreateConnection() ?? throw new ArgumentNullException(nameof(dbContext));
            _dbConnection.Open();
        }

        public IDbConnection Connection
            => _dbConnection;

        public IDbTransaction Transaction 
            => _transaction ?? throw new InvalidOperationException("Transaction has not been started.");

        public IDbTransaction BeginTransaction()
            => _transaction = _dbConnection.BeginTransaction();

        public void Commit()
        {
            _transaction?.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _transaction = null;

                    if (_dbConnection.State != ConnectionState.Closed)
                        _dbConnection.Close();

                    _dbConnection.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
