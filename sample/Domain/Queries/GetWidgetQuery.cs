using CommandQuery.Framing;
using CommandQueryApiSample.Domain.Messages;
using CommandQueryApiSample.Domain.Models;

namespace CommandQueryApiSample.Domain.Queries
{
    public class GetWidgetQuery
        : IAsyncHandler<GetWidget, Widget>
    {
        public async Task<Widget> Execute(GetWidget message)
        {
            var widget = new Widget
            {
                Id = message.Id
            };

            return await Task.FromResult(widget);
        }
    }
}