using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models.Subhire;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.Entity.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Subhire
{
    public static class SubhireEquipmentTransfer
    {
        /// <summary>
        /// Transfer from List<SubhireEquipmentEntity> to List<SubhireEquipmentDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<SubhireEquipmentDto> TransferToListDto(this List<SubhireEquipmentEntity> entities, DateTime? from = null, DateTime? to = null)
        {
            List<SubhireEquipmentDto> dtos = entities.Select(x => x.TransferToDto(from, to)).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<SubhireEquipmentDto> to List<SubhireEquipmentEntity>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<SubhireEquipmentEntity> TransferToListEntity(this List<SubhireEquipmentDto> dtos)
        {
            List<SubhireEquipmentEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }


        /// <summary>
        /// Transfer from SubhireEquipmentEntity to SubhireEquipmentDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SubhireEquipmentDto TransferToDto(this SubhireEquipmentEntity entity, DateTime? from = null, DateTime? to = null, string exclude = null)
        {
            SubhireEquipmentDto dto = new SubhireEquipmentDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                SubhireId = entity.SubhireId,
                GroupId = entity.SubhireEquipmentGroupId,
                Group = entity.SubhireEquipmentGroup?.TransferToDto("equipments"),
                Equipment = entity.Equipment?.TransferToDto("Equipment"),
                EquipmentId = entity.EquipmentId,
                ParentId = entity.ParentId,
                Children = entity.Children?.ToList().TransferToListDto(),
                Quantity = entity.Quantity,
                Price = entity.Price,
                TotalPrice = entity.TotalPrice,
                Discount = entity.Discount,
                Factor = entity.Equipment?.FactorGroup?.FactorGroupOptions != null ? entity.Equipment.FactorGroup.FactorGroupOptions.FactorCalculation(from, to) : 0,
                Remark = entity.Remark,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return dto;
        }

        /// <summary>
        /// Transfer from SubhireEquipmentDto to SubhireEquipmentEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SubhireEquipmentEntity TransferToEntity(this SubhireEquipmentDto dto)
        {
            SubhireEquipmentEntity entity = new SubhireEquipmentEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                SubhireId = dto.SubhireId,
                SubhireEquipmentGroupId = dto.GroupId,
                EquipmentId = dto.EquipmentId,
                ParentId = dto.ParentId,
                Children = dto.Children?.ToList().TransferToListEntity(),
                Quantity = dto.Quantity,
                Price = dto.Price,
                TotalPrice = dto.TotalPrice,
                Discount = dto.Discount,
                Factor = dto.Factor,
                Remark = dto.Remark,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }

        public static SubhireEquipmentGridDto TransferToTreeGridDto(this SubhireEquipmentEntity entity, DateTime? from = null, DateTime? to = null, string obj = null)
        {
            decimal? child = null;
            SubhireEquipmentGridDto model = new SubhireEquipmentGridDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                SubhireId = entity.SubhireId,
                GroupId = entity.SubhireEquipmentGroupId,
                //GroupName = entity.SubhireEquipmentGroup?.Name, //в еквіпментах не виводимо 
                EquipmentId = entity.EquipmentId,
                EquipmentName = entity.Equipment?.Name,
                ParentId = entity.ParentId,
                Quantity = entity.Quantity,
                Price = (obj == "child") ? child : entity.Price,
                TotalPrice = (obj == "child") ? child : entity.TotalPrice,
                Discount = (obj == "child") ? child : entity.Discount,
                Factor = (obj == "child") ? child : entity.Equipment?.FactorGroup?.FactorGroupOptions != null ? entity.Equipment.FactorGroup.FactorGroupOptions.FactorCalculation(from, to) : 0,
                Remark = entity.Remark,
                Childrens = entity.Children?.ToList().TransferToTreeGridDto(from, to, "child"),

            };

            return model;
        }

        public static List<SubhireEquipmentGridDto> TransferToTreeGridDto(this List<SubhireEquipmentEntity> dtos, DateTime? from = null, DateTime? to = null, string obj = null)
        {
            List<SubhireEquipmentGridDto> dto = dtos.Select(x => x.TransferToTreeGridDto(from, to, obj)).ToList();
            return dto;
        }
    }
}
