using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Entity.Configuration.Financial;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Configuration.Financial
{
    internal static class AdditionalConditionTransfer
    {
        /// <summary>
        /// Transfer from List<AdditionalConditionEntity> to List<AdditionalConditionDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<AdditionalConditionDto> TransferToListDto(this List<AdditionalConditionEntity> entities)
        {
            List<AdditionalConditionDto> additionalConditions = entities.Select(x => new AdditionalConditionDto()
            {
                Id = x.Id,
                Name = x.Name,
                Text = x.Text,
                CreationDate = x.CreationDate,
                LastModifiedDate = x.LastModifiedDate
            }).ToList();

            return additionalConditions;
        }

        /// <summary>
        /// Transfer from AdditionalConditionDto to AdditionalConditionEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static AdditionalConditionDto TransferToDto(this AdditionalConditionEntity entity)
        {
            AdditionalConditionDto additionalCondition = new AdditionalConditionDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Text = entity.Text,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return additionalCondition;
        }

        /// <summary>
        /// Transfer from AdditionalConditionEntity to AdditionalConditionDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static AdditionalConditionEntity TransferToEntity(this AdditionalConditionDto dto)
        {
            AdditionalConditionEntity additionalCondition = new AdditionalConditionEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Text = dto.Text,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return additionalCondition;
        }
    }
}
