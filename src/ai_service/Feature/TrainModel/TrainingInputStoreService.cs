using AI_service.Shared.Data;
using AI_service.Shared.Domain;

namespace AI_service.Feature.TrainModel
{
    internal interface ITrainingInputStoreService
    {
        Task<Guid> StoreHandler(TrainModelCommand command, CancellationToken cancellationToken);
    }

    internal class TrainingInputStoreService : ITrainingInputStoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        internal TrainingInputStoreService(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<Guid> StoreHandler(TrainModelCommand command, CancellationToken cancellationToken)
        {
            var text = Text.Create(command.content);

            var vector = Vector.Create(
                text.Id,
                DateTime.UtcNow,
                Guid.NewGuid());

            var vectorTag = VectorTag.Create(
                vector.Id,
                command.tag);

            try
            {
                _unitOfWork.BeginTransaction();

                await _unitOfWork.InsertAsync(text, cancellationToken);
                await _unitOfWork.InsertAsync(vector, cancellationToken);
                await _unitOfWork.InsertAsync(vectorTag, cancellationToken);

                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }

            return vector.Id;
        }
    }
}
