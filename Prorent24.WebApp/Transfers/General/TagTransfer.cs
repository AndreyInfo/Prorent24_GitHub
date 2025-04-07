using Prorent24.BLL.Models.General.Tag;
using Prorent24.WebApp.Models.General.Tag;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.General
{
    internal static class TagTransfer
    {
        /// <summary>
        /// Transfer from List<TagViewModel> to List<TagDto>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<TagViewModel> TransferToViewModel(this List<TagDto> list)
        {
            List<TagViewModel> viewModels = list.Select(x => new TagViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Selected = x.Selected,
                DirectoryId = x.DirectoryId
            }).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from TagDto to TagViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TagViewModel TransferToViewModel(this TagDto dto)
        {
            TagViewModel view = new TagViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                BelongsTo = dto.Entity,
                ReferenceId = dto.ReferenceId,
                DirectoryId = dto.DirectoryId
            };

            return view;
        }

        /// <summary>
        /// Transfer from TagViewModel to TagDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static TagDto TransferToDtoModel(this TagViewModel view)
        {
            TagDto dto = new TagDto()
            {
                Id = view.Id,
                Name = view.Name,
                Entity = view.BelongsTo,
                ReferenceId = view.ReferenceId
            };

            return dto;
        }
    }
}
