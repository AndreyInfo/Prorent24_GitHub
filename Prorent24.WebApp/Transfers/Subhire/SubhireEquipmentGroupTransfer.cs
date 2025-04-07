using Prorent24.BLL.Models.Subhire;
using Prorent24.WebApp.Models.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Subhire
{
    public static class SubhireEquipmentGroupTransfer
    {
        /// <summary>
        /// Transfer from List<SubhireEquipmentGroupViewModel> to List<SubhireEquipmentGroupDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<SubhireEquipmentGroupDto> TransferToListDto(this List<SubhireEquipmentGroupViewModel> views)
        {
            List<SubhireEquipmentGroupDto> dtos = views.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<SubhireEquipmentGroupDto> to List<SubhireEquipmentGroupViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<SubhireEquipmentGroupViewModel> TransferToListViewModel(this List<SubhireEquipmentGroupDto> dtos)
        {
            List<SubhireEquipmentGroupViewModel> views = dtos.Select(x => x.TransferToViewModel()).ToList();
            return views;
        }


        /// <summary>
        /// Transfer from SubhireEquipmentGroupViewModel to SubhireEquipmentGroupDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static SubhireEquipmentGroupDto TransferToDto(this SubhireEquipmentGroupViewModel view, string exclude = null)
        {
            SubhireEquipmentGroupDto dto = new SubhireEquipmentGroupDto()
            {
                Id = view.Id,
                SubhireId = view.SubhireId,
                Name = view.Name,
                Equipments = view.Equipments?.ToList().TransferToListDto()
            };

            return dto;
        }

        /// <summary>
        /// Transfer from SubhireEquipmentGroupDto to SubhireEquipmentGroupViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SubhireEquipmentGroupViewModel TransferToViewModel(this SubhireEquipmentGroupDto dto)
        {
            SubhireEquipmentGroupViewModel entity = new SubhireEquipmentGroupViewModel()
            {
                Id = dto.Id,
                SubhireId = dto.SubhireId,
                Name = dto.Name,
                Equipments = dto.Equipments?.ToList().TransferToListViewModel(),
                GroupId = dto.Id
            };

            return entity;
        }
    }
}
