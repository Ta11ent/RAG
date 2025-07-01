namespace AI_service.Feature.TrainModel
{
    public sealed record TrainModelCommand(Guid tagId, string content) : IRequest;
}
