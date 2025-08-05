using FluentValidation;

namespace AI_service.Feature.TrainModel
{
    internal class TrainModelCommandValidator : AbstractValidator<TrainModelCommand>
    {
        public TrainModelCommandValidator()
        {
            RuleFor(x => x.content).NotEmpty();
            RuleFor(x => x.tagIds)
                .Must(tags => tags.Any(tag => tag != Guid.Empty))
                .WithMessage("At least one tag ID must be provided and not be empty.");
        }
    }
}
