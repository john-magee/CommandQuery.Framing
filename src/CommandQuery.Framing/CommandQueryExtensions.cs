using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CommandQuery.Framing;

public static class CommandQueryExtensions
{
    public static IServiceCollection AddCommandQuery(this IServiceCollection serviceCollection, params Assembly[] handlers)
    {
        serviceCollection.AddSingleton<IBroker, Broker>();
        serviceCollection.AddSingleton<IDomainEventPublisher, DomainEventPublisher>();

        serviceCollection.ScanAndAddTransientTypes(handlers,
                                                   [
                                                       typeof(IAsyncHandler<,>),
                                                       typeof(IHandler<,>),
                                                       typeof(IDomainEvent<>)
                                                   ]);

        return serviceCollection;
    }
}