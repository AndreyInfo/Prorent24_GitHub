using Prorent24.BLL.Models.Subhire;
using Prorent24.BLL.Transfers.Contact;
using Prorent24.BLL.Transfers.ContactPerson;
using Prorent24.BLL.Transfers.CrewMember;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Project;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.Entity.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Subhire
{
    public static class SubhireTransfer
    {
        /// <summary>
        /// Transfer from List<SubhireEntity> to List<SubhireDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<SubhireDto> TransferToListDto(this List<SubhireEntity> entities)
        {
            List<SubhireDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from SubhireEntity to SubhireDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SubhireDto TransferToDto(this SubhireEntity entity, string exclude = null)
        {
            SubhireDto dto = new SubhireDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Number = entity.Number,
                Status = entity.Status,
                SupplierContactId = entity.SupplierContactId,
                SupplierContact = entity.SupplierContact?.TransferToDto(),
                SupplierContactPersonId = entity.SupplierContactPersonId,
                SupplierContactPerson = entity.SupplierContactPerson?.TransferToDto(),
                LocationContactId = entity.LocationContactId,
                LocationContact = entity.LocationContact?.TransferToDto(),
                LocationContactPersonId = entity.LocationContactPersonId,
                LocationContactPerson = entity.LocationContactPerson?.TransferToDto(),
                AccountManagerId = entity.AccountManagerId,
                AccountManager = entity.AccountManager?.TransferToDto(),
                Reference = entity.Reference,
                EquipmentCost = entity.EquipmentCost,
                AdditionalCost = entity.AdditionalCost,
                TotalCost = entity.TotalCost,
                Type = entity.Type,
                Remark = entity.Remark,
                UsagePeriodStart = entity.UsagePeriodStart,
                UsagePeriodEnd = entity.UsagePeriodEnd,
                PlanningPeriodStart = entity.PlanningPeriodStart,
                PlanningPeriodEnd = entity.PlanningPeriodEnd,
                DeliveryCollectionStart = entity.DeliveryCollectionStart,
                DeliveryCollectionEnd = entity.DeliveryCollectionEnd,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,

                Tasks = exclude == "Subhire" ? null : entity.Tasks?.ToList().TransferToListDto("Subhire"),
                Notes = entity.Notes?.ToList().TransferToListDto(),
                Tags = entity.Tags?.ToList().TransferToListTagDto(),
                Files = entity.Files?.ToList().TransferToListDto(),

                Projects = entity.Projects?.Where(x => x.Project != null).Select(x => x.Project).ToList().TransferToListDto()
            };

            return dto;
        }

        /// <summary>
        /// Transfer from SubhireDto to SubhireEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SubhireEntity TransferToEntity(this SubhireDto dto)
        {
            SubhireEntity entity = new SubhireEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Number = dto.Number,
                Status = dto.Status,
                SupplierContactId = dto.SupplierContactId,
                SupplierContact = dto.SupplierContact?.TransferToEntity(),
                SupplierContactPersonId = dto.SupplierContactPersonId,
                SupplierContactPerson = dto.SupplierContactPerson?.TransferToEntity(),
                LocationContactId = dto.LocationContactId,
                LocationContact = dto.LocationContact?.TransferToEntity(),
                LocationContactPersonId = dto.LocationContactPersonId,
                LocationContactPerson = dto.LocationContactPerson?.TransferToEntity(),
                AccountManagerId = dto.AccountManagerId,
                AccountManager = dto.AccountManager?.TransferToEntity(),
                Reference = dto.Reference,
                EquipmentCost = dto.EquipmentCost,
                AdditionalCost = dto.AdditionalCost,
                TotalCost = dto.TotalCost,
                Type = dto.Type,
                Remark = dto.Remark,
                UsagePeriodStart = dto.UsagePeriodStart,
                UsagePeriodEnd = dto.UsagePeriodEnd,
                PlanningPeriodStart = dto.PlanningPeriodStart,
                PlanningPeriodEnd = dto.PlanningPeriodEnd,
                DeliveryCollectionStart = dto.DeliveryCollectionStart,
                DeliveryCollectionEnd = dto.DeliveryCollectionEnd,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }
    }
}
