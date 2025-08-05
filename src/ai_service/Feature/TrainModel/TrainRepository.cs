using AI_service.Shared.Data;
using AI_service.Shared.Domain.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace AI_service.Feature.TrainModel
{

    internal class TrainRepository : ITrainRepository
    {
        public async Task AddPayloadAsync(
            Payload content, 
            IDbConnection connection, 
            CancellationToken cancellationToken,
            IDbTransaction? dbTransaction = null)
        {
            await connection.InsertAsync(content, dbTransaction, cancellationToken);
        }

        public async Task AddPayloadTagAsync(
            IEnumerable<PayloadTag> payloadTag, 
            IDbConnection connection, 
            CancellationToken cancellationToken,
            IDbTransaction? dbTransaction = null)
        {
            await connection.InsertAsync(payloadTag, dbTransaction, cancellationToken);
        }

        public async Task AddPendingTrainAsync(
            PendingTrain pendingTrain, 
            IDbConnection connection, 
            CancellationToken cancellationToken, 
            IDbTransaction? dbTransaction = null)
        {
            await connection.InsertAsync(pendingTrain, dbTransaction, cancellationToken);
        }

        public async Task AddVectorAsync(
            Vector vector, 
            IDbConnection connection, 
            CancellationToken cancellationToken, 
            IDbTransaction? dbTransaction = null)
        {
            await connection.InsertAsync(vector, dbTransaction, cancellationToken);
        }

        public async Task<IEnumerable<Tag?>> GetTagAsync(
            Guid[] tags, 
            IDbConnection connection,
            CancellationToken cancellationToken, 
            IDbTransaction? dbTransaction = null)
        {
            var sql = "SELECT * FROM Tags WHERE Id = ANY(@Ids)";
            return await connection.QueryAsync<Tag?>(sql, new { Ids = tags });
        }
    }
}
