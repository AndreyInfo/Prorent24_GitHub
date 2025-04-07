using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.Common.Hubs
{
    public interface INotifier
    {
        Task SendAsync(string[] userIds, Action<NotifierOptions> options);
    }
}
