using Prorent24.BLL.Models.Subhire;
using Prorent24.WebApp.Models.Subhire;
using Prorent24.WebApp.Transfers.Contact;
using Prorent24.WebApp.Transfers.ContactPerson;
using Prorent24.WebApp.Transfers.CrewMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Subhire
{
    public static class SubhireTransfer
    {
        /// <summary>
        /// Transfer from List<SubhireViewModel> to List<SubhireDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<SubhireDto> TransferToListDto(this List<SubhireViewModel> models)
        {
            List<SubhireDto> dtos = models.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from SubhireViewModel to SubhireDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SubhireDto TransferToDto(this SubhireViewModel model, string exclude = null)
        {
            SubhireDto dto = new SubhireDto()
            {
                Id = model.Id,
                Name = model.Name,
                Number = model.Number,
                Status = model.Status,
                SupplierContactId = model.SupplierContactId,
                //SupplierContact = model.SupplierContact?.TransferToDto(),
                SupplierContactPersonId = model.SupplierContactPersonId,
                //SupplierContactPerson = model.SupplierContactPerson?.TransferToDto(),
                LocationContactId = model.LocationContactId,
                //LocationContact = model.LocationContact?.TransferToDto(),
                LocationContactPersonId = model.LocationContactPersonId,
                //LocationContactPerson = model.LocationContactPerson?.TransferToDto(),
                AccountManagerId = model.AccountManagerId,
                //AccountManager = model.AccountManager?.TransferToDto(),
                Reference = model.Reference,
                EquipmentCost = model.EquipmentCost,
                AdditionalCost = model.AdditionalCost,
                TotalCost = model.TotalCost,
                Type = model.Type,
                Remark = model.Remark,
                UsagePeriodStart = model.UsagePeriodStart,
                UsagePeriodEnd = model.UsagePeriodEnd,
                PlanningPeriodStart = model.PlanningPeriodStart,
                PlanningPeriodEnd = model.PlanningPeriodEnd,
                DeliveryCollectionStart = model.DeliveryCollectionStart,
                DeliveryCollectionEnd = model.DeliveryCollectionEnd
            };

            return dto;
        }

        /// <summary>
        /// Transfer from SubhireDto to SubhireViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SubhireViewModel TransferToViewModel(this SubhireDto dto)
        {
            SubhireViewModel model = new SubhireViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Number = dto.Number,
                Status = dto.Status,
                SupplierContactId = dto.SupplierContactId,
                SupplierContactCompany = dto.SupplierContactCompany,
                SupplierContact = dto.SupplierContact?.TransferToViewModel(),
                SupplierContactPersonId = dto.SupplierContactPersonId,
                SupplierContactPerson = dto.SupplierContactPerson?.TransferToViewModel(),
                LocationContactId = dto.LocationContactId,
                LocationContact = dto.LocationContact?.TransferToViewModel(),
                LocationContactPersonId = dto.LocationContactPersonId,
                LocationContactPerson = dto.LocationContactPerson?.TransferToViewModel(),
                AccountManagerId = dto.AccountManagerId,
                AccountManager = dto.AccountManager?.TransferToViewModel(),
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
                ProjectName = dto.ProjectName
            };

            return model;
        }
    }
}
