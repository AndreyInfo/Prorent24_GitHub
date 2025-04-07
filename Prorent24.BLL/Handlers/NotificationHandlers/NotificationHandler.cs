using MediatR;
using Microsoft.AspNetCore.SignalR;
using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Services.Notification;
using Prorent24.Common.Hubs;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Prorent24.BLL.Handlers.NotificationHandlers
{
    public class NotificationHandler : INotificationHandler<NotificationHandlerModel>
    {
        private readonly INotificationService _notificationService;
        private readonly IHubContext<HubConnector> _hubContext;
        private readonly INotifier _notifier;

        public NotificationHandler(INotifier notifier,INotificationService notificationService, IHubContext<HubConnector> hubContext)
        {
            _notificationService = notificationService;
            _hubContext = hubContext;
            _notifier = notifier;
        }

        public async Task Handle(NotificationHandlerModel notification, CancellationToken cancellationToken)
        {
            NotificationHandlerModel model = await _notificationService.Create(notification);
            await _notifier.SendAsync(notification.UserIds.ToArray(), options =>
            {
                options.Module = ModuleTypeEnum.Tasks.ToString();
                options.Type = PushType.Notification;
                options.Event = EventType.Info;
                options.Title = notification.Theme;
                options.Message = notification.Message;
            });

        }
    }

}
