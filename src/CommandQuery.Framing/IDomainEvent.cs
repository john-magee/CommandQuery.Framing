using System;
using System.Threading.Tasks;

namespace CommandQuery.Framing
{
    public interface IDomainEvent<in T>
    {
        event EventHandler<DomainEventArgs> OnComplete;

        Task Execute(T message);
    }
}