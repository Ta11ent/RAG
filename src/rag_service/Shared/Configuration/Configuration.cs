namespace AI_service.Shared.Configuration
{
    internal static class Configuration
    {
        internal static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GrpcMlServiceSettings>(configuration.GetSection("PollyConfiguration:Grpc:MlService"));

            return services;
        }
    }
}
