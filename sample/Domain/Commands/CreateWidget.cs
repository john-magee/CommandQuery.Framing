using CommandQuery.Framing;
using CommandQueryApiSample.Domain.Messages;
using CommandQueryApiSample.Domain.Requests;

namespace CommandQueryApiSample.Domain.Commands
{
    public class CreateWidget(IDomainEventPublisher publisher)
        : IAsyncHandler<CreateWidgetMessage, CommandResponse<string>>
    {
        private readonly IDomainEventPublisher _publisher = publisher;

        public async Task<CommandResponse<string>> Execute(CreateWidgetMessage message)
        {
            var response = Guid.NewGuid().ToString();

            _publisher.MessageResult += (sender, eventargs) =>
            {
                response += $" message was sent and processed with Success={eventargs.Success}";
            };

            await _publisher.Publish(new WidgetCreated());

            return Response.Ok(response);
        }
    }
}