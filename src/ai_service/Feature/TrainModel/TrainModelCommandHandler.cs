using AI_service.Shared.Outcome;

namespace AI_service.Feature.TrainModel
{
    internal sealed class TrainModelCommandHandler : IRequestHandler<TrainModelCommand>
    {
        public Task<Result> Handle(TrainModelCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
