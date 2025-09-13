using DbUp;

namespace AI_service.Extensions
{
    internal static class MigrationExtensions
    {
        internal static void ApplyMigration(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            var connectionString = scope.ServiceProvider
                .GetRequiredService<IConfiguration>()
                .GetConnectionString("PostgreSQL");

            var migrationsPath = Path.Combine(Directory.GetCurrentDirectory(), "Shared", "Persistence", "Migrations");

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(connectionString) 
                .WithScriptsFromFileSystem(migrationsPath) 
                .LogToConsole() 
                .Build();

            var result = upgrader.PerformUpgrade();
        }
    }
}
