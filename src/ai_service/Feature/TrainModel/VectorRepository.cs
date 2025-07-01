using AI_service.Shared.Data;
using AI_service.Shared.Domain;
using Dapper;

namespace AI_service.Feature.TrainModel
{
   
    internal class VectorRepository : IVectorRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public VectorRepository(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<Guid> AddOriginalContent(string context, CancellationToken cancellationToken)
        {
            var text = Text.Create(context);

            await _unitOfWork.Connection.InsertAsync(text, cancellationToken: cancellationToken);

            return text.Id;
        }

        public async Task AddVectorAsync(Guid tagId, Guid vectorId, Guid textId, CancellationToken cancellationToken)
        {
            var vector = Vector.Create(textId, vectorId);
            var vectorTags = VectorTag.Create(vectorId, tagId);

            var transaction = _unitOfWork.BeginTransaction();

            try
            {
                await _unitOfWork.Connection.InsertAsync(vector, transaction, cancellationToken);
                await _unitOfWork.Connection.InsertAsync(vectorTags, transaction, cancellationToken);

                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<Tag?> GetTagAsync(Guid tagId, CancellationToken cancellationToken)
        {
            var sql = "SELECT * FROM Tags WHERE Id = @Id";

            return await _unitOfWork.Connection.QuerySingleOrDefaultAsync<Tag>(sql, new { Id = tagId });
        }
    }
}
