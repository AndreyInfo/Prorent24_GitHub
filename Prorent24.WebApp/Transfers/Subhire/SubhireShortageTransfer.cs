using Prorent24.BLL.Models.Subhire;
using Prorent24.WebApp.Models.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Subhire
{
    public static class  SubhireShortageShortageTransfer
    {

        /// <summary>
        /// Transfer from List< SubhireShortageViewModel> to List< SubhireShortageDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List< SubhireShortageDto> TransferToListDto(this List< SubhireShortageViewModel> models)
        {
            List< SubhireShortageDto> dtos = models.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from List< SubhireShortageDto> toList< SubhireShortageViewModel>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<SubhireShortageViewModel> TransferToListViewModel(this  List<SubhireShortageDto> models)
        {
            List<SubhireShortageViewModel> view = models.Select(x => x.TransferToViewModel()).ToList();
            return view;
        }

        /// <summary>
        /// Transfer from  SubhireShortageViewModel to  SubhireShortageDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static  SubhireShortageDto TransferToDto(this  SubhireShortageViewModel model)
        {
             SubhireShortageDto dto = new  SubhireShortageDto()
            {
                ProjectId = model.ProjectId,
                ProjectName = model.ProjectName,
                EquipmentId = model.EquipmentId,
                EquipmentName = model.EquipmentName,
                PlannedQuantity = model.PlannedQuantity,
                OwnStockQuantity = model.OwnStockQuantity,
                SubhiredQuantity = model.SubhiredQuantity,
                ShortageQuantity = model.ShortageQuantity,
                Explanation = model.Explanation
                
            };

            return dto;
        }

        /// <summary>
        /// Transfer from  SubhireShortageDto to  SubhireShortageViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static  SubhireShortageViewModel TransferToViewModel(this  SubhireShortageDto dto)
        {
             SubhireShortageViewModel model = new  SubhireShortageViewModel()
            {
                 ProjectId = dto.ProjectId,
                 ProjectName = dto.ProjectName,
                 EquipmentId = dto.EquipmentId,
                 EquipmentName = dto.EquipmentName,
                 PlannedQuantity = dto.PlannedQuantity,
                 OwnStockQuantity = dto.OwnStockQuantity,
                 SubhiredQuantity = dto.SubhiredQuantity,
                 ShortageQuantity = dto.PlannedQuantity - dto.OwnStockQuantity - dto.SubhiredQuantity,
                 Explanation = dto.Explanation

             };

            return model;
        }
    }
}
