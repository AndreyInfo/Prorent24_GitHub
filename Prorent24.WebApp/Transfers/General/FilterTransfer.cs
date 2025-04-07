using Prorent24.BLL.Models.General.Folder;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.General.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using Prorent24.BLL.Models.General.Preset;
using Prorent24.WebApp.Models.General.Perset;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.WebApp.Models.General.Tag;
using Newtonsoft.Json;
using Prorent24.Common.Models.Filters;
using Prorent24.WebApp.Models.General.Period;
using Prorent24.BLL.Models.General.Period;

namespace Prorent24.WebApp.Transfers.General
{
    internal static class FilterTransfer
    {
        /// <summary>
        /// Transfer from FilterListDto to FilterListViewModel
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<FilterListViewModel> TransferToViewModel(this List<FilterListDto> list)
        {
            List<FilterListViewModel> viewModels = list.Select(x => new FilterListViewModel()
            {
                FilterType = x.FilterType,
                Data = x.GetDataByFilterTypeForView(x.FilterType)
            }).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from FilterListDto to FilterListViewModel
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<FilterListDto> TransferToDtoModel(this List<FilterListViewModel> list)
        {
            List<FilterListDto> viewModels = list.Select(x => new FilterListDto()
            {
                FilterType = x.FilterType,
                Data = x.GetDataByFilterTypeForDto(x.FilterType)
            }).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from FilterDto to FilterViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SavedFilterViewModel TransferToViewModel(this SavedFilterDto dto)
        {
            SavedFilterViewModel view = new SavedFilterViewModel()
            {
                Id = dto.Id,
                Text = dto.Text
            };

            return view;
        }

        /// <summary>
        /// Transfer from List<FilterDto> to List<FilterViewModel>
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<SavedFilterViewModel> TransferToViewModel(this List<SavedFilterDto> list)
        {
            List<SavedFilterViewModel> viewModels = list.Select(x => new SavedFilterViewModel()
            {
                Id = x.Id,
                Text = x.Text
            }).ToList();

            return viewModels;
        }



        /// <summary> 
        /// Transfer from FilterViewModel to FilterDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static SavedFilterDto TransferToDtoModel(this SavedFilterViewModel view)
        {
            SavedFilterDto dto = new SavedFilterDto()
            {
                Id = view.Id,
                ModuleType = view.ModuleType,
                FilterGroupType = view.FilterGroupType,
                FilterType = view.FilterType,
                Text = view.Text,
                Value = view.Value.TransferToDtoModel()
            };

            return dto;
        }

        /// <summary> FilterValueViewModel
        /// Transfer from FilterValueViewModel to FilterValueDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static SavedFilterValueDto TransferToDtoModel(this SavedFilterValueViewModel view)
        {
            SavedFilterValueDto dto = new SavedFilterValueDto()
            {
                Property = view.Property,
                CrewMemberId = view.CrewMemberId,
                Date = view.Date,
                UserRoleId = view.UserRoleId
            };

            return dto;
        }

        public static List<SelectedFilter> TransferToDtoModel(this List<SelectedFilterViewModel> view)
        {
            List<SelectedFilter> dtos = view?.Select(x => new SelectedFilter()
            {
                FilterType = x.FilterType.ToString(),
                SelectedAll = x.SelectedAll,
                Values = x.Values
            }).ToList();

            return dtos;
        }

        #region Private

        private static object GetDataByFilterTypeForView(this object data, FilterEnum filterType)
        {
            FilterListDto filter = (FilterListDto)data;

            switch (filterType)
            {
                case FilterEnum.Presets:
                    {

                        List<PresetDto> listDto = (List<PresetDto>)filter.Data;
                        List<PresetViewModel> listView = listDto.TransferToViewModel();
                        return listView;
                    }
                case FilterEnum.Period:
                    {

                        PeriodDto dto = (PeriodDto)filter.Data;
                        PeriodViewModel view = dto.TransferToViewModel();
                        return view;
                    }
                case FilterEnum.Folders:
                    {
                        List<FolderDto> listDto =  (List<FolderDto>)filter.Data;
                        List<FolderViewModel> listView = listDto.TransferToViewModel();
                        return listView;
                    }
                case FilterEnum.Tags:
                    {
                        List<TagDto> listDto = (List<TagDto>)filter.Data;
                        List<TagViewModel> listView = listDto.TransferToViewModel();
                        return listView;
                    }
                case FilterEnum.AddedFilters:
                    {
                        List<SavedFilterDto> listDto = (List<SavedFilterDto>)filter.Data;
                        List<SavedFilterViewModel> listView = listDto.TransferToViewModel();
                        return listView;
                    }
            }

            return null;
        }

        private static object GetDataByFilterTypeForDto(this object data, FilterEnum filterType)
        {
            FilterListViewModel filter = (FilterListViewModel)data;

            switch (filterType)
            {
                case FilterEnum.Presets:
                    {
                        List<PresetDto> listDto = JsonConvert.DeserializeObject<List<PresetDto>>(filter.Data.ToString());
                        return listDto;
                    }
                case FilterEnum.Folders:
                    {
                        List<FolderDto> listDto = JsonConvert.DeserializeObject<List<FolderDto>>(filter.Data.ToString());
                        return listDto;
                    }
                case FilterEnum.Tags:
                    {
                        List<TagDto> listDto = JsonConvert.DeserializeObject<List<TagDto>>(filter.Data.ToString());
                        return listDto;
                    }
                case FilterEnum.AddedFilters:
                    {
                        List<SavedFilterDto> listDto = JsonConvert.DeserializeObject<List<SavedFilterDto>>(filter.Data.ToString());
                        return listDto;
                    }
            }

            return null;
        }

        #endregion
    }
}
