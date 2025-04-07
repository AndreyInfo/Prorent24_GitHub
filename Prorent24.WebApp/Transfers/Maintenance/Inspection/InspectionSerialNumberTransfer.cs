using Prorent24.BLL.Models.Maintenance;
using Prorent24.WebApp.Models.Maintenance.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Maintenance.InspectionSerialNumber
{
    public static class InspectionSerialNumberSerialNumberTransfer
    {
        /// <summary>
        /// Transfer from List<InspectionSerialNumberViewModel> to List<InspectionSerialNumberDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<InspectionSerialNumberDto> TransferToListDto(this List<InspectionSerialNumberViewModel> models)
        {
            List<InspectionSerialNumberDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from InspectionSerialNumberDto to InspectionSerialNumberViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static InspectionSerialNumberDto TransferToDto(this InspectionSerialNumberViewModel model)
        {
            InspectionSerialNumberDto dto = new InspectionSerialNumberDto()
            {
                Id = model.Id,
                InspectionId = model.InspectionId,
                EquipmentId = model.EquipmentId,
                SerialNumberId = model.SerialNumberId
                //AlternativeId = model.AlternativeId
            };

            return dto;
        }

        /// <summary>
        /// Transfer from InspectionSerialNumberViewModel to InspectionSerialNumberDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static InspectionSerialNumberViewModel TransferToViewModel(this InspectionSerialNumberDto dto)
        {
            InspectionSerialNumberViewModel model = new InspectionSerialNumberViewModel()
            {
                Id = dto.Id,
                InspectionId = dto.InspectionId,
                EquipmentId = dto.EquipmentId,
                Equipment = dto.Equipment?.Name,
                SerialNumberId = dto.SerialNumberId,
                SerialNumber = dto.SerialNumber?.SerialNumber
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<InspectionSerialNumberDto> to List<InspectionSerialNumberViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<InspectionSerialNumberViewModel> TransferToViewModel(this List<InspectionSerialNumberDto> dtos)
        {
            List<InspectionSerialNumberViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
