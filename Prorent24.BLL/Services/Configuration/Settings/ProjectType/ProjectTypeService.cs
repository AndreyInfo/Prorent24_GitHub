using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Settings.ProjectType;
using Prorent24.Entity.Configuration.Settings;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using Prorent24.BLL.Transfers.Configuration.Settings;
using Prorent24.DAL.Storages.Configuration.SystemSettings;
using Prorent24.Entity.Configuration;
using Prorent24.Enum.Configuration;
using System;
using Prorent24.BLL.Transfers.Configuration;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.Common.Models.Filters;

namespace Prorent24.BLL.Services.Configuration.Settings.ProjectType
{
    internal class ProjectTypeService : IProjectTypeService
    {
        private readonly IProjectTypeStorage _projectTypeStorage;
        private readonly ISystemSettingStorage _systemSettingStorage;

        public ProjectTypeService(IProjectTypeStorage projectTypeStorage, ISystemSettingStorage systemSettingStorage)
        {
            _projectTypeStorage = projectTypeStorage;
            _systemSettingStorage = systemSettingStorage;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<ProjectTypeEntity> pagedList = await _projectTypeStorage.GetAll();
            List<ProjectTypeEntity> listEntities = pagedList.Items.ToList();
            List<ProjectTypeDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<ProjectTypeDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.ProjectTypeEntity
            };
        }

        public async Task<ProjectTypeDto> GetById(int id)
        {
            ProjectTypeEntity entity = await _projectTypeStorage.GetById(id);
            ProjectTypeDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<ProjectTypeDto> Create(ProjectTypeDto model)
        {
            ProjectTypeEntity transferEntity = model.TransferToEntity();
            ProjectTypeEntity entity = await _projectTypeStorage.Create(transferEntity);
            ProjectTypeDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<ProjectTypeDto> Update(ProjectTypeDto model)
        {
            ProjectTypeEntity transferEntity = model.TransferToEntity();
            ProjectTypeEntity entity = await _projectTypeStorage.Update(transferEntity);
            ProjectTypeDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _projectTypeStorage.Delete(id);
            return result;
        }

        public async Task<ProjectTypeDefaultDto> GetProjectTypeByDefault()
        {
            SystemSettingEntity entity = await _systemSettingStorage.GetById(SystemSettingsTypeEnum.DefaultProjectType);

            if (entity != null)
            {
                return entity.TransferToDto<ProjectTypeDefaultDto>();
            }
            else
            {
                IPagedList<ProjectTypeEntity> pagedList = await _projectTypeStorage.GetAll();
                ProjectTypeEntity defEntity = pagedList.Items.FirstOrDefault();
                return new ProjectTypeDefaultDto() { Id = defEntity.Id };
            }

        }

        public async Task<ProjectTypeDefaultDto> UpdateProjectTypeByDefault(ProjectTypeDefaultDto defaultProjectType)
        {
            SystemSettingEntity transferEntity = defaultProjectType.TransferToEntity();

            transferEntity.Type = SystemSettingsTypeEnum.TimeAndLocations;
            transferEntity.LastModifiedDate = DateTime.UtcNow;

            SystemSettingEntity entity = await _systemSettingStorage.Update(transferEntity);
            ProjectTypeDefaultDto dto = entity.TransferToDto<ProjectTypeDefaultDto>();
            return dto;
        }

        public async Task<bool> DeleteMultiple(List<int> Values)
        {
            bool result = await _projectTypeStorage.Delete(Values);
            return result;
        }
    }
}
