using Prorent24.BLL.Models.General.Folder;
using Prorent24.WebApp.Models.General.Folder;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.General
{
    internal static class FolderTransfer
    {
        /// <summary>
        /// Transfer from List<FolderDto> to List<FolderViewModel>
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<FolderViewModel> TransferToViewModel(this List<FolderDto> list)
        {
            List<FolderViewModel> viewModels = list.Select(x => new FolderViewModel()
            {
                Id = x.Id,
                ModuleType = x.ModuleType,
                Name = x.Name,
                Order = x.Order,
                ParentId = x.ParentId,
                Childs = x.Childs?.TransferToViewModel(),
                Selected = x.Selected
            }).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from FolderDto to FolderViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static FolderViewModel TransferToViewModel(this FolderDto dto)
        {
            FolderViewModel view = new FolderViewModel()
            {
                Id = dto.Id,
                ModuleType = dto.ModuleType,
                Name = dto.Name,
                Order = dto.Order,
                ParentId = dto.ParentId,
                Selected = dto.Selected
            };

            return view;
        }

        /// <summary>
        /// Transfer from FolderViewModel to FolderDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static FolderDto TransferToDtoModel(this FolderViewModel view)
        {
            FolderDto dto = new FolderDto()
            {
                Id = view.Id,
                ModuleType = view.ModuleType,
                Name = view.Name,
                Order = view.Order,
                ParentId = view.ParentId,
                Selected = view.Selected
            };

            return dto;
        }
    }
}
