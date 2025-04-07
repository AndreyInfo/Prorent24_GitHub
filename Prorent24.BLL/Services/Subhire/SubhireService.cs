using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Builders;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Models.Subhire;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.BLL.Transfers.Project;
using Prorent24.BLL.Transfers.Subhire;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Contact;
using Prorent24.DAL.Storages.Equipment;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Project;
using Prorent24.DAL.Storages.Subhire;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.Project;
using Prorent24.Entity.Subhire;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Subhire
{
    internal class SubhireService : BaseService, ISubhireService
    {
        private readonly ISubhireStorage _subhireStorage;
        private readonly IProjectStorage _projectStorage;
        private readonly IEquipmentStorage _equipmentStorage;
        private readonly ISubhireEquipmentGroupStorage _subhireEquipmentGroupStorage;
        private readonly IContactPersonStorage _contactPersonStorage;
        public SubhireService(IHttpContextAccessor httpContextAccessor, IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage, ISubhireStorage subhireStorage, IProjectStorage projectStorage, 
            IEquipmentStorage equipmentStorage, ISubhireEquipmentGroupStorage subhireEquipmentGroupStorage,
            IContactPersonStorage contactPersonStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _subhireStorage = subhireStorage;
            _projectStorage = projectStorage;
            _equipmentStorage = equipmentStorage;
            _subhireEquipmentGroupStorage = subhireEquipmentGroupStorage;
            _contactPersonStorage = contactPersonStorage;

        }

        public async Task<SubhireDto> Create(SubhireDto model)
        {
            SubhireEntity transferEntity = model.TransferToEntity();
            transferEntity.Number = string.IsNullOrWhiteSpace(transferEntity.Number) ? GeneratorExtention.GenerateUniqueCode() : transferEntity.Number;
            if (transferEntity.LocationContactPersonId != null)
            {
                transferEntity.LocationContactId = (await _contactPersonStorage.GetById(transferEntity.LocationContactPersonId)).ContactId;
            }
            if (transferEntity.SupplierContactPersonId != null)
            {
                transferEntity.SupplierContactId = (await _contactPersonStorage.GetById(transferEntity.SupplierContactPersonId)).ContactId;
            }
            SubhireEntity entity = await _subhireStorage.Create(transferEntity);
            SubhireDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<SubhireDto> SaveFromShortage(int? subhireId, List<ShortageEquipmentDto> equipments, DateTime? from, DateTime? to)
        {
            List<EquipmentEntity> distinctEquipment = await _equipmentStorage.GetByIds(equipments.Select(x => x.EquipmentId).ToList());
            List<int> projectIds = equipments.Select(y=>y.ProjectId).Distinct().ToList();
            if (subhireId == null)
            {
                SubhireEntity subhire = new SubhireEntity()
                {
                    Name = "Субаренда по проекту",
                    UsagePeriodStart = from,
                    UsagePeriodEnd = to,
                    PlanningPeriodStart = (DateTime)from,
                    PlanningPeriodEnd = (DateTime)to
                };
                SubhireEntity subhireEntity = await _subhireStorage.Create(subhire);
                SubhireEquipmentGroupEntity group = new SubhireEquipmentGroupEntity()
                {
                    SubhireId = subhireEntity.Id,
                    Name = "Группа", //TODO зробити різні групи по проектам еквіпментів
                    Equipments = equipments.Select(x => new SubhireEquipmentEntity()
                    {
                        EquipmentId = x.EquipmentId,
                        Name = distinctEquipment.Where(y => y.Id == x.EquipmentId).FirstOrDefault().Name,
                        Quantity = x.Quantity,
                        Price = distinctEquipment.Where(y => y.Id == x.EquipmentId).FirstOrDefault().SubhirePrice ?? 0,
                        SubhireId = subhireEntity.Id,
                    }).ToList()
                };
                SubhireEquipmentGroupEntity entity = await _subhireEquipmentGroupStorage.Create(group);

                List<SubhireProjectEntity> subhireProjects = projectIds.Select(x => new SubhireProjectEntity()
                {
                    ProjectId = x,
                    SubhireId = subhireEntity.Id
                }).ToList();

                List<SubhireProjectEntity> subhireProjEntities = await _subhireStorage.CreateSubhireProjects(subhireProjects);

                return subhireEntity.TransferToDto();
            }
            else
            {
                SubhireEntity entity = await _subhireStorage.GetById(subhireId);
                bool needUpdate = false;
                if (entity.PlanningPeriodStart >= from)
                {
                    entity.PlanningPeriodStart = (DateTime)from;
                    needUpdate = true;
                }
                if (entity.PlanningPeriodStart >= from)
                {
                    entity.UsagePeriodStart = from;
                    needUpdate = true;
                }
                if (entity.PlanningPeriodEnd <= to)
                {
                    entity.PlanningPeriodEnd = (DateTime)to;
                    needUpdate = true;
                }
                if (entity.UsagePeriodEnd <= to)
                {
                    entity.UsagePeriodEnd = to;
                    needUpdate = true;
                }
                if (needUpdate)
                {
                    SubhireEntity entityUpdate = await _subhireStorage.Update(entity);
                }
                SubhireEquipmentGroupEntity group = new SubhireEquipmentGroupEntity()
                {
                    SubhireId = entity.Id,
                    Name = "Группа",
                    Equipments = equipments.Select(x => new SubhireEquipmentEntity()
                    {
                        EquipmentId = x.EquipmentId,
                        Name = distinctEquipment.Where(y => y.Id == x.EquipmentId).FirstOrDefault().Name,
                        Quantity = x.Quantity,
                        Price = distinctEquipment.Where(y => y.Id == x.EquipmentId).FirstOrDefault().SubhirePrice ?? 0,
                        SubhireId = entity.Id
                    }).ToList()
                };
                SubhireEquipmentGroupEntity groupEntity = await _subhireEquipmentGroupStorage.Create(group);
                List<int> existingProj = await _subhireStorage.GetSubhireProjects(entity.Id);
                List<SubhireProjectEntity> subhireProjects = projectIds.Where(x => !existingProj.Contains(x)).Select(x => new SubhireProjectEntity()
                {
                    ProjectId = x,
                    SubhireId = entity.Id
                }).ToList();

                List<SubhireProjectEntity> subhireProjEntities = await _subhireStorage.CreateSubhireProjects(subhireProjects);

                return entity.TransferToDto();
            }
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _subhireStorage.Delete(id);
            return result;
        }

        public async Task<SubhireDto> GetById(int id)
        {
            SubhireEntity entity = await _subhireStorage.GetById(id);
            SubhireDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(int id)
        {
            SubhireEntity entity = await _subhireStorage.GetById(id);
            SubhireDto dto = entity?.TransferToDto();
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<SubhireDto>(EntityEnum.SubhireEntity);

            return moduleDetails;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            string searchText = string.Empty;
            IQueryable<SubhireEntity> queryableEntity = _subhireStorage.QueryableEntity.CreateFilterForSubhireEntity(filterList, out searchText);
            IPagedList<SubhireEntity> pagedList = await _subhireStorage.GetAllByFilter(queryableEntity, searchText);
            
            List<SubhireEntity> listEntities = pagedList.Items.ToList();
            List<SubhireDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<SubhireDto>(await GetUserColumns(EntityEnum.SubhireEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.SubhireEntity
            };
        }

        public async Task<List<ModuleDetailDto>> GetShortageDetails(int projectId, int equipmentId, int[] subhireIds)
        {
            ProjectEntity projectEntity = await _projectStorage.GetById(projectId);
            ProjectDto project = projectEntity.TransferToDto();
            EquipmentEntity equipmentEntity = await _equipmentStorage.GetById(equipmentId);
            EquipmentDto equipment = equipmentEntity.TransferToDto();
            List<SubhireEntity> subhireEntities = await _subhireStorage.GetByIds(subhireIds);
            List<SubhireDto> subhires = subhireEntities.TransferToListDto();
            SubhireShortageDetailDto shortageDetailDto = new SubhireShortageDetailDto()
            {
                Project = project,
                Equipment = equipment,
                Subhires = subhires
            };

            List<ModuleDetailDto> moduleDetails = shortageDetailDto.CreateDetails<SubhireShortageDetailDto>(EntityEnum.SubhireShortageEntity);
            return moduleDetails;
        }

        public async Task<SubhireListDto> GetSubhiresByProject(List<int> projectIds)
        {
            List<ProjectEntity> projects = await _projectStorage.GetByIds(projectIds);
            List<SubhireEntity> entities = await _subhireStorage.GetByProjects(projectIds);
            DateTime? dateFrom = projects.Min(x => x.Times.Where(y => y.DefaultUsagePeriod).FirstOrDefault()?.From);
            DateTime? dateTo = projects.Max(x => x.Times.Where(y => y.DefaultUsagePeriod).FirstOrDefault()?.Until);
            SubhireListDto subhireListDto = new SubhireListDto()
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                Subhires = entities.Select(x => new SubhireShortDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    SupplierName = x.SupplierContact?.CompanyName,
                    Status = x.Status,
                    PlanningPeriodStart = x.PlanningPeriodStart,
                    PlanningPeriodEnd = x.PlanningPeriodEnd,
                    IsAlmostInPeriod = x.PlanningPeriodStart >= dateFrom && x.PlanningPeriodEnd <= dateTo
                }).ToList()
            };

            return subhireListDto;
        }

        public async Task<SubhireDto> Update(SubhireDto model)
        {
            SubhireEntity transferEntity = model.TransferToEntity();
            transferEntity.Number = string.IsNullOrWhiteSpace(transferEntity.Number) ? GeneratorExtention.GenerateUniqueCode() : transferEntity.Number;
            if (transferEntity.LocationContactPersonId != null)
            {
                transferEntity.LocationContactId = (await _contactPersonStorage.GetById(transferEntity.LocationContactPersonId)).ContactId;
            }
            if (transferEntity.SupplierContactPersonId != null)
            {
                transferEntity.SupplierContactId = (await _contactPersonStorage.GetById(transferEntity.SupplierContactPersonId)).ContactId;
            }
            SubhireEntity entity = await _subhireStorage.Update(transferEntity);
            SubhireDto dto = entity.TransferToDto();
            return dto;
        }
    }
}
