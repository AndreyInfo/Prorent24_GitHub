using Prorent24.Entity.Notification;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Notification
{
    public interface INotificationStorage
    {
        Task<List<NotificationEntity>> Create(List<NotificationEntity> model);

        Task<NotificationEntity> GetById(int id);

        Task<IPagedList<NotificationEntity>> GetAllByUser(string userId);

        Task<NotificationEntity> ReadNotification(int id);

        Task<List<NotificationEntity>> GetShortList(string userId);
    }
}
