using Prorent24.BLL.Models.Maintenance;
using Prorent24.WebApp.Models.General;
using Prorent24.WebApp.Models.Maintenance.Repair;
using Prorent24.WebApp.Transfers.Contact;
using Prorent24.WebApp.Transfers.CrewMember;
using Prorent24.WebApp.Transfers.Equipment;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.Maintenance.Repair
{
    public static class RepairTransfer
    {
        /// <summary>
        /// Transfer from List<RepairViewModel> to List<RepairDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<RepairDto> TransferToListDto(this List<RepairViewModel> models)
        {
            List<RepairDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from RepairDto to RepairViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static RepairDto TransferToDto(this RepairViewModel model)
        {
            RepairDto dto = new RepairDto()
            {
                Id = model.Id,
                EquipmentId = model.EquipmentId,
                SerialNumberId = model.SerialNumberId,

                AssignToId = model.AssignToId,
                ExternalRepairId = model.ExternalRepairId,
                ReportedById = model.ReportedById,
                From = model.From,
                Until = model.Until,
                Quantity = model.Quantity,
                Cost = model.Cost,

                Remark = model.Remark,
                Usable = model.Usable
            };

            return dto;
        }

        /// <summary>
        /// Transfer from RepairViewModel to RepairDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static RepairViewModel TransferToViewModel(this RepairDto dto)
        {
            RepairViewModel model = new RepairViewModel()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                SerialNumberId = dto.SerialNumberId,

                AssignToId = dto.AssignToId,
                ExternalRepairId = dto.ExternalRepairId,
                ReportedById = dto.ReportedById,
                From = dto.From,
                Until = dto.Until,
                Quantity = dto.Quantity,
                Cost = dto.Cost,

                Remark = dto.Remark,
                Usable = dto.Usable,
                AssignTo = dto.AssignTo != null ? new SelectViewModel()
                {
                    Id = dto.AssignTo?.Id,
                    Name = dto.AssignTo?.FirstName + " " + dto.AssignTo?.LastName
                } : null,
                ExternalRepair = dto.ExternalRepair != null ? new SelectViewModel()
                {
                    Id = dto.ExternalRepair?.Id,
                    Name = dto.ExternalRepair?.FirstName + " " + dto.ExternalRepair?.LastName
                } : null,
                Equipment = dto.Equipment != null ? new SelectViewModel()
                {
                    Id = dto.Equipment?.Id,
                    Name = dto.Equipment?.Name
                } : null,
                ReportedBy = dto.ReportedBy != null ? new SelectViewModel()
                {
                    Id = dto.ReportedBy?.Id,
                    Name = dto.ReportedBy?.FirstName + " " + dto.ReportedBy?.LastName
                } : null,

            };
            return model;
        }

        /// <summary>
        /// Transfer from List<RepairDto> to List<RepairViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<RepairViewModel> TransferToViewModel(this List<RepairDto> dtos)
        {
            List<RepairViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
