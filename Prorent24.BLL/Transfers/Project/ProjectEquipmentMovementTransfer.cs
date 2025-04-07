using Prorent24.BLL.Models.Project;
using Prorent24.Entity.Project;
using Prorent24.Enum.Equipment;
using Prorent24.Enum.Project;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Project
{
    public static class ProjectEquipmentMovementTransfer
    {
        public static List<ProjectEquipmentMovementDto> TransferToListDto(this List<ProjectEquipmentMovementEntity> projEquipmentMovement)
        {
            List<ProjectEquipmentMovementDto> movements = projEquipmentMovement.Select(item =>

               item.TransferToDto()
        ).ToList();

            return movements;

        }

        private static ProjectEquipmentMovementDto TransferToDto(this ProjectEquipmentDto projEquipment, ProjectEquipmentMovementStatusEnum status, ProjectEquipmentDto parentProjEquipment = null)
        {
            ProjectEquipmentMovementDto dto = new ProjectEquipmentMovementDto()
            {
                ProjectId = projEquipment.ProjectId,
                ProjectName = projEquipment.ProjectName,
                GroupId = projEquipment.GroupId,
                GroupName = projEquipment.GroupName,
                ProjectEquipmentId = projEquipment.Id,
                EquipmentId = projEquipment.EquipmentId,
                EquipmentName = projEquipment.EquipmentName,
                SelectedQuantity = projEquipment.Quantity * (parentProjEquipment?.Quantity ?? 1),
                TotalQuantity = projEquipment.Quantity * (parentProjEquipment?.Quantity ?? 1),
                MovementStatus = status,
                KitCaseEquipments = projEquipment.Equipment?.SetType == SetTypeEnum.Kit ? projEquipment.Children?.Select(x => x.TransferToDto(status, projEquipment)).ToList() : null,
                LimitQuantity = projEquipment.Quantity * (parentProjEquipment?.Quantity ?? 1)
            };

            return dto;
        }

        private static ProjectEquipmentMovementDto TransferToDto(this ProjectEquipmentMovementEntity projEquipmentMove)
        {
            ProjectEquipmentMovementDto dto = new ProjectEquipmentMovementDto()
            {
                Id = projEquipmentMove.Id,
                ProjectId = projEquipmentMove.ProjectId,
                ProjectName = projEquipmentMove.Project.Name,
                GroupId = projEquipmentMove.GroupId,
                GroupName = projEquipmentMove.ProjectEquipmentGroup.Name,
                ProjectEquipmentId = projEquipmentMove.ProjectEquipmentId,
                EquipmentId = projEquipmentMove.EquipmentId,
                EquipmentName = projEquipmentMove?.Equipment?.Name,
                EquipmentType = projEquipmentMove?.Equipment?.SetType!=null ?(SetTypeEnum)projEquipmentMove?.Equipment?.SetType : SetTypeEnum.Item,
                SelectedQuantity = projEquipmentMove.SelectedQuantity,
                TotalQuantity = projEquipmentMove.TotalQuantity,
                MovementStatus = projEquipmentMove.MovementStatus,
                KitCaseEquipmentId = projEquipmentMove.KitCaseEquipmentId,
                KitCaseEquipments = projEquipmentMove.KitCaseEquipments != null ? projEquipmentMove.KitCaseEquipments.ToList().Select(x => x.TransferToDto()).ToList() : null,
                LimitQuantity = projEquipmentMove.SelectedQuantity,
                SerialNumbers = projEquipmentMove?.Equipment?.EquipmentSerialNumbers?.Select(x=>x.SerialNumber).ToList()
            };

            return dto;
        }

        private static int CalculateKitCase(this List<ProjectEquipmentEntity> entities)
        {
            int quantity = 0;
            for (int i = 0; i < entities.Count; i++)
            {
                quantity += entities[i].Quantity;
            }

            return quantity;
        }


    }
}
