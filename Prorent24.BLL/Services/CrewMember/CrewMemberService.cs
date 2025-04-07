using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prorent24.BLL.Builders;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Services.General.Folder;
using Prorent24.BLL.Services.General.Tag;
using Prorent24.BLL.Transfers.CrewMember;
using Prorent24.BLL.Transfers.General;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.Directory;
using Prorent24.DAL.Storages.CrewMember;
using Prorent24.DAL.Storages.General.Address;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Project;
using Prorent24.DAL.Storages.Tasks;
using Prorent24.Entity;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.Directory;
using Prorent24.Entity.General;
using Prorent24.Entity.Project;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.CrewMember
{
    internal class CrewMemberService : BaseService, ICrewMemberService
    {
        private readonly ICrewMemberStorage _crewMemberStorage;
        private readonly IAddressStorage _addressStorage;
        private readonly IProjectPlanningStorage _projectPlanningStorage;
        private readonly ITaskStorage _taskStorage;
        private readonly IDirectoryStorage _directoryStorage;

        public CrewMemberService(IHttpContextAccessor httpContextAccessor, IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage, ICrewMemberStorage crewMemberStorage,
            IAddressStorage addressStorage, IProjectPlanningStorage projectPlanningStorage,
            ITaskStorage taskStorage, IDirectoryStorage directoryStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _crewMemberStorage = crewMemberStorage;
            _addressStorage = addressStorage;
            _projectPlanningStorage = projectPlanningStorage;
            _taskStorage = taskStorage;
            _directoryStorage = directoryStorage;
        }
        public async Task<List<CrewMemberDto>> GetAll()
        {
            IPagedList<CrewMemberEntity> list = await _crewMemberStorage.GetAll();
            return list?.Items?.ToList()?.TransferToListDto();
        }
        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.CrewMembers, PermissionFieldEnum.DatabaseCrewMember);
            string searchText = String.Empty;
            IQueryable<CrewMemberEntity> queryableEntity = _crewMemberStorage.QueryableEntity.Include(x=>x.User).ThenInclude(x=>x.Roles).Include(x => x.Address).Include(x => x.DefaultRate).CreateFilterForCrewMemberEntity(filterList, out searchText);
            IPagedList<CrewMemberEntity> pagedList = await _crewMemberStorage.GetAllByFilter(queryableEntity, searchText);
            List<CrewMemberEntity> listEntities = pagedList.Items.ToList();

            string lang = this.GetCurrentLang();
            List<DirectoryEntity> countries = await _directoryStorage.GetAllByType(DirectoryTypeEnum.Country, lang);

            List<Role> roles = await _userRoleStorage.GetList();
            List<CrewMemberDto> listDtos = listEntities.TransferToListDto(permission, countries, roles);

            BaseGrid grid = listDtos.CreateGrid<CrewMemberDto>(await GetUserColumns(EntityEnum.CrewMemberEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.CrewMemberEntity,
                Delete = permission.Delete,
                Add = permission.Add,
                View = permission.View,
                Edit = permission.Edit
            };
        }

        public async Task<CrewMemberDto> GetById(string id)
        {
            CrewMemberEntity entity = await _crewMemberStorage.GetById(id);
            CrewMemberDto dto = entity != null ? entity.TransferToDto() : new CrewMemberDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(string id)
        {
            CrewMemberEntity entity = await _crewMemberStorage.GetById(id);
            if (entity != null)
            {
                entity.Tasks = await _taskStorage.GetAllByCrewMember(id);
            }
            CrewMemberDto dto = entity?.TransferToDto();
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<CrewMemberDto>(EntityEnum.CrewMemberEntity, GetUserId());

            return moduleDetails;
        }

        public async Task<CrewMemberDto> Create(CrewMemberDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.CrewMembers, PermissionFieldEnum.DatabaseCrewMember);

            if (permission.Add)
            {
                CrewMemberEntity transferEntity = model.TransferToEntity();
                CrewMemberEntity entity = await _crewMemberStorage.Create(transferEntity);
                CrewMemberDto dto = entity.TransferToDto();
                return dto;
            }
            else
            {
                return null;
            }
        }

        public async Task<CrewMemberDto> Update(CrewMemberDto model, string userId)
        {
            CrewMemberEntity transferEntity = model.TransferToEntity();

            AddressEntity addressEntity = transferEntity.Address;
            var crewMember = await this._crewMemberStorage.GetById(model.Id);
            bool ownUser = model.Id == userId;
            crewMember = crewMember.TransferFromEntity(transferEntity, ownUser);
            if (crewMember.AddressId == null)
            {
                var address = await this._addressStorage.Create(addressEntity);
                crewMember.AddressId = address.Id;

            }
            else
            {
                AddressEntity address = crewMember.Address?.TransferFromEntity(transferEntity.Address);
                address.Id = Convert.ToInt32(crewMember.AddressId);
            }
            CrewMemberEntity entity = await _crewMemberStorage.Update(crewMember);
            CrewMemberDto dto = (await this._crewMemberStorage.GetById(entity.UserId)).TransferToDto();
            return dto;
            //}
            // else
            //{
            //    return null;
            // }
        }

        public async Task<bool> Delete(string id)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.CrewMembers, PermissionFieldEnum.DatabaseCrewMember);
            if (permission.Delete)
            {
                bool result = await _crewMemberStorage.Delete(id);
                return result;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateDefaultRate(string id, int rateId)
        {
            CrewMemberEntity entity = await _crewMemberStorage.GetById(id);
            if (entity.DefaultRateId == null)
            {
                entity.DefaultRateId = rateId;
                entity = await _crewMemberStorage.Update(entity);
                return true;
            }

            return false;
        }

        public async Task<BaseListDto> GetCrewMwmberForProjectPlanning()
        {
            List<CrewMemberEntity> entities = await _crewMemberStorage.GetCrewMembers();
            List<ProjectPlanningEntity> projectPlnnings = await _projectPlanningStorage.GetAllByCrewMembers(entities.Select(x => x.UserId).Distinct().ToList());
            projectPlnnings = projectPlnnings.Where(x => x.Function != null && x.Function.Project != null).ToList();
            List<CrewMemberForProjectPlanningDto> listProjectPlanCrewMembers = entities.TransferToListForProjectPlanningDto();
            foreach (CrewMemberForProjectPlanningDto el in listProjectPlanCrewMembers)
            {

                foreach (ProjectPlanningEntity projPlan in projectPlnnings)
                {
                    if (el.Id == projPlan.CrewMemberId)
                    {
                        if (el.ProjectPeriods == null)
                        {
                            el.ProjectPeriods = new List<ProjectPlannindPeriod>();
                        }
                        el.ProjectPeriods.Add(new ProjectPlannindPeriod()
                        {
                            ProjectId = (int)projPlan.Function.ProjectId,
                            ProjectName = projPlan.Function.Project?.Name,
                            Status = projPlan.Function.Project.Status,
                            From = (DateTime)(projPlan.Function.ProjectFunctionGroup?.UseFrom != null ? projPlan.Function.ProjectFunctionGroup?.UseFrom : projPlan.Function.Project?.Times?.Where(x => x.DefaultUsagePeriod).FirstOrDefault().From),
                            To = (DateTime)(projPlan.Function.ProjectFunctionGroup?.UseUntil != null ? projPlan.Function.ProjectFunctionGroup?.UseUntil : projPlan.Function.Project?.Times?.Where(x => x.DefaultUsagePeriod).FirstOrDefault().Until)
                        });

                    }
                }

            }
            BaseGrid grid = listProjectPlanCrewMembers.CreateGrid<CrewMemberForProjectPlanningDto>();
            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.ProjectPlanningCrewmember
            };

        }


    }
}
