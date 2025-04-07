using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Models.Notification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Notification
{
    public interface INotificationService
    {
        Task<NotificationHandlerModel> Create(NotificationHandlerModel model);
        Task<BaseListDto> GetPagedList();
        Task<NotificationDto> GetById(int id);
        Task<NotificationDto> ReadNotification(int id);
        Task<List<NotificationDto>> GetShortList();
    }
}
