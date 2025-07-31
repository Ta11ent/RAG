namespace AI_service.Shared.Configuration
{
    public sealed record GrpcMlServiceSettings(int RetryCount, int RetryDelaySeconds);
}
