using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.Project;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Project;
using Prorent24.Entity.Project;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Project.AdditionalCost
{
    internal class ProjectAdditionalCostService : BaseService, IProjectAdditionalCostService
    {
        private readonly IProjectAdditionalCostStorage _projectAdditionalCostStorage;
        public ProjectAdditionalCostService(IHttpContextAccessor httpContextAccessor,
            IProjectAdditionalCostStorage projectAdditionalCostStorage,
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _projectAdditionalCostStorage = projectAdditionalCostStorage ?? throw new ArgumentNullException(nameof(projectAdditionalCostStorage));
        }

        public async Task<ProjectAdditionalCostDto> Create(ProjectAdditionalCostDto model)
        {
            ProjectAdditionalCostEntity transferEntity = model.TransferToEntity();
            ProjectAdditionalCostEntity entityCreate = await _projectAdditionalCostStorage.Create(transferEntity);
            ProjectAdditionalCostEntity entity = await _projectAdditionalCostStorage.GetById(entityCreate.Id);
            ProjectAdditionalCostDto dto = entity?.TransferToDto();
            return dto;

        }
        public async Task<bool> Delete(int id)
        {
            bool result = await _projectAdditionalCostStorage.Delete(id);
            return result;
        }
        public async Task<ProjectAdditionalCostDto> GetById(int id)
        {
            ProjectAdditionalCostEntity entity = await _projectAdditionalCostStorage.GetById(id);
            ProjectAdditionalCostDto dto = entity?.TransferToDto();
            return dto;
        }

        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseListDto> GetPagedList(int projectId)
        {
            IPagedList<ProjectAdditionalCostEntity> pagedList = await _projectAdditionalCostStorage.GetAllByForeignId(projectId);
            List<ProjectAdditionalCostEntity> listEntities = pagedList.Items.ToList();
            List<ProjectAdditionalCostDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<ProjectAdditionalCostDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.ProjectAdditionalCostEntity
            };
        }

        public async Task<List<ProjectAdditionalCostDto>> GetAdditionalCosts(int projectId)
        {
            IPagedList<ProjectAdditionalCostEntity> pagedList = await _projectAdditionalCostStorage.GetAllByForeignId(projectId);
            List<ProjectAdditionalCostEntity> listEntities = pagedList.Items.ToList();
            List<ProjectAdditionalCostDto> listDtos = listEntities.TransferToListDto();
            return listDtos;
        }

        public async Task<ProjectAdditionalCostDto> Update(ProjectAdditionalCostDto model)
        {
            ProjectAdditionalCostEntity transferEntity = model.TransferToEntity();
            ProjectAdditionalCostEntity entityUpdate = await _projectAdditionalCostStorage.Update(transferEntity);
            ProjectAdditionalCostEntity entity = await _projectAdditionalCostStorage.GetById(entityUpdate.Id);
            ProjectAdditionalCostDto dto = entity?.TransferToDto();
            return dto;
        }
    }
}
