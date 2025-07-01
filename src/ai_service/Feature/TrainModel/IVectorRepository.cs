using AI_service.Shared.Domain;

namespace AI_service.Feature.TrainModel
{
    internal interface IVectorRepository
    {
        Task AddVectorAsync(Guid tagId, Guid vectorId, Guid textId, CancellationToken cancellationToken);

        Task<Guid> AddOriginalContent(string context, CancellationToken cancellationToken); 

        Task<Tag?> GetTagAsync(Guid tagId, CancellationToken cancellationToken);
    }
}
