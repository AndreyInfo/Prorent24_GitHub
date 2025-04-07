using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Models.General.Preset;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.General.Perset;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.General
{
    internal static class PresetTransfer
    {
        /// <summary>
        /// Transfer from List<PresetDto> to List<PresetViewModel>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<PresetViewModel> TransferToViewModel(this List<PresetDto> list)
        {
            List<PresetViewModel> viewModels = list.Select(x => new PresetViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                IsDefault = x.IsDefault
            }).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from PresetDto to PresetViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static PresetViewModel TransferToViewModel(this PresetDto dto)
        {
            PresetViewModel view = new PresetViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                ModuleType = dto.ModuleType,
                IsDefault = dto.IsDefault
            };

            return view;
        }

        /// <summary>
        /// Transfer from PresetViewModel to PresetDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static PresetDto TransferToDtoModel(this PresetViewModel view)
        {
            PresetDto dto = new PresetDto()
            {
                Id = view.Id,
                Name = view.Name,
                ModuleType = view.ModuleType,
                Filters = view.Filters.TransferToDtoModel()
            };

            return dto;
        }

        #region Private

        //private static Dictionary<FilterEnum, SelectedPresetFilterViewModel> TransferToViewModel(this Dictionary<FilterEnum, SelectedFilter> dto)
        //{
        //    Dictionary<FilterEnum, SelectedPresetFilterViewModel> view = new Dictionary<FilterEnum, SelectedPresetFilterViewModel>();
        //    foreach (var item in dto)
        //    {
        //        view.Add(item.Key, item.Value.TransferToViewModel(item.Key));
        //    }

        //    return view;
        //}

        //private static SelectedPresetFilterViewModel TransferToViewModel(this SelectedFilter dto, FilterEnum filter)
        //{

        //    SelectedPresetFilterViewModel view = new SelectedPresetFilterViewModel()
        //    {
        //        SelectedAll = filter == FilterEnum.Tags ?  dto.SelectedAll : null,
        //        Values = dto.Values
        //    };

        //    return view;
        //}

        //private static Dictionary<FilterEnum, SelectedFilter> TransferToDtoModel(this Dictionary<FilterEnum, SelectedPresetFilterViewModel> view)
        //{
        //    Dictionary<FilterEnum, SelectedFilter> dto = new Dictionary<FilterEnum, SelectedFilter>();
        //    foreach (var item in view)
        //    {
        //        dto.Add(item.Key, item.Value.TransferToDtoModel());
        //    }

        //    return dto;
        //}

        //private static SelectedFilter TransferToDtoModel(this SelectedPresetFilterViewModel view)
        //{

        //    SelectedFilter dto = new SelectedFilter()
        //    {
        //        SelectedAll = view.SelectedAll.Value,
        //        Values = view.Values
        //    };

        //    return dto;
        //}

        #endregion

    }
}
