using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.Entity.Configuration;
using Prorent24.Entity.Configuration.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.Settings
{
    public static class ProjectTypeTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectTypeEntity> to List<ProjectTypeDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectTypeDto> TransferToListDto(this List<ProjectTypeEntity> entities)
        {
            List<ProjectTypeDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from ProjectTypeEntity to ProjectTypeDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectTypeDto TransferToDto(this ProjectTypeEntity entity)
        {
            ProjectTypeDto dto = new ProjectTypeDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Color = entity.Color,
                ContractTemplateId = entity.ContractTemplateId,
                DefaultAdditionalConditionId = entity.DefaultAdditionalConditionId,
                InvoiceMommentId = entity.InvoiceMommentId,
                InvoiceTemplateId = entity.InvoiceTemplateId,
                LetterheadTemplateId = entity.LetterheadTemplateId,
                QuotationTemplateId = entity.QuotationTemplateId,
                PackingSlipTemplateId = entity.PackingSlipTemplateId,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectTypeDto to ProjectTypeEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectTypeEntity TransferToEntity(this ProjectTypeDto dto)
        {
            ProjectTypeEntity entity = new ProjectTypeEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Color = dto.Color,
                ContractTemplateId = dto.ContractTemplateId,
                DefaultAdditionalConditionId = dto.DefaultAdditionalConditionId,
                InvoiceMommentId = dto.InvoiceMommentId,
                InvoiceTemplateId = dto.InvoiceTemplateId,
                LetterheadTemplateId = dto.LetterheadTemplateId,
                QuotationTemplateId = dto.QuotationTemplateId,
                PackingSlipTemplateId = dto.PackingSlipTemplateId,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }


        /// <summary>
        /// Transfer from ProjectTypeDefaultDto to SystemSettingEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns type="SystemSettingEntity"></returns>
        public static SystemSettingEntity TransferToEntity(this ProjectTypeDefaultDto dto)
        {
            SystemSettingEntity customerCommmunication = new SystemSettingEntity();
            customerCommmunication.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            return customerCommmunication;
        }
    }
}
