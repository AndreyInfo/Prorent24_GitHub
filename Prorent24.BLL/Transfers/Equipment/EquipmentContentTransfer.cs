using Prorent24.BLL.Models.Equipment;
using Prorent24.Entity.Equipment;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Equipment
{
    public static class EquipmentAContentTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentContentEntity> to List<EquipmentContentDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentContentDto> TransferToListDto(this List<EquipmentContentEntity> entities)
        {
            List<EquipmentContentDto> EquipmentContentDtos = entities.Select(x => x.TransferToDto()).ToList();

            return EquipmentContentDtos;
        }

        /// <summary>
        /// Transfer from EquipmentContentDto to EquipmentContentEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EquipmentContentDto TransferToDto(this EquipmentContentEntity entity)
        {
            EquipmentContentDto EquipmentContentDto = new EquipmentContentDto()
            {
                Id = entity.Id,
                EquipmentId = entity.EquipmentId,
                ContentId = entity.ContentId,
                Content = entity.Content?.TransferToDto(),
                //ContentName = entity.Content?.Name,
                Quantity = entity.Quantity,
                UnfoldFinancialDocuments = entity.UnfoldFinancialDocuments,
                UnfoldPackingSlip = entity.UnfoldPackingSlip
            };

            return EquipmentContentDto;
        }

        /// <summary>
        /// Transfer from EquipmentContentEntity to EquipmentContentDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentContentEntity TransferToEntity(this EquipmentContentDto dto)
        {
            EquipmentContentEntity EquipmentContentEntity = new EquipmentContentEntity()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                ContentId = dto.ContentId,
                Quantity = dto.Quantity,
                UnfoldFinancialDocuments = dto.UnfoldFinancialDocuments,
                UnfoldPackingSlip = dto.UnfoldPackingSlip
            };
            return EquipmentContentEntity;
        }

        /// <summary>
        /// Transfer from List<EquipmentContentDto> to List<EquipmentContentEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentContentEntity> TransferToViewModel(this List<EquipmentContentDto> dto)
        {
            List<EquipmentContentEntity> EquipmentContentEntitys = dto.Select(x => x.TransferToEntity()).ToList();
            return EquipmentContentEntitys;
        }
    }
}
