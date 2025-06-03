using AI_service.Shared.Outcome;
using FluentValidation;
using FluentValidation.Results;

namespace AI_service.Shared.Behaviors
{
    internal class ValidationDecorator
    {
        internal sealed class CommandBaseHandler<TRequest>(
            IRequestHandler<TRequest> inner,
            IEnumerable<IValidator<TRequest>> validators)
                : IRequestHandler<TRequest> where TRequest : IRequest
        {
            public async Task<Result> Handle(TRequest request, CancellationToken cancellationToken)
            {
                var context = new ValidationContext<TRequest>(request);
                ValidationResult[] validationResults = await Task.WhenAll(
                    validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                ValidationFailure[] failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToArray();

                if (failures.Length != 0)
                {
                    return Result.Failure(CreateValidationError(failures));
                }

                return await inner.Handle(request, cancellationToken);
            }
        }

        internal sealed class CommandHandler<TRequest, TResponse>(
           IRequestHandler<TRequest, TResponse> inner,
           IEnumerable<IValidator<TRequest>> validators)
            : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
            {
                var context = new ValidationContext<TRequest>(request);
                ValidationResult[] validationResults = await Task.WhenAll(
                    validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                ValidationFailure[] failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToArray();

                if (failures.Length != 0)
                {
                    return Result.Failure<TResponse>(CreateValidationError(failures));
                }

                return await inner.Handle(request, cancellationToken);
            }
        }

        private static ValidationError CreateValidationError(ValidationFailure[] validationFailures)
            => new(validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage)).ToArray());
    }
}
