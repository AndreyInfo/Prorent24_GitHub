using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.Common.Extentions;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.Project;
using Prorent24.Enum.Equipment;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Project
{
    public static class ProjectEquipmentTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectEquipmentEntity> to List<ProjectEquipmentDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentDto> TransferToListDto(this List<ProjectEquipmentEntity> entities)
        {
            List<ProjectEquipmentDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }


        /// <summary>
        /// Transfer from  List<ProjectEquipmentDto> to List<ProjectEquipmentEntity>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentEntity> TransferToListEntity(this List<ProjectEquipmentDto> dtos)
        {
            List<ProjectEquipmentEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }


        /// <summary>
        /// Transfer from ProjectEquipmentEntity to ProjectEquipmentDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectEquipmentDto TransferToDto(this ProjectEquipmentEntity entity, string exclude = null)
        {
            ProjectEquipmentDto dto = new ProjectEquipmentDto()
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId.Value,
                ProjectName = entity.Project?.Name,
                Name = entity.Name,
                GroupId = entity.ProjectEquipmentGroupId,
                Group = entity.ProjectEquipmentGroup?.TransferToDto("Equipments"),
                EquipmentId = entity.EquipmentId,
                Equipment = entity.Equipment?.TransferToDto(),
                ParentId = entity.ParentId,
                Children = entity.Children?.ToList().TransferToListDto(),
                Quantity = entity.Quantity,
                Price = entity.Price,
                TotalPrice = entity.TotalPrice,
                Discount = entity.Discount,
                Factor = entity.Factor,
                Remark = entity.Remark,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,
                VatClassId = entity.VatClassId,
                AvailableQuantity = entity.CalculateAvailableQuantity(),
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectEquipmentDto to ProjectEquipmentEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectEquipmentEntity TransferToEntity(this ProjectEquipmentDto dto)
        {
            ProjectEquipmentEntity entity = new ProjectEquipmentEntity()
            {
                Id = dto.Id,
                ProjectId = dto.ProjectId,
                Name = dto.Name,
                ProjectEquipmentGroupId = dto.GroupId,
                EquipmentId = dto.EquipmentId,
                ParentId = dto.ParentId,
                Children = dto.Children?.ToList().TransferToListEntity(),
                Quantity = dto.Quantity > 0 ? dto.Quantity : 1,
                Price = dto.Price,
                TotalPrice = dto.TotalPrice,
                Discount = dto.Discount,
                Factor = dto.Factor,
                Remark = dto.Remark,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate,
                VatClassId = dto.VatClassId
            };

            return entity;
        }

        /// <summary>
        /// Transfer from ProjectEquipmentDto to ProjectEquipmentEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentEntity> TransferToListEntity(this ProjectEquipmentDto dto, List<EquipmentContentEntity> equipmentContentEntities, int parentId)
        {
            List<ProjectEquipmentEntity> entities = equipmentContentEntities.Where(x => x.Content != null).Select(x => new ProjectEquipmentEntity()
            {
                ProjectId = dto.ProjectId,
                Name = x.Content.Name,
                ProjectEquipmentGroupId = dto.GroupId,
                EquipmentId = x.ContentId,
                ParentId = parentId,
                Quantity = x.Content.Quantity.HasValue ? x.Content.Quantity.Value : 1,
                CreationDate = x.Content.CreationDate,
                LastModifiedDate = x.Content.LastModifiedDate,
                VatClassId = x.Content.VATClassId,
                Price = x.Content.SupplyType == SupplyTypeEnum.Rental ? (x.Content.RentalPrice.HasValue ? x.Content.RentalPrice.Value : 0) : (x.Content.SalePrice.HasValue ? x.Content.SalePrice.Value : 0),
                TotalPrice = x.Content.SupplyType == SupplyTypeEnum.Rental ? x.Content.RentalPrice.TotalPriceCalculation(0, 1, (x.Content.Quantity.HasValue ? x.Content.Quantity.Value : 1)) : x.Content.SalePrice.TotalPriceCalculation(0, 1, (x.Content.Quantity.HasValue ? x.Content.Quantity.Value : 1)),
                Discount = dto.Discount,
                Factor = dto.Factor,
                Remark = dto.Remark,
            }).ToList();

            return entities;
        }


        /// <summary>
        /// Transfer from ProjectEquipmentMovementDto to ProjectEquipmentMovementEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectEquipmentMovementEntity TransferToEntity(this ProjectEquipmentMovementDto dto, int projectId, int groupId, ProjectEquipmentMovementStatusEnum? movementStatus = null)
        {
            var result = new ProjectEquipmentMovementEntity()
            {
                //Id = dto.Id,
                ProjectId = projectId,
                GroupId = groupId,
                ProjectEquipmentId = dto.ProjectEquipmentId,
                EquipmentId = dto.EquipmentId,
                SelectedQuantity = dto.SelectedQuantity,
                TotalQuantity = dto.TotalQuantity,
                MovementStatus = movementStatus??dto.MovementStatus,
                KitCaseEquipments = dto.KitCaseEquipments != null ? dto.KitCaseEquipments.Select(item => item.TransferToEntity(projectId, groupId, dto.MovementStatus)).ToList() : null
            };

            return result;
        }

        /// <summary>
        /// Transfer from ProjectEquipmentMovementEntity to ProjectEquipmentMovementDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectEquipmentMovementDto  TransferToDto(this ProjectEquipmentMovementEntity entity, ProjectEquipmentMovementStatusEnum? movementStatus = null,SetTypeEnum setType=SetTypeEnum.Item)
        {
            var result = new ProjectEquipmentMovementDto()
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                GroupId = entity.GroupId,
                ProjectEquipmentId = entity.ProjectEquipmentId,
                EquipmentId = entity.EquipmentId,
                EquipmentName = entity.Equipment?.Name,
                EquipmentType = entity?.Equipment == null ? setType : (SetTypeEnum)entity?.Equipment?.SetType,
                SelectedQuantity = entity.SelectedQuantity,
                TotalQuantity = entity.TotalQuantity,
                MovementStatus = movementStatus ?? entity.MovementStatus,
                LimitQuantity = entity.SelectedQuantity,
                KitCaseEquipments = entity.KitCaseEquipments != null ? entity.KitCaseEquipments.Select(item => item.TransferToDto(entity.MovementStatus)).ToList() : null
            };

            return result;
        }


        public static long CalculateAvailableQuantity(this ProjectEquipmentEntity entity)
        {
            try
            {
                if (entity.Equipment.SupplyType == Enum.Equipment.SupplyTypeEnum.Rental)
                {
                    return entity?.Equipment?.Quantity - entity?.Equipment?.ProjectEquipments.Where(x => x.Id != entity.Id && x.ProjectEquipmentGroup.EndUsePeriod >= DateTime.UtcNow && x.ProjectEquipmentGroup.StartUsePeriod <= DateTime.UtcNow).Sum(z => z.Quantity) ?? 0;
                }
                else
                {
                    return entity?.Equipment?.Quantity ?? 0;

                }
            }
            catch (Exception ex)
            {
                return entity?.Equipment?.Quantity ?? 0;

            }
        }

        /// <summary>
        /// Transfer from List<ProjectEquipmentDto> to List<ProjectEquipmentGridDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentGridDto> TransferToListGridDto(this List<ProjectEquipmentDto> entities)
        {

            List<ProjectEquipmentGridDto> dtos = entities.Select(x => x.TransferToGridDto(null)).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from ProjectEquipmentDto to ProjectFunctionGridDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectEquipmentGridDto TransferToGridDto(this ProjectEquipmentDto dto, string exclude = null)
        {
            ProjectEquipmentGridDto grid = new ProjectEquipmentGridDto()
            {
                Id = dto.Id,
                GroupId = dto.GroupId,
                GroupName = null,
                EquipmentName = dto.EquipmentName,
                EquipmentId = dto.EquipmentId,
                ParentId = dto.ParentId,
                Childrens = dto.Children?.ToList().TransferToListGridDto(),
                Quantity = dto.Quantity,
                Price = dto.Price,
                TotalPrice = dto.TotalPrice,
                Discount = dto.Discount,
                Factor = dto.Factor,
                Remark = dto.Remark,
                AvailableQuantity = dto.AvailableQuantity,
                Group = dto.Group,
                VatClassId = dto.VatClassId

            };
            return grid;
        }

    }
}
