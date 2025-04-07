using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Prorent24.Common.Hubs
{
    internal class Notifier : INotifier
    {
        private readonly IHubContext<HubConnector> _hubContext;

        public Notifier(IHubContext<HubConnector> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendAsync(string[] userIds, Action<NotifierOptions> options)
        {
            NotifierOptions _options = new NotifierOptions();
            options.Invoke(_options);

            await _hubContext.Clients.Users(userIds).SendAsync(_options.Type.ToString(), new EventModel(_options.Event, new EventData()
            {
                Title = _options.Title,
                Message = _options.Message
            })); 
        }
    }
}
