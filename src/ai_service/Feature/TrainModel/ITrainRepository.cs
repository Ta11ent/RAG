using AI_service.Shared.Domain.Entities;
using System.Data;

namespace AI_service.Feature.TrainModel
{
    internal interface ITrainRepository
    {
        Task AddVectorAsync(
            Vector vector, 
            IDbConnection connection, 
            CancellationToken cancellationToken,
            IDbTransaction? dbTransaction = null);

        Task AddPayloadTagAsync(
            IEnumerable<PayloadTag> payloadTag,
            IDbConnection connection, 
            CancellationToken cancellationToken,
            IDbTransaction? dbTransaction = null);

        Task AddPayloadAsync(
            Payload content, 
            IDbConnection connection, 
            CancellationToken cancellationToken,
            IDbTransaction? dbTransaction = null); 

        Task<IEnumerable<Tag?>> GetTagAsync(
            Guid[] tags,
            IDbConnection connection, 
            CancellationToken cancellationToken,
            IDbTransaction? dbTransaction = null);

        Task AddPendingTrainAsync(
            PendingTrain pendingTrain,
            IDbConnection connection, 
            CancellationToken cancellationToken,
            IDbTransaction? dbTransaction = null);
    }
}
