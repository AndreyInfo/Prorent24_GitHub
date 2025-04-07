using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Project
{
    public static class ProjectEquipmentTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectEquipmentViewModel> to List<ProjectEquipmentDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentDto> TransferToListDto(this List<ProjectEquipmentViewModel> entities)
        {
            List<ProjectEquipmentDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from  List<ProjectEquipmentDto> to List<ProjectEquipmentViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentViewModel> TransferToListViewModel(this List<ProjectEquipmentDto> dtos)
        {
            List<ProjectEquipmentViewModel> view = dtos.Select(x => x.TransferToViewModel()).ToList();
            return view;
        }


        /// <summary>
        /// Transfer from ProjectEquipmentViewModel to ProjectEquipmentDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectEquipmentDto TransferToDto(this ProjectEquipmentViewModel view, string exclude = null)
        {
            ProjectEquipmentDto dto = new ProjectEquipmentDto()
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
                Remark = view.Remark,
                VatClassId = view.VatClassId
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectEquipmentDto to ProjectEquipmentViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectEquipmentViewModel TransferToViewModel(this ProjectEquipmentDto dto)
        {
            ProjectEquipmentViewModel entity = new ProjectEquipmentViewModel()
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
                Remark = dto.Remark,
                VatClassId = dto.VatClassId,
                AvailableQuantity = dto.AvailableQuantity
            };

            return entity;
        }
    }
}
