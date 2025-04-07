using Prorent24.BLL.Models.Maintenance;
using Prorent24.WebApp.Models.Maintenance.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Maintenance.Inspection
{
    public static class InspectionTransfer
    {
        /// <summary>
        /// Transfer from List<InspectionViewModel> to List<InspectionDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<InspectionDto> TransferToListDto(this List<InspectionViewModel> models)
        {
            List<InspectionDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from InspectionDto to InspectionViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static InspectionDto TransferToDto(this InspectionViewModel model)
        {
            InspectionDto dto = new InspectionDto()
            {
                Id = model.Id,
                Date = model.Date,
                Description = model.Description,
                PeriodicInspectionId = model.PeriodicInspectionId,
                Status = model.Status
            };

            return dto;
        }

        /// <summary>
        /// Transfer from InspectionViewModel to InspectionDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static InspectionViewModel TransferToViewModel(this InspectionDto dto)
        {
            InspectionViewModel model = new InspectionViewModel()
            {
                Id = dto.Id,
                Date = dto.Date,
                Description = dto.Description,
                PeriodicInspectionId = dto.PeriodicInspectionId,
                Status = dto.Status
                
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<InspectionDto> to List<InspectionViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<InspectionViewModel> TransferToViewModel(this List<InspectionDto> dtos)
        {
            List<InspectionViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
