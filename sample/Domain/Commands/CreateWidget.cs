using CommandQuery.Framing;
using CommandQueryApiSample.Domain.Messages;
using CommandQueryApiSample.Domain.Requests;

namespace CommandQueryApiSample.Domain.Commands;

//v1
public class CreateWidget(IDomainEventPublisher publisher)
    : IAsyncHandler<CreateWidgetMessage, CommandResponse<string>>
{
    public async Task<CommandResponse<string>> Execute(CreateWidgetMessage message, CancellationToken cancellationToken = default)
    {
        var response = Guid.NewGuid().ToString();

        publisher.MessageResult += (sender, eventargs) =>
        {
            response += $"Name: {message.Name} message was sent and processed with Success={eventargs.Success}";
        };

        await publisher.Publish(new WidgetCreated{Name = message.Name}, cancellationToken);

        return Response.Ok(response);
    }
}