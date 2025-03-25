using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CommandQuery.Framing
{
    public class Broker(IServiceProvider serviceProvider)
        : IBroker
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public TResponse Handle<TRequest, TResponse>(TRequest message) where TRequest : IMessage
        {
            var messageHandler = _serviceProvider.GetService<IHandler<TRequest, TResponse>>();
            var result = messageHandler.Execute(message);

            return result;
        }

        public async Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest message) where TRequest : IMessage
        {
            var messageHandler = _serviceProvider.GetService<IAsyncHandler<TRequest, TResponse>>();
            var result = await messageHandler.Execute(message);
            
            return result;
        }
    }
}