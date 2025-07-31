namespace AI_service.Shared.Outcome
{
    internal sealed record UnavailableError : Error
    {
        public UnavailableError(Error error)
            : base(
                 "General.Unavailable",
                 "Unavailable error occurred",
                 ErrorType.Unavailable)
        {
            Error = error;
        }

        internal Error Error { get; }
        
        internal static UnavailableError FromResults(Result result) => new (result.Error);
    }
}
