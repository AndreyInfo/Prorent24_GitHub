using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Prorent24.Common.Hubs
{
    public class HubConnector : Hub
    {
        public HubConnector()
        {
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }
}
