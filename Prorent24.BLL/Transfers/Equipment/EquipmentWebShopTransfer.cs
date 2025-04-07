using Prorent24.BLL.Models.Equipment;
using Prorent24.Entity.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Equipment
{
    public static class EquipmentWebShopTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentWebShopEntity> to List<EquipmentWebShopDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentWebShopDto> TransferToListDto(this List<EquipmentWebShopEntity> entities)
        {
            List<EquipmentWebShopDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from EquipmentWebShopDto to EquipmentWebShopEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EquipmentWebShopDto TransferToDto(this EquipmentWebShopEntity entity)
        {
            EquipmentWebShopDto dto = new EquipmentWebShopDto()
            {
                Id = entity.Id,
                EquipmentId = entity.EquipmentId,
                InWebshop = entity.InWebshop,
                ShortDescription = entity.ShortDescription,
                LongDescription = entity.LongDescription,
                SEOTitle = entity.SEOTitle,
                SEODescription = entity.SEODescription,
                SEOKeyword = entity.SEOKeyword,
                FeaturedProduct = entity.FeaturedProduct
            };

            return dto;
        }

        /// <summary>
        /// Transfer from EquipmentWebShopEntity to EquipmentWebShopDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentWebShopEntity TransferToEntity(this EquipmentWebShopDto dto)
        {
            EquipmentWebShopEntity entity = new EquipmentWebShopEntity()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                InWebshop = dto.InWebshop,
                ShortDescription = dto.ShortDescription,
                LongDescription = dto.LongDescription,
                SEOTitle = dto.SEOTitle,
                SEODescription = dto.SEODescription,
                SEOKeyword = dto.SEOKeyword,
                FeaturedProduct = dto.FeaturedProduct
            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<EquipmentWebShopDto> to List<EquipmentWebShopEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentWebShopEntity> TransferToViewModel(this List<EquipmentWebShopDto> dto)
        {
            List<EquipmentWebShopEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }
    }
}
