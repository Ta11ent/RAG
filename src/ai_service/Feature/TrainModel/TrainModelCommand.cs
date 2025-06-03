namespace AI_service.Feature.TrainModel
{
    public sealed record TrainModelCommand(int tag, string content) : IRequest;
}
