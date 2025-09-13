namespace AI_service.Feature.TrainModel
{
    public interface ITrainService
    {
        Task<Guid[]> GetNonExistentTagsAsync(Guid[] tags, CancellationToken cancellationToken);

        Task<Guid> AddPayloadtAsync(string value, Guid[] tags, CancellationToken cancellationToken);

        Task AddVectorAsync(Guid payloadId, Guid vectorId, CancellationToken cancellationToken);

        Task AddPendingTrainAsync(Guid payloadId, CancellationToken cancellationToken);
    }
}
