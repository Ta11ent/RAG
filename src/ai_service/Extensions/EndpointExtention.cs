using AI_service.Endpoints;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace AI_service.Extensions
{
    internal static class EndpointExtention
    {
        internal static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            services.AddEndpoints(Assembly.GetExecutingAssembly());

            return services;
        }

        internal static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
           ServiceDescriptor[] serviceDescriptors = assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false} &&
                               type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }

        internal static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroup = null)
        {
            IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

            IEndpointRouteBuilder builder = routeGroup is null ? app : routeGroup;

            foreach (IEndpoint endpoint in endpoints)
            {
                endpoint.MapEndpoint(builder);
            }

            return app;
        }
    }
}
