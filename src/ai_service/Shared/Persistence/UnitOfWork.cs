using System.Data;

namespace AI_service.Shared.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbTransaction? _transaction;
        private bool _disposed = false;

        public IDbTransaction Transaction 
            => _transaction ?? throw new InvalidOperationException("Transaction has not been started.");

        public IDbTransaction BeginTransaction(IDbConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("Connection is not Open");
            }

            _transaction = connection.BeginTransaction();
            return _transaction;
        }

        public void Commit()
        {
            EnsureTransactionStarted();
            _transaction?.Commit();
            Dispose();
        }

        public void Rollback()
        {
            EnsureTransactionStarted();
            _transaction?.Rollback();
            Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _transaction = null;
                }
            }
            _disposed = true;
        }

        private void EnsureTransactionStarted()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }
        }
    }
}
