using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Equipment
{
    public static class EquipmentWebShopTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentWebShopViewModel> to List<EquipmentWebShopDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<EquipmentWebShopDto> TransferToListDto(this List<EquipmentWebShopViewModel> models)
        {
            List<EquipmentWebShopDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from EquipmentWebShopDto to EquipmentWebShopViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentWebShopDto TransferToDto(this EquipmentWebShopViewModel model)
        {
            EquipmentWebShopDto dto = new EquipmentWebShopDto()
            {
                InWebshop = model.InWebshop,
                ShortDescription = model.ShortDescription,
                LongDescription = model.LongDescription,
                SEOTitle = model.SEOTitle,
                SEODescription = model.SEODescription,
                SEOKeyword = model.SEOKeyword,
                FeaturedProduct = model.FeaturedProduct
            };

            return dto;
        }

        /// <summary>
        /// Transfer from EquipmentWebShopViewModel to EquipmentWebShopDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentWebShopViewModel TransferToViewModel(this EquipmentWebShopDto dto)
        {
            EquipmentWebShopViewModel model = new EquipmentWebShopViewModel()
            {
                InWebshop = dto.InWebshop,
                ShortDescription = dto.ShortDescription,
                LongDescription = dto.LongDescription,
                SEOTitle = dto.SEOTitle,
                SEODescription = dto.SEODescription,
                SEOKeyword = dto.SEOKeyword,
                FeaturedProduct = dto.FeaturedProduct
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<EquipmentWebShopDto> to List<EquipmentWebShopViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<EquipmentWebShopViewModel> TransferToViewModel(this List<EquipmentWebShopDto> dtos)
        {
            List<EquipmentWebShopViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
