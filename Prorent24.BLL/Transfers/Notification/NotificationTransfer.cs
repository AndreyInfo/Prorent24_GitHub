using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Models.Notification;
using Prorent24.BLL.Transfers.Maintenance.Repair;
using Prorent24.BLL.Transfers.Project;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.Entity.Notification;
using Prorent24.Enum.General;
using Prorent24.Enum.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Notification
{
    public static class NotificationTransfer
    {
        public static List<NotificationEntity> TransferToEntity(this NotificationHandlerModel model)
        {
            List<NotificationEntity> entities = model.UserIds.Select(x => new NotificationEntity()
            {
                Type = model.Type,
                Theme = model.Theme,
                Message = model.Message,
                TaskId = model.Type == NotificationTypeEnum.TaskCreatedPrivate ? model.EntityId : null,
                ProjectId = model.Type == NotificationTypeEnum.ProjectCreated ? model.EntityId : null,
                RepairId = model.Type == NotificationTypeEnum.RepairCreated ? model.EntityId : null,
                UserId = x
            }).ToList();
            return entities;
        }


        public static NotificationHandlerModel TransferToHandlerModel(this NotificationEntity model)
        {
            NotificationHandlerModel handlerModel = new NotificationHandlerModel()
            {
                Type = model.Type,
                Theme = model.Theme,
                Message = model.Message,
                EntityId = model.TransferToEntityId(),
                UserId = model.CreationUserId

            };
            return handlerModel;
        }

        public static NotificationDto TransferToDto(this NotificationEntity model)
        {
            NotificationDto dto = new NotificationDto()
            {
                Id = model.Id,
                Type = model.Type,
                Theme = model.Theme,
                Message = model.Message,
                EntityId = model.TransferToEntityId(),
                IsRead = model.IsRead??false,
                ModuleType = model.Type.TransferToModule(),
                CreationDate = model.CreationDate
            };
            return dto;
        }

        public static List<NotificationDto> TransferToListDto(this List<NotificationEntity> entities)
        {
            List<NotificationDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        public static int TransferToEntityId(this NotificationEntity model)
        {
            switch (model.Type)
            {
                case NotificationTypeEnum.TaskCreatedPrivate:
                    return (int)model.TaskId;
                default:
                    return 0;


            }
        }

        public static ModuleTypeEnum? TransferToModule(this NotificationTypeEnum type)
        {
            switch (type)
            {
                case NotificationTypeEnum.TaskCreatedPrivate:
                    return ModuleTypeEnum.Tasks;
                case NotificationTypeEnum.ProjectCreated:
                    return ModuleTypeEnum.Projects;
                case NotificationTypeEnum.RepairCreated:
                    return ModuleTypeEnum.Maintenance;
                default:
                    return null;


            }
        }

    }
}
