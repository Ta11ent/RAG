using AI_service.Shared.Data;
using AI_service.Shared.DbContext;
using AI_service.Shared.Domain.Entities;
using Dapper;

namespace AI_service.Feature.TrainModel
{
   
    internal class VectorRepository : IVectorRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
   
        public VectorRepository(
            IDbContext dbContext,
            IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
           
        public async Task<Guid> AddOriginalContent(string context, CancellationToken cancellationToken)
        {
            var text = Text.Create(context);

            using var connection = _dbContext.CreateConnection();
            connection.Open();

            await connection.InsertAsync(text, cancellationToken: cancellationToken);

            return text.Id;
        }

        public async Task AddVectorAsync(Guid tagId, Guid vectorId, Guid textId, CancellationToken cancellationToken)
        {
            var vector = Vector.Create(textId, vectorId);
            var vectorTags = VectorTag.Create(vector.Id, tagId);

            using var connection = _dbContext.CreateConnection();
            connection.Open();

            var transaction = _unitOfWork.BeginTransaction(connection);

            try
            {
                await connection.InsertAsync(vector, transaction, cancellationToken);
                await connection.InsertAsync(vectorTags, transaction, cancellationToken);

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
            using var connection = _dbContext.CreateConnection();
            connection.Open();

            var sql = "SELECT * FROM Tags WHERE Id = @Id";

            return await connection.QuerySingleOrDefaultAsync<Tag>(sql, new { Id = tagId });
        }
    }
}
