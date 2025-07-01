namespace AI_service.Feature.TrainModel
{
    public interface ITrainMlService
    {
        Task<Guid> TrainModel(string content, Guid tagId, CancellationToken cancellationToken);
    }
}
