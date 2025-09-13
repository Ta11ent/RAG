using AI_service.Endpoints;
using AI_service.Shared.Data;
using AI_service.Shared.DbContext;
using AI_service.Shared.Domain.Entities;

namespace AI_service.Feature.TrainModel
{
    internal sealed class TrainService : ITrainService
    {
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrainRepository _trainRepository;

        public TrainService(
            IDbContext dbContext,
            IUnitOfWork unitOfWork,
            ITrainRepository trainRepository)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _trainRepository = trainRepository;
        }

        public async Task<Guid> AddPayloadtAsync(string value, Guid[] tags, CancellationToken cancellationToken)
        {
            var payload = Payload.Create(value);
            var payloadTags = tags.Select(tag => PayloadTag.Create(payload.Id, tag)).ToArray();

            using var connection = _dbContext.CreateConnection();
            connection.Open();

            var transaction = _unitOfWork.BeginTransaction(connection);

            try
            {
                await _trainRepository.AddPayloadAsync(payload, connection, cancellationToken);
                await _trainRepository.AddPayloadTagAsync(payloadTags, connection, cancellationToken);

                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                return Guid.Empty;
            }

            return payload.Id;
        }

        public async Task AddPendingTrainAsync(Guid payloadId, CancellationToken cancellationToken)
        {
            var pendingTrain = PendingTrain.Create(payloadId);
            
            using var connection = _dbContext.CreateConnection();
            connection.Open();

            await _trainRepository.AddPendingTrainAsync(pendingTrain, connection, cancellationToken);
        }

        public async Task AddVectorAsync(Guid payloadId, Guid vectorId, CancellationToken cancellationToken)
        {
            var vector = Vector.Create(payloadId, vectorId);

            using var connection = _dbContext.CreateConnection();
            connection.Open();

            await _trainRepository.AddVectorAsync(vector, connection, cancellationToken);
        }

        public async Task<Guid[]> GetNonExistentTagsAsync(Guid[] tags, CancellationToken cancellationToken)
        {
            using var connection = _dbContext.CreateConnection();
            connection.Open();

            var response = await _trainRepository.GetTagAsync(tags, connection, cancellationToken);
            return tags.Except(response.Select(x => x.Id)).ToArray();
        }
    }
}
