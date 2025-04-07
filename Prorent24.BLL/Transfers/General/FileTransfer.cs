using Prorent24.BLL.Models.General.File;
using Prorent24.Entity.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.General
{
    public static class FileTransfer
    {
        /// <summary>
        /// Transfer from List<FileEntity> to List<FileDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<FileDto> TransferToListDto(this List<FileEntity> entities)
        {
            List<FileDto> folders = entities.Select(x => new FileDto()
            {
                Id = x.Id,
                Name = x.Name,
                BelongsTo = x.BelongsTo,
                Confidential = x.Confidential,
                Description = x.Description,
                Path = x.Path,
                TaskId = x.TaskId,
                ContactId = x.ContactId,
                CrewMemberId = x.CrewMemberId,
                VehicleId = x.VehicleId,
                EquipmentId = x.EquipmentId,
                CreationDate = x.CreationDate,
                LastModifiedDate = x.LastModifiedDate
            }).ToList();

            return folders;
        }

        /// <summary>
        /// Transfer from FileDto to FileEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static FileEntity TransferToEntity(this FileDto dto)
        {
            FileEntity entity = new FileEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                BelongsTo = dto.BelongsTo,
                Confidential = dto.Confidential,
                Description = dto.Description,
                Path = dto.Path,
                TaskId = dto.TaskId,
                ContactId = dto.ContactId,
                CrewMemberId = dto.CrewMemberId,
                VehicleId = dto.VehicleId,
                EquipmentId = dto.EquipmentId,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate,
                InspectionId = dto.InspectionId,
                SubhireId = dto.SubhireId,
                RepairId = dto.RepairId,
                Size = dto.Size,
                IsImage = dto.IsImage
            };

            return entity;
        }

        /// <summary>
        /// Transfer from FileEntity to FileDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static FileDto TransferToDto(this FileEntity entity)
        {
            FileDto dto = new FileDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                BelongsTo = entity.BelongsTo,
                Confidential = entity.Confidential,
                Description = entity.Description,
                Path = entity.Path,
                TaskId = entity.TaskId,
                ContactId = entity.ContactId,
                CrewMemberId = entity.CrewMemberId,
                VehicleId = entity.VehicleId,
                EquipmentId = entity.EquipmentId,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,
                InspectionId = entity.InspectionId,
                SubhireId = entity.SubhireId,
                RepairId = entity.RepairId
            };

            return dto;
        }
    }
}
