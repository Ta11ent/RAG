namespace AI_service.Feature.TrainModel
{
    public sealed record TrainModelCommand(Guid[] tagIds, string content) : IRequest;
}
