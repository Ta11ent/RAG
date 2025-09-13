using AI_service.Shared.Data;
using AI_service.Shared.DbContext;

namespace AI_service.Extensions
{
    internal static class DatabaseExtension
    {
        internal static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSQL");

            services.AddScoped<IDbContext>(conf => new PostgreDbContext(connectionString!));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
