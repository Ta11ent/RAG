namespace AI_service.Shared.Outcome
{
    internal static class InfrastructureErrors
    {
        internal static class MlService
        {
            internal static Error Unavailable() => Error.Unavailable(
                "General.Unavailable",
                 "An error occurred while calling external service");
        }
    }
}
