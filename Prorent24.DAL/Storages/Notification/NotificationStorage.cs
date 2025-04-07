using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Notification;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Notification
{
    internal class NotificationStorage : BaseStorage<NotificationEntity>, INotificationStorage
    {
        public NotificationStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IPagedList<NotificationEntity>> GetAllByUser(string userId)
        {
            return await _repos.TableNoTracking.Where(x => x.UserId == userId).OrderByDescending(x => x.CreationDate).ToPagedListAsync(0, 500);
        }

        public async Task<NotificationEntity> GetById(int id)
        {
            return await _repos.Table
              .Include(x => x.Task).ThenInclude(x => x.Executors).ThenInclude(x => x.CrewMember)
              .Include(x => x.Task).ThenInclude(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
              .Include(x => x.Project)
              .Include(x => x.Repair)
              .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<NotificationEntity>> GetShortList(string userId)
        {
            List<NotificationEntity> list =  await _repos.TableNoTracking.Where(x => x.UserId == userId && x.IsRead != true ).ToListAsync();
            return list.TakeLast(5).OrderByDescending(x=>x.Id).ToList();
        }

        public async Task<NotificationEntity> ReadNotification(int id)
        {
            NotificationEntity notification = await _repos.FindAsync(id);
            notification.IsRead = true;
            await Task.Run(() =>
            {
                _repos.Update(notification, f => f.IsRead);
                _unitOfWork.SaveChanges();
            });

            return notification;
        }
    }
}
