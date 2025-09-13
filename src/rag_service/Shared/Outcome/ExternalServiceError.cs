namespace AI_service.Shared.Outcome
{
    internal sealed record ExternalServiceError : Error
    {
        public ExternalServiceError(Error error)
            : base(
                 "General.Unavailable",
                 "An error occurred while calling external service",
                 ErrorType.Unavailable)
        {
            Error = error;
        }

        internal Error Error { get; }
        
        internal static ExternalServiceError FromResults(Result result) => new (result.Error);
    }
}
