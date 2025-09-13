namespace AI_service.Shared.Outcome
{
    internal sealed record ValidationError : Error
    {
        internal ValidationError(Error[] errors)
            : base(
                "General.Validation",
                "One or more validation errors occurred",
                ErrorType.Validation)
        {
            Errors = errors;
        }

        internal Error[] Errors { get; }

        internal static ValidationError FromResults(IEnumerable<Result> results) 
            => new(results.Where(r => !r.IsSuccess).Select(r => r.Error).ToArray());
    }
}
