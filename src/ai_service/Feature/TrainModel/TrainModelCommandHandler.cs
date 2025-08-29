using AI_service.Shared.Outcome;
using AI_service.Shared.Services.ml_service;
using Ml;

namespace AI_service.Feature.TrainModel
{
    internal sealed class TrainModelCommandHandler : IRequestHandler<TrainModelCommand>
    {
        private readonly ITrainService _trainService;
        private readonly IMlServiceClient _mlService;

        public TrainModelCommandHandler(
            ITrainService trainService,
            IMlServiceClient mlService)
        {
            _trainService = trainService ?? throw new ArgumentNullException(nameof(trainService));
            _mlService = mlService ?? throw new ArgumentNullException(nameof(mlService));
        }

        public async Task<Result> Handle(TrainModelCommand command, CancellationToken cancellationToken)
        {
            var tags = await _trainService.GetNonExistentTagsAsync(command.tagIds, cancellationToken);
            if (tags.Any())
                return Result.Failure(DomainErrors.Tag.NotFound(tags));

            var payloadId = await _trainService.AddPayloadtAsync(command.content, command.tagIds, cancellationToken);

            var dataEntry = CreateDataEntry(command.content, tags);
            var response = await _mlService.TrainAsync(new[] { dataEntry });

            if (response.Success)
            {
                var vectorId = Guid.Parse(response.Ids.First());
                await _trainService.AddVectorAsync(payloadId, vectorId, cancellationToken);
            }
            else
            {
                await _trainService.AddPendingTrainAsync(payloadId, cancellationToken);
            }

            return Result.Success();
        }

        private static DataEntry CreateDataEntry(string content, Guid[] tags) =>
            new()
            {
                Text = content,
                Tags = { tags.Select(tagId => tagId.ToString()) }
            };
    }
}
