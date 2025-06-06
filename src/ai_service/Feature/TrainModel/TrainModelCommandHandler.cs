using AI_service.Shared.Outcome;

namespace AI_service.Feature.TrainModel
{
    internal sealed class TrainModelCommandHandler : IRequestHandler<TrainModelCommand>
    {
        private readonly ITrainingInputStoreService _inputStoreService;

        internal TrainModelCommandHandler(ITrainingInputStoreService inputStoreService)
        {
            _inputStoreService = inputStoreService;
        }

        public async Task<Result> Handle(TrainModelCommand command, CancellationToken cancellationToken)
        {
            var vectorId = await _inputStoreService.StoreHandler(
                command.tag,
                command.content,
                cancellationToken);

            return Result.Success();
        }
    }
}
