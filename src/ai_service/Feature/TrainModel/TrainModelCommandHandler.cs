using AI_service.Shared.Outcome;

namespace AI_service.Feature.TrainModel
{
    internal sealed class TrainModelCommandHandler : IRequestHandler<TrainModelCommand>
    {
        private readonly IVectorRepository _repository;
      //  private readonly ITrainMlService _trainService;

        public TrainModelCommandHandler(
            IVectorRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            //_trainService = trainService ?? throw new ArgumentNullException(nameof(trainService));
        }

        public async Task<Result> Handle(TrainModelCommand command, CancellationToken cancellationToken)
        {
            var tag = await _repository.GetTagAsync(command.tagId, cancellationToken);
            if (tag is null)
                return Result.Failure(DomainErrors.Tag.NotFound(command.tagId));

            var textId = await _repository.AddOriginalContent(command.content, cancellationToken);

            //var vectorId = await _trainService.TrainModel(command.content, command.tagId, cancellationToken);

            Guid vectorId = Guid.NewGuid();

            await _repository.AddVectorAsync(command.tagId, vectorId, textId, cancellationToken);

            return Result.Success();
        }
    }
}
