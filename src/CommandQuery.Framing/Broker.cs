using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandQuery.Framing;

public class Broker(IServiceProvider serviceProvider)
    : IBroker
{
    public TResponse Handle<TRequest, TResponse>(TRequest message) where TRequest : IMessage
    {
        var messageHandler = serviceProvider.GetService<IHandler<TRequest, TResponse>>();
        var result = messageHandler.Execute(message);

        return result;
    }

    public async Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest message, CancellationToken cancellationToken) where TRequest : IMessage
    {
        var messageHandler = serviceProvider.GetService<IAsyncHandler<TRequest, TResponse>>();
        var result = await messageHandler.Execute(message, cancellationToken);

        return result;
    }
}