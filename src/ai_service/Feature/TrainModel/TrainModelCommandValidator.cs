using FluentValidation;

namespace AI_service.Feature.TrainModel
{
    internal class TrainModelCommandValidator : AbstractValidator<TrainModelCommand>
    {
        public TrainModelCommandValidator()
        {
            RuleFor(x => x.content).NotEmpty();
            RuleFor(x => x.tagId).NotEqual(Guid.Empty);
        }
    }
}
