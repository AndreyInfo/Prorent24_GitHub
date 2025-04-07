using Prorent24.BLL.Models.Subhire;
using Prorent24.WebApp.Models.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Subhire
{
    public static class SubhireEquipmentTransfer
    {
        /// <summary>
        /// Transfer from List<SubhireEquipmentViewModel> to List<SubhireEquipmentDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<SubhireEquipmentDto> TransferToListDto(this List<SubhireEquipmentViewModel> entities)
        {
            List<SubhireEquipmentDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<SubhireEquipmentDto> to List<SubhireEquipmentViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<SubhireEquipmentViewModel> TransferToListViewModel(this List<SubhireEquipmentDto> dtos)
        {
            List<SubhireEquipmentViewModel> view = dtos.Select(x => x.TransferToViewModel()).ToList();
            return view;
        }


        /// <summary>
        /// Transfer from SubhireEquipmentViewModel to SubhireEquipmentDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SubhireEquipmentDto TransferToDto(this SubhireEquipmentViewModel view, string exclude = null)
        {
            SubhireEquipmentDto dto = new SubhireEquipmentDto()
            {
                Id = view.Id,
                GroupId = view.GroupId,
                Name = view.Name,
                EquipmentId = view.EquipmentId,
                ParentId = view.ParentId,
                Children = view.Children?.ToList().TransferToListDto(),
                Quantity = view.Quantity,
                Price = view.Price,
                TotalPrice = view.TotalPrice,
                Discount = view.Discount,
                Factor = view.Factor,
                Remark = view.Remark
            };

            return dto;
        }

        /// <summary>
        /// Transfer from SubhireEquipmentDto to SubhireEquipmentViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SubhireEquipmentViewModel TransferToViewModel(this SubhireEquipmentDto dto)
        {
            SubhireEquipmentViewModel model = new SubhireEquipmentViewModel()
            {
                Id = dto.Id,
                GroupId = dto.GroupId,
                GroupName = dto.GroupName,
                Name = dto.Name,
                EquipmentId = dto.EquipmentId,
                EquipmentName = dto.EquipmentName,
                ParentId = dto.ParentId,
                Children = dto.Children?.ToList().TransferToListViewModel(),
                Quantity = dto.Quantity,
                Price = dto.Price,
                TotalPrice = dto.TotalPrice,
                Discount = dto.Discount,
                Factor = dto.Factor,
                Remark = dto.Remark
            };

            return model;
        }
    }
}
