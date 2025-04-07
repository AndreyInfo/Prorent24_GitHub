using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Enum.Directory;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Configuration.Settings
{
    public static class DocumentTemplateTransfer
    {
        /// <summary>
        /// Transfer from List<DocumentTemplateEntity> to List<DocumentTemplateDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<DocumentTemplateDto> TransferToListDto(this List<DocumentTemplateEntity> entities)
        {
            List<DocumentTemplateDto> documentTemplates = entities.Select(x => x.TransferToDto()).ToList();

            return documentTemplates;
        }

        /// <summary>
        /// Transfer from DocumentTemplateDto to DocumentTemplateEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DocumentTemplateDto TransferToDto(this DocumentTemplateEntity entity)
        {
            DocumentTemplateDto documentTemplate = new DocumentTemplateDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type,
                CountryId = entity.CountryId,
                LanguageId = entity.LanguageId,
                CountryName = entity.Country?.Locs?.FirstOrDefault(x => x.DirectoryId == entity.Country.Id)?.Name,
                LanguageName = entity.Language?.Locs?.FirstOrDefault(x => x.DirectoryId == entity.Language.Id)?.Name,
                Html = entity.Html,
                CSS = entity.CSS,
                HeaderText = entity.HeaderText,
                FooterText = entity.FooterText,
                IsSystemTemplate = entity.IsSystemTemplate,
                IsHidden = entity.IsHidden,
                Blocks = entity.Blocks?.ToList().TransferToDtos()
            };

            return documentTemplate;
        }

        /// <summary>
        /// Transfer from DocumentTemplateEntity to DocumentTemplateDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static DocumentTemplateEntity TransferToEntity(this DocumentTemplateDto dto)
        {
            DocumentTemplateEntity documentTemplate = new DocumentTemplateEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Type = dto.Type,
                CountryId = dto.CountryId,
                Html = dto.Html,
                CSS = dto.CSS,
                HeaderText = dto.HeaderText,
                FooterText = dto.FooterText,
                IsSystemTemplate = dto.IsSystemTemplate,
                IsHidden = dto.IsHidden,
                LanguageId = dto.LanguageId
            };

            return documentTemplate;
        }

        /// <summary>
        /// Transfer from DocumentTemplateBlockDto to DocumentTemplateBlockEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DocumentTemplateBlockDto TransferToDto(this DocumentTemplateBlockEntity entity) {
            DocumentTemplateBlockDto dto = new DocumentTemplateBlockDto()
            {
                BlockConfigurationJSON = entity.BlockConfigurationJSON,
                Id = entity.Id,
                DocumentTemplateId = entity.DocumentTemplateId,
                Name = entity.Name,
                Order = entity.Order,
                Type = entity.Type
            };

            return dto;
        }

        public static List<DocumentTemplateBlockDto> TransferToDtos(this List<DocumentTemplateBlockEntity> entity)
        {
            List<DocumentTemplateBlockDto> dtos = entity.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        public static DocumentTemplateBlockEntity TransferToEntity(this DocumentTemplateBlockDto dto)
        {
            DocumentTemplateBlockEntity entity = new DocumentTemplateBlockEntity()
            {
                BlockConfigurationJSON = dto.BlockConfigurationJSON,
                Id = dto.Id,
                DocumentTemplateId = dto.DocumentTemplateId,
                Name = dto.Name,
                Order = dto.Order,
                Type = dto.Type
            };

            return entity;
        }
    }
}
