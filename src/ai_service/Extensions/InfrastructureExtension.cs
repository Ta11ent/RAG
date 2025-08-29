using AI_service.Shared.Behaviors;
using AI_service.Shared.Services.ml_service;

namespace AI_service.Extensions
{
    internal static class InfrastructureExtension
    {
        internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<Ml.Vector.VectorClient>(client =>
            {
                var test = configuration["MLService:GrpcUrl"]!;
                client.Address = new Uri(configuration["MLService:GrpcUrl"]!);
            })
            .AddInterceptor<RetryInceraptor>();

            services.AddTransient<IMlServiceClient, MlServiceClient>();
            services.AddTransient<RetryInceraptor>();

            return services;
        }
    }
}
