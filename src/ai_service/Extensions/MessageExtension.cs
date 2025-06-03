using AI_service.Shared.Behaviors;
using FluentValidation;

namespace AI_service.Extensions
{
    internal static class MessageExtension
    {
        internal static IServiceCollection AddMessage(this IServiceCollection services)
        {
            services.Scan(scan => scan
              .FromAssembliesOf(typeof(MessageExtension))
              .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)), publicOnly: false)
              .AsImplementedInterfaces()
              .WithScopedLifetime());

            services.Decorate(typeof(IRequestHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));

            services.AddValidatorsFromAssembly(typeof(MessageExtension).Assembly, includeInternalTypes: true);

            return services;
        }
    }
}
