namespace AI_service.Shared.Configuration
{
   // public sealed record GrpcMlServiceSettings(int RetryCount, int RetryDelaySeconds);
   public class GrpcMlServiceSettings
    {
        public int RetryCount { get; init; }
        public int RetryDelaySeconds { get; init; }
    }
}
