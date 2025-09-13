namespace AI_service.Feature.TrainModel
{
    internal static class TrainModelServiceRegistration
    {
        internal static IServiceCollection AddTrainModelFeature(this IServiceCollection services)
        {
            services.AddScoped<ITrainRepository, TrainRepository>();
            services.AddScoped<ITrainService, TrainService>();

            return services;
        }
    }
}
