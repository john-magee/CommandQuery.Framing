using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandQuery.Framing;

public class DomainEventPublisher(IServiceProvider serviceProvider)
    : IDomainEventPublisher
{
    public event EventHandler MessageSent;
    public event EventHandler<DomainEventArgs> MessageResult;

    public async Task Publish<TMessageType>(TMessageType message, CancellationToken cancellationToken = default)
    {
        var events = serviceProvider.GetServices<IDomainEvent<TMessageType>>();

        foreach (var domainEvent in events)
        {
            domainEvent.OnComplete += DomainEvent_OnComplete;
            
            MessageSent?.Invoke(this, new DomainEventArgs());

            await domainEvent.Execute(message);
        }
    }

    private void DomainEvent_OnComplete(object sender, DomainEventArgs e)
    {
        MessageResult?.Invoke(this, e);
    }
}