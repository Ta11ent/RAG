namespace AI_service.Feature.TrainModel
{
    public sealed record TrainModelCommand(Guid tag, string content) : IRequest;
}
