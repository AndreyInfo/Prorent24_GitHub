using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.WebApp.Models.Configuration.Settings;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.Configurations.Settings
{
    public static class ProjectTypeTransfer
    {
        /// <summary>
        /// Transfer from List<ProjectTypeDto> to List<ProjectTypeViewModel> 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ProjectTypeViewModel> TransferToListDto(this List<ProjectTypeDto> entities)
        {
            List<ProjectTypeViewModel> models = entities.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }

        /// <summary>
        /// Transfer from ProjectTypeViewModel to ProjectTypeDto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ProjectTypeDto TransferToDto(this ProjectTypeViewModel model)
        {
            ProjectTypeDto dto = new ProjectTypeDto()
            {
                Id = model.Id,
                Name = model.Name,
                Color = model.Color,
                ContractTemplateId = model.ContractTemplateId,
                DefaultAdditionalConditionId = model.DefaultAdditionalConditionId,
                InvoiceMommentId = model.InvoiceMommentId,
                InvoiceTemplateId = model.InvoiceTemplateId,
                LetterheadTemplateId = model.LetterheadTemplateId,
                QuotationTemplateId = model.QuotationTemplateId,
                PackingSlipTemplateId = model.PackingSlipTemplateId
            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectTypeDto to ProjectTypeDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectTypeViewModel TransferToViewModel(this ProjectTypeDto dto)
        {
            ProjectTypeViewModel entity = new ProjectTypeViewModel()
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
                PackingSlipTemplateId = dto.PackingSlipTemplateId
            };

            return entity;
        }


        public static ProjectTypeDefaultDto TransferToDto(this ProjectTypeDefaultViewModel model) {
            ProjectTypeDefaultDto dto = new ProjectTypeDefaultDto()
            {
                Id = model.Id
            };

            return dto;
        }

        public static ProjectTypeDefaultViewModel TransferToViewModel(this ProjectTypeDefaultDto dto) {
            ProjectTypeDefaultViewModel model = new ProjectTypeDefaultViewModel()
            {
                Id = dto.Id
            };

            return model;
        }
    }
}
