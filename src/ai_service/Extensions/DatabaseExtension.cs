using AI_service.Shared.DbContext;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AI_service.Extensions
{
    internal static class DatabaseExtension
    {
        internal static IServiceCollection AddDbConnection(this IServiceCollection services)
        {
            services.TryAddScoped<IDbContext, PostgreDbContext>();
            return services;
        }

    }
}
