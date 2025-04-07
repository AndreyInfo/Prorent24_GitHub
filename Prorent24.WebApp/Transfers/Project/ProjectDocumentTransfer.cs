using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Project
{
    public static class ProjectDocumentTransfer
    {
        /// <summary>
        /// Transfer from ProjectDocumentViewModel to ProjectDocumentDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectDocumentViewModel TransferToViewModel(this ProjectDocumentDto dto)
        {
            ProjectDocumentViewModel model = new ProjectDocumentViewModel()
            {
                Id = dto.Id,
                Description = dto.Description,
                DocumentType = dto.DocumentType,
                Name = dto.Name,
                GeneratedOn = dto.GeneratedOn,
                Date = dto.CreationDate
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<ProjectDocumentDto> to List<ProjectDocumentViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectDocumentViewModel> TransferToViewModel(this List<ProjectDocumentDto> dtos)
        {
            List<ProjectDocumentViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }

        /// <summary>
        /// Transfer from ProjectDocumentViewModel to ProjectDocumentDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectDocumentGroupViewModel TransferToViewModel(this ProjectDocumentGroupDto dto)
        {
            ProjectDocumentGroupViewModel model = new ProjectDocumentGroupViewModel()
            {
               Type = dto.Type,
               Data = dto.Data.TransferToViewModel()
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<ProjectDocumentDto> to List<ProjectDocumentViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectDocumentGroupViewModel> TransferToViewModel(this List<ProjectDocumentGroupDto> dtos)
        {
            List<ProjectDocumentGroupViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
