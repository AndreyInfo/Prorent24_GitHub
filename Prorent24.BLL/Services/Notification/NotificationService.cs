using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Models.Notification;
using Prorent24.BLL.Transfers.Notification;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Notification;
using Prorent24.Entity.Notification;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Notification
{
    internal class NotificationService : BaseService, INotificationService
    {
        private readonly INotificationStorage _notificationStorage;
        public NotificationService(IHttpContextAccessor httpContextAccessor, IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage, INotificationStorage notificationStorage):base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _notificationStorage = notificationStorage;
        }
        public async Task<NotificationHandlerModel> Create(NotificationHandlerModel model)
        {
            List<NotificationEntity> entities = await _notificationStorage.Create(model.TransferToEntity());
            return model;
        }

        public async Task<NotificationDto> GetById(int id)
        {
            NotificationEntity entity = await _notificationStorage.GetById(id);
            NotificationDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<BaseListDto> GetPagedList()
        {
            IPagedList<NotificationEntity> pagedList = await _notificationStorage.GetAllByUser(GetUserId());
            List <NotificationEntity> listEntities = pagedList.Items.ToList();
            List<NotificationDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<NotificationDto>(await GetUserColumns(EntityEnum.ProjectEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.NotificationEntity
            };
        }

        public async Task<List<NotificationDto>> GetShortList()
        {
            List<NotificationEntity> list = await _notificationStorage.GetShortList(GetUserId());
            return list.TransferToListDto();
        }

        public async Task<NotificationDto> ReadNotification(int id)
        {
            NotificationEntity entity = await _notificationStorage.ReadNotification(id);
            NotificationDto dto = entity?.TransferToDto();
            return dto;
        }
    }
}
