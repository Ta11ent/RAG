namespace AI_service.Feature.TrainModel
{
    internal static class TrainModelServiceRegistration
    {
        internal static IServiceCollection AddTrainModelFeature(this IServiceCollection services)
        {
            services.AddScoped<ITrainingInputStoreService, TrainingInputStoreService>();
            return services;
        }
    }
}
