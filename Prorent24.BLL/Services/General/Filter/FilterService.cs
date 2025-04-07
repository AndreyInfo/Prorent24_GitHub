using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Models.General.Folder;
using Prorent24.BLL.Models.General.Period;
using Prorent24.BLL.Models.General.Preset;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Services.General.Folder;
using Prorent24.BLL.Services.General.Tag;
using Prorent24.BLL.Transfers.General;
using Prorent24.Common.Models.Filters;
using Prorent24.DAL.Storages.General.Filter;
using Prorent24.Entity.General;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Filter
{
    internal class FilterService : IFilterService
    {
        private readonly IPresetService _presetService;
        private readonly ISavedFilterStorage _savedFilterStorage;
        private readonly IFolderService _folderService;
        private readonly ITagService _tagService;

        public FilterService(IPresetService presetService,
                             ISavedFilterStorage savedFilterStorage,
                             IFolderService folderService,
                             ITagService tagService)
        {
            _presetService = presetService;
            _savedFilterStorage = savedFilterStorage;
            _folderService = folderService;
            _tagService = tagService;
        }

        /// <summary>
        /// Get list filters by MenuType
        /// </summary>
        /// <param name="menuType"></param>
        /// <returns></returns>
        public async Task<List<FilterListDto>> GetListFilters(ModuleTypeEnum moduleType)
        {
            List<FilterListDto> list = new List<FilterListDto>();

            switch (moduleType)
            {
                case ModuleTypeEnum.Equipment:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));

                        list.Add(NewFilter(FilterEnum.Folders, await _folderService.GetFolders(moduleType)));

                        list.Add(NewFilter(FilterEnum.Tags, await _tagService.GetTags(moduleType)));

                        break;
                    }
                case ModuleTypeEnum.Contacts:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));
                        
                        list.Add(NewFilter(FilterEnum.Folders, await _folderService.GetFolders(moduleType)));

                        list.Add(NewFilter(FilterEnum.Tags, await _tagService.GetTags(moduleType)));
                        
                        break;
                    }
                case ModuleTypeEnum.CrewMembers:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));
                        
                        list.Add(NewFilter(FilterEnum.Folders, await _folderService.GetFolders(moduleType)));

                        list.Add(NewFilter(FilterEnum.Tags, await _tagService.GetTags(moduleType)));

                        break;
                    }
                case ModuleTypeEnum.Tasks:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));

                        list.Add(NewFilter(FilterEnum.Period, new PeriodDto()));

                        list.Add(NewFilter(FilterEnum.Tags, await _tagService.GetTags(moduleType)));

                        break;
                    }
                case ModuleTypeEnum.TimeRegistration:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));

                        list.Add(NewFilter(FilterEnum.Period, new PeriodDto()));

                        break;
                    }
                case ModuleTypeEnum.Vehicles:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));

                        list.Add(NewFilter(FilterEnum.Folders, await _folderService.GetFolders(moduleType)));

                        list.Add(NewFilter(FilterEnum.Tags, await _tagService.GetTags(moduleType)));

                        break;
                    }
                case ModuleTypeEnum.Projects:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));

                        list.Add(NewFilter(FilterEnum.Period, new PeriodDto()));

                        list.Add(NewFilter(FilterEnum.View, null));

                        list.Add(NewFilter(FilterEnum.Tags, await _tagService.GetTags(moduleType)));

                        break;

                    }
                case ModuleTypeEnum.Subhire:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));

                        list.Add(NewFilter(FilterEnum.Period, new PeriodDto()));
                        
                        list.Add(NewFilter(FilterEnum.Tags, await _tagService.GetTags(moduleType)));

                        break;

                    }
                case ModuleTypeEnum.Maintenance:
                case ModuleTypeEnum.Repairs:
                case ModuleTypeEnum.Inspections:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));

                        list.Add(NewFilter(FilterEnum.Tags, await _tagService.GetTags(moduleType)));

                        break;
                    }
                case ModuleTypeEnum.CrewPlanner:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));

                        list.Add(NewFilter(FilterEnum.Period, new PeriodDto()));

                        break;
                    }
                case ModuleTypeEnum.Invoices:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));

                        list.Add(NewFilter(FilterEnum.Period, new PeriodDto()));

                        list.Add(NewFilter(FilterEnum.Tags, await _tagService.GetTags(moduleType)));

                        break;
                    }
                case ModuleTypeEnum.SubhireShortage:
                    {
                        list.Add(NewFilter(FilterEnum.SearchText, null));

                        list.Add(NewFilter(FilterEnum.Period, new PeriodDto()));

                        break;

                    }
            }

            return list;
        }

        /// <summary>
        /// Get filters by preset
        /// </summary>
        /// <param name="presetId"></param>
        /// <returns></returns>
        public async Task<List<FilterListDto>> GetFiltersByPreset(int presetId)
        {
            PresetDto dto = await _presetService.GetPreset(presetId);
            if (dto.Filters?.Count > 0)
            {
                List<FilterListDto> filterLists = await GetListFilters(dto.ModuleType);
                filterLists.ForEach(item =>
                {
                    var filter = dto.Filters.FirstOrDefault(x => x.FilterType.ToFilterEnum() == item.FilterType);
                    if (filter != null)
                    {
                        if (filter.Values != null && filter.Values.Count > 0)
                        {
                            switch (filter.FilterType.ToFilterEnum())
                            {
                                case FilterEnum.Presets:
                                    {
                                        List<PresetDto> presetDtos = (List<PresetDto>)item.Data;
                                        presetDtos.ForEach(x =>
                                        {
                                            if (filter.Values.Any(f => Convert.ToInt32(f) == x.Id))
                                            {
                                                x.Selected = true;
                                            }
                                        });

                                        item.Data = presetDtos;
                                        break;
                                    }
                                case FilterEnum.Folders:
                                    {
                                        List<FolderDto> folderDtos = (List<FolderDto>)item.Data;
                                        folderDtos.ForEach(x =>
                                        {
                                            if (filter.Values.Any(f => Convert.ToInt32(f) == x.Id))
                                            {
                                                x.Selected = true;
                                            }

                                            x.Childs?.ForEach(ch =>
                                            {
                                                if (filter.Values.Any(f => Convert.ToInt32(f) == ch.Id))
                                                {
                                                    ch.Selected = true;
                                                }
                                            });

                                        });

                                        item.Data = folderDtos;
                                        break;
                                    }
                                case FilterEnum.Tags:
                                    {
                                        List<TagDto> tagDtos = (List<TagDto>)item.Data;
                                        tagDtos.ForEach(x =>
                                        {
                                            if (filter.Values.Any(f => Convert.ToInt32(f) == x.Id))
                                            {
                                                x.Selected = true;
                                            }
                                        });

                                        item.Data = tagDtos;
                                        break;
                                    }
                            }
                        }
                    }
                });

                return filterLists;
            }

            return new List<FilterListDto>();
        }

        /// <summary>
        /// Create new filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<SavedFilterDto> SaveFilter(SavedFilterDto filter)
        {
            SavedFilterEntity entity = await _savedFilterStorage.Create(filter.TransferToEntity());
            SavedFilterDto dto = entity.TransferToDto();
            return dto;
        }


        /// <summary>
        /// Delete filter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFilter(int id)
        {
            bool result = await _savedFilterStorage.Delete(id);
            return result;
        }

        private FilterListDto NewFilter(FilterEnum filterType, object data)
        {
            FilterListDto filter = new FilterListDto()
            {
                FilterType = filterType,
                Data = data
            };

            return filter;
        }

        private async Task<List<SavedFilterDto>> GetListSavedFilters(ModuleTypeEnum moduleType)
        {
            List<SavedFilterEntity> entities = await _savedFilterStorage.GetSavedFilters(moduleType);
            List<SavedFilterDto> listDtos = entities.TransferToListDto();
            return listDtos;

        }
    }
}
