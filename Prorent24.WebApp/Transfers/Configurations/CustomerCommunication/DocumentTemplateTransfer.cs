using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.WebApp.Models.Configuration.CustomerCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.CustomerCommunication
{
    public static class DocumentTemplateTransfer
    {

        public static List<DocumentTemplateViewModel> TransferToViewModels(this List<DocumentTemplateDto> dtos) {

            return dtos.Select(x => x.TransferToViewModel()).ToList();
        }

        /// <summary>
        /// Transfer from DocumentTemplateDto to DocumentTemplateViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static DocumentTemplateViewModel TransferToViewModel(this DocumentTemplateDto dto)
        {
            DocumentTemplateViewModel viewModel = new DocumentTemplateViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Type = dto.Type,
                TypeId = dto.Type,
                CountryId = dto.CountryId,
                Html = dto.Html,
                CSS = dto.CSS,
                HeaderText = dto.HeaderText,
                FooterText = dto.FooterText,
                IsSystemTemplate = dto.IsSystemTemplate,
                IsHidden = dto.IsHidden,
                LanguageId = dto.LanguageId
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from DocumentTemplateViewModel to DocumentTemplateDto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DocumentTemplateDto TransferToDto(this DocumentTemplateViewModel model)
        {
            DocumentTemplateDto dto = new DocumentTemplateDto()
            {
                Id = model.Id,
                Name = model.Name,
                //Type = model.Type,
                Type = model.TypeId,
                CountryId = model.CountryId,
                Html = model.Html,
                CSS = model.CSS,
                HeaderText = model.HeaderText,
                FooterText = model.FooterText,
                IsSystemTemplate = model.IsSystemTemplate,
                IsHidden = model.IsHidden,
                LanguageId = model.LanguageId
            };

            return dto;
        }

        /// <summary>
        /// Transfer from DocumentTemplateBlockViewModel to DocumentTemplateBlockDto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DocumentTemplateBlockDto TransferToDto(this DocumentTemplateBlockViewModel model)
        {
            DocumentTemplateBlockDto dto = new DocumentTemplateBlockDto()
            {
                Id = model.Id,
                Name = model.Name,
                //Type = model.Type,
                DocumentTemplateId = model.DocumentTemplateId,
                Order = model.Order,
                Type = model.Type,
                DisplayHeader = model.DisplayHeader
            };

            return dto;
        }

        /// <summary>
        /// Transfer from DocumentTemplateBlockDto to DocumentTemplateBlockViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DocumentTemplateBlockViewModel TransferToViewModel(this DocumentTemplateBlockDto dto)
        {
            DocumentTemplateBlockViewModel model = new DocumentTemplateBlockViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                //Type = model.Type,
                DocumentTemplateId = dto.DocumentTemplateId,
                Order = dto.Order,
                Type = dto.Type,
                DisplayHeader = dto.DisplayHeader
            };

            return model;
        }
    }
}
