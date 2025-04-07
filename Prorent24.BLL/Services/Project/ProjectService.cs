using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Builders;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.Project;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Contact;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Project;
using Prorent24.Entity.Project;
using Prorent24.Enum.Entity;
using Prorent24.Enum.Project;
using Prorent24.UnitOfWork.PagedList;


namespace Prorent24.BLL.Services.Project
{
    internal class ProjectService : BaseService, IProjectService
    {
        private readonly IProjectStorage _projectStorage;
        private readonly IContactPersonStorage _contactPersonStorage;
        private readonly IProjectTimeStorage _projectTimeStorage;
        private readonly IProjectCrewMemberStorage _projectCrewMemberStorage;

        public ProjectService(IHttpContextAccessor httpContextAccessor, IUserRoleStorage userRoleStorage,
            IProjectStorage projectStorage, IProjectTimeStorage projectTimeStorage,
            IProjectCrewMemberStorage projectCrewMemberStorage,
            IColumnStorage сolumnStorage, IContactPersonStorage contactPersonStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _projectStorage = projectStorage;
            _projectTimeStorage = projectTimeStorage;
            _projectCrewMemberStorage = projectCrewMemberStorage;
            _contactPersonStorage = contactPersonStorage;
        }

        #region Project
        public async Task<ProjectDto> Create(ProjectDto model)
        {
            ProjectEntity transferEntity = model.TransferToEntity();
            if(transferEntity.ClientContactPersonId != null)
            {
                transferEntity.ClientContactId = (await _contactPersonStorage.GetById(transferEntity.ClientContactPersonId)).ContactId;
            }
            if (transferEntity.LocationContactPersonId != null)
            {
                transferEntity.LocationContactId = (await _contactPersonStorage.GetById(transferEntity.LocationContactPersonId)).ContactId;
            }
            transferEntity.Number = string.IsNullOrWhiteSpace(transferEntity.Number) ? GeneratorExtention.GenerateUniqueCode() : transferEntity.Number;
            transferEntity.CrewMembers = model.CrewMembers.TransferToProjectVisibilityEntity();
            ProjectEntity entity = await _projectStorage.Create(transferEntity);
            ProjectEntity proj = await _projectStorage.GetById(entity.Id);
            return proj.TransferToDto();
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _projectStorage.Delete(id);
            return result;
        }


        public async Task<ProjectDto> GetById(int id)
        {
            ProjectEntity entity = await _projectStorage.GetById(id);
            ProjectDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(int id)
        {
            ProjectEntity entity = await _projectStorage.GetById(id);
            ProjectDto dto = entity?.TransferToDto();
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<ProjectDto>(EntityEnum.ProjectEntity);

            return moduleDetails;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            string searchText;
            IQueryable<ProjectEntity> queryableEntity = _projectStorage.QueryableEntity.CreateFilterForProjectEntity(filterList, out searchText);
            IPagedList<ProjectEntity> pagedList = await _projectStorage.GetAllByFilter(queryableEntity, searchText);
            List<ProjectEntity> listEntities = pagedList.Items.ToList();
            List<ProjectDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<ProjectDto>(await GetUserColumns(EntityEnum.ProjectEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.ProjectEntity
            };
        }

        public async Task<List<ProjectDto>> GeList()
        {
            IPagedList<ProjectEntity> pagedList = await _projectStorage.GetAllByFilter(_projectStorage.QueryableEntity);
            List<ProjectEntity> listEntities = pagedList.Items.ToList();
            List<ProjectDto> listDtos = listEntities.TransferToListDto();
            return listDtos;
        }

        public async Task<ProjectDto> Update(ProjectDto model)
        {
            ProjectEntity transferEntity = model.TransferToEntity();
            transferEntity.Number = string.IsNullOrWhiteSpace(transferEntity.Number) ? GeneratorExtention.GenerateUniqueCode() : transferEntity.Number;
            if (transferEntity.ClientContactPersonId != null)
            {
                transferEntity.ClientContactId = (await _contactPersonStorage.GetById(transferEntity.ClientContactPersonId)).ContactId;
            }
            if (transferEntity.LocationContactPersonId != null)
            {
                transferEntity.LocationContactId = (await _contactPersonStorage.GetById(transferEntity.LocationContactPersonId)).ContactId;
            }
            ProjectEntity entity = await _projectStorage.Update(transferEntity);
            ProjectDto dto = entity.TransferToDto();
            if (model.CrewMembers != null)
            {
                var isDeleted = await _projectCrewMemberStorage.DeleteAllByProjectId(dto.Id);

                List<ProjectVisibilityCrewMemberEntity> visibilityTransferEntities = model.CrewMembers.TransferToProjectVisibilityEntity();
                List<CrewMemberShortDto> crewMembersList = new List<CrewMemberShortDto>();

                foreach (var _entity in visibilityTransferEntities)
                {
                    _entity.ProjectId = entity.Id;
                    ProjectVisibilityCrewMemberEntity visibilityEntity = await _projectCrewMemberStorage.Create(_entity);
                    var transferedDto = visibilityEntity.TransferToDto();
                    crewMembersList.Add(transferedDto);
                }

                dto.CrewMembers = crewMembersList;
            }
            if (model.Times != null)
            {
                var isDeleted = await _projectTimeStorage.DeleteAllOtherByProjectId(dto.Id);

                List<ProjectTimeEntity> timeTransferEntities = model.Times.Where(x => x.DefaultPlanPeriod != true && x.DefaultUsagePeriod != true).ToList().TransferToListEntity();
                List<ProjectTimeDto> times = model.Times.Where(x => x.DefaultPlanPeriod == true || x.DefaultUsagePeriod == true).ToList();
                foreach (var _entity in timeTransferEntities)
                {
                    _entity.ProjectId = model.Id;
                    ProjectTimeEntity time = await _projectTimeStorage.Create(_entity);
                    var transferedDto = time.TransferToDto();
                    times.Add(transferedDto);
                }

                dto.Times = times;
            }
            ProjectEntity proj = await _projectStorage.GetById(dto.Id);
            return proj.TransferToDto();
        }

        public async Task ChangeStatus(int id, ProjectChangeStatusEnum status)
        {
            ProjectEntity entity = await _projectStorage.GetById(id);
            entity.Status = status.TransferToStatus();
            await _projectStorage.Update(entity);
        }

        public async Task SetStatus(int id, ProjectStatusEnum status)
        {
            ProjectEntity entity = await _projectStorage.GetById(id);
            entity.Status = status;
            await _projectStorage.Update(entity);
        }

        #endregion

        #region ProjectTime
        public async Task<ProjectTimeDto> CreateTime(ProjectTimeDto model)
        {
            ProjectTimeEntity transferEntity = model.TransferToEntity();
            ProjectTimeEntity entity = await _projectTimeStorage.Create(transferEntity);
            ProjectTimeDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<ProjectTimeDto> UpdateTime(ProjectTimeDto model)
        {
            ProjectTimeEntity transferEntity = model.TransferToEntity();
            ProjectTimeEntity entity = await _projectTimeStorage.Update(transferEntity);
            ProjectTimeDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> DeleteTime(int id)
        {
            bool result = await _projectTimeStorage.Delete(id);
            return result;
        }
        #endregion

    }
}
