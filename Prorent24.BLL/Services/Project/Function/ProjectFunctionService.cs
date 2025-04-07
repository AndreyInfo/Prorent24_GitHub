using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.Project;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.Common.Models.Warehouse;
using Prorent24.DAL.Storages.Project;
using Prorent24.Entity.Project;
using Prorent24.Enum.Entity;
using Prorent24.Enum.Project;

namespace Prorent24.BLL.Services.Project.Function
{
    internal class ProjectFunctionService : IProjectFunctionService
    {
        private readonly IProjectFunctionStorage _projectFunctionStorage;
        private readonly IProjectFunctionGroupStorage _projectFunctionGroupStorage;
        private readonly IProjectStorage _projectStorage;

        public ProjectFunctionService(IProjectFunctionStorage projectFunctionStorage, IProjectFunctionGroupStorage projectFunctionGroupStorage,
            IProjectStorage projectStorage)
        {
            _projectFunctionStorage = projectFunctionStorage;
            _projectFunctionGroupStorage = projectFunctionGroupStorage;
            _projectStorage = projectStorage;
        }

        public async Task<ProjectFunctionDto> CreateFunction(ProjectFunctionDto dto)
        {
            ProjectFunctionEntity transferEntity = dto.TransferToEntity();
            CalculateCostAndPrice(dto, ref transferEntity);
            ProjectFunctionEntity entity = await _projectFunctionStorage.Create(transferEntity);
            ProjectFunctionDto result = entity.TransferToDto();
            return result;
        }

        public async Task<ProjectFunctionGridDto> CreateFunctionForProject(ProjectFunctionDto dto, VatSchemeRateDto vatScheme)
        {
            ProjectFunctionEntity transferEntity = dto.TransferToEntity();
            if (transferEntity.ProjectId != null && transferEntity.Quantity == null)
            {
                transferEntity.Quantity = 1;
            }
            if (dto.ParentFunctionId != null)
            {
                ProjectFunctionEntity parentFunctionEntity = await _projectFunctionStorage.GetById(dto.ParentFunctionId);
                transferEntity.ExternalName = parentFunctionEntity.ExternalName;
                transferEntity.TimeBeforeInMinutes = parentFunctionEntity.TimeBeforeInMinutes;
                transferEntity.TimeBeforeType = parentFunctionEntity.TimeBeforeType;
                transferEntity.TimeAfterInMinutes = parentFunctionEntity.TimeAfterInMinutes;
                transferEntity.TimeAfterType = parentFunctionEntity.TimeAfterType;
                transferEntity.TakeTimeFromLocation = parentFunctionEntity.TakeTimeFromLocation;
                transferEntity.DurationInMinutes = parentFunctionEntity.DurationInMinutes;
                transferEntity.DurationType = parentFunctionEntity.DurationType;
                transferEntity.BreakInMinutes = parentFunctionEntity.BreakInMinutes;
                transferEntity.BreakType = parentFunctionEntity.BreakType;
                transferEntity.SubhireFixed = parentFunctionEntity.SubhireFixed;
                transferEntity.SubhireHourRate = parentFunctionEntity.SubhireHourRate;
                transferEntity.RentalFixed = parentFunctionEntity.RentalFixed;
                transferEntity.RentalHourRate = parentFunctionEntity.RentalHourRate;
                transferEntity.VatClassId = parentFunctionEntity.VatClassId;
                transferEntity.ShowInPlaner = parentFunctionEntity.ShowInPlaner;
                transferEntity.IncludeInPrice = parentFunctionEntity.IncludeInPrice;
                //transferEntity.PlanFromTimeType = parentFunctionEntity.PlanFromTimeType;
                //transferEntity.PlanFrom = parentFunctionEntity.PlanFrom;
                //transferEntity.PlanUntilTimeType = parentFunctionEntity.PlanUntilTimeType;
                //transferEntity.PlanUntil = parentFunctionEntity.PlanUntil;
                //transferEntity.UseFromTimeType = parentFunctionEntity.UseFromTimeType;
                //transferEntity.UseFrom = parentFunctionEntity.UseFrom;
                //transferEntity.UseUntilTimeType = parentFunctionEntity.UseUntilTimeType;
                //transferEntity.UseUntil = parentFunctionEntity.UseUntil;
                transferEntity.TimeFrameId = parentFunctionEntity.TimeFrameId;
                transferEntity.Distance = parentFunctionEntity.Distance;
            }
            CalculateCostAndPrice(dto, ref transferEntity, vatScheme);
            ProjectFunctionEntity entity = await _projectFunctionStorage.Create(transferEntity);
            ProjectFunctionGridDto result = entity.TransferToGridDto();
            return result;
        }


        public async Task<bool> DeleteFunction(int id)
        {
            bool result = await _projectFunctionStorage.Delete(id);
            return result;
        }


        public async Task<BaseListDto> GetAllFunctionsByProject(int? projectId)
        {
            List<ProjectFunctionEntity> entities;
            List<ProjectFunctionGridDto> listProjectFunctionDtos = new List<ProjectFunctionGridDto>();
            List<FunctionDto> listFunctionDtos;
            BaseGrid grid=null;
            if (projectId.HasValue && projectId > 0)
            {
                List<ProjectFunctionGroupEntity> groupEntities = await _projectFunctionGroupStorage.GetListFunctionGroupsByProdjectId(projectId.Value);
                entities = await _projectFunctionStorage.GetAllByProject(projectId.Value);
                if (groupEntities.Count == 0 && entities.Count > 0)
                {
                    ProjectEntity project = await _projectStorage.GetById(projectId.Value);
                    ProjectFunctionGroupEntity createEntity = new ProjectFunctionGroupEntity()
                    {
                        SubprojectId = projectId.Value,
                        Name = "Група",
                        UseFrom = project.Times?.Where(x => x.DefaultUsagePeriod).FirstOrDefault()?.From,
                        UseUntil = project.Times?.Where(x => x.DefaultUsagePeriod).FirstOrDefault()?.Until,
                        PlanFrom = project.Times?.Where(x => x.DefaultPlanPeriod).FirstOrDefault()?.From,
                        PlanUntil = project.Times?.Where(x => x.DefaultPlanPeriod).FirstOrDefault()?.Until,
                    };
                    ProjectFunctionGroupEntity entity = await _projectFunctionGroupStorage.Create(createEntity);
                    List<ProjectFunctionEntity> updateEntities = entities.Select(x => { x.ProjectFunctionGroupId = entity.Id; return x; }).ToList();
                    foreach (ProjectFunctionEntity el in updateEntities)
                    {
                        var updateEntity = await _projectFunctionStorage.Update(el);
                    }
                    ProjectFunctionGridDto group = entity.TransferToGridDto();
                    group.Childrens = entities.TransferToListGridDto();
                    listProjectFunctionDtos.Add(group);
                }
                else
                {
                    listProjectFunctionDtos = groupEntities.TransferToListGridDto();
                    listProjectFunctionDtos.Select(x => { x.Childrens = entities.Where(y => y.ProjectId == x.ProjectId && y.ProjectFunctionGroupId == x.Id).ToList().TransferToListGridDto(); return x; }).ToList();
                }
                grid = listProjectFunctionDtos.CreateGrid<ProjectFunctionGridDto>();
                grid.DataGroups = listProjectFunctionDtos.GroupBy(x => x.Type.ToString()).Select(x => new
                {
                    Key = x.Key.ToLower(),
                    Values = x.Take(500).ToList()
                });
            }
            else
            {
                entities = await _projectFunctionStorage.GetAllFunction();
                listFunctionDtos = entities.TransferToListFunctionDto();
                grid = listFunctionDtos.CreateGrid<FunctionDto>();
                grid.DataGroups = listFunctionDtos.GroupBy(x => x.Type.ToString()).Select(x => new
                {
                    Key = x.Key.ToLower(),
                    Values = x.Take(500).ToList()
                });
            }


            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.ProjectFunctionEntity
            };

        }

        public async Task<ProjectFunctionDto> GetFunctionById(int id)
        {
            ProjectFunctionEntity entity = await _projectFunctionStorage.GetById(id);
            ProjectFunctionDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<ProjectFunctionDto> UpdateFunction(ProjectFunctionDto dto)
        {
            ProjectFunctionEntity transferEntity = dto.TransferToEntity();
            CalculateCostAndPrice(dto, ref transferEntity);
            ProjectFunctionEntity entity = await _projectFunctionStorage.Update(transferEntity);
            ProjectFunctionDto model = entity.TransferToDto();
            return model;
        }

        public async Task<ProjectFunctionGridDto> UpdateFunctionForProject(ProjectFunctionDto dto, VatSchemeRateDto vatScheme)
        {
            ProjectFunctionEntity transferEntity = dto.TransferToEntity();
            CalculateCostAndPrice(dto, ref transferEntity, vatScheme);
            ProjectFunctionEntity dbEntity = await _projectFunctionStorage.GetById(dto.Id);
            transferEntity.UseFrom = dto.UseFrom ?? dbEntity.UseFrom;
            transferEntity.UseUntil = dto.UseUntil ?? dbEntity.UseUntil;
            transferEntity.PlanFrom = dto.PlanFrom ?? dbEntity.PlanFrom;
            transferEntity.PlanUntil = dto.PlanUntil ?? dbEntity.PlanUntil;
            ProjectFunctionEntity entity = await _projectFunctionStorage.Update(transferEntity);
            ProjectFunctionGridDto model = entity.TransferToGridDto();
            return model;
        }

        private static void CalculateCostAndPrice(ProjectFunctionDto dto, ref ProjectFunctionEntity transferEntity, VatSchemeRateDto vatScheme = null)
        {
            if (dto.Type == ProjectFunctionTypeEnum.Crew)
            {
                int durationUsage = dto.CalculateFunctionDurationUsage();
                transferEntity.TotalCosts = dto.CalculateFunctionCrewTotalCosts(durationUsage);
                transferEntity.TotalPrice = dto.CalculateFunctionCrewTotalPrice(durationUsage);
                
            }
            else
            {
                decimal distance = dto.Distance;
                transferEntity.TotalCosts = dto.CalculateFunctionTransportTotalCosts(distance);
                transferEntity.TotalPrice = dto.CalculateFunctionTransportTotalPrice(distance);
            }
            if (vatScheme != null)
            {
                transferEntity.TotalIncVat =  transferEntity.CalculateTotalIncVat(vatScheme);
            }
        }


        #region Function Groups

        public async Task<BaseListDto> GetAllGroupByProject(int projectId)
        {
            List<ProjectFunctionGroupEntity> entities = await _projectFunctionGroupStorage.GetListFunctionGroupsByProdjectId(projectId);
            List<ProjectFunctionGroupDto> dtos = entities.TransferToListDto();
            BaseGrid grid = dtos.CreateGrid<ProjectFunctionGroupDto>();
            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.ProjectFunctionEntity
            };

        }

        public async Task<ProjectFunctionGridDto> CreateGroup(ProjectFunctionGroupDto dto)
        {
            ProjectFunctionGroupEntity transferEntity = dto.TransferToEntity();
            ProjectEntity project = await _projectStorage.GetById(dto.ProjectId);
            if (dto.UseFrom == null)
            {
                transferEntity.UseFrom = project.Times?.Where(x => x.DefaultUsagePeriod).FirstOrDefault()?.From;
            }
            if (dto.UseUntil == null)
            {
                transferEntity.UseUntil = project.Times?.Where(x => x.DefaultUsagePeriod).FirstOrDefault()?.Until;
            }
            if (dto.PlanFrom == null)
            {
                transferEntity.PlanFrom = project.Times?.Where(x => x.DefaultPlanPeriod).FirstOrDefault()?.From;
            }
            if (dto.PlanUntil == null)
            {
                transferEntity.PlanUntil = project.Times?.Where(x => x.DefaultPlanPeriod).FirstOrDefault()?.Until;
            }
            ProjectFunctionGroupEntity entity = await _projectFunctionGroupStorage.Create(transferEntity);
            ProjectFunctionGridDto result = entity.TransferToGridDto();
            return result;
        }

        public async Task<bool> DeleteGroup(int id)
        {
            bool result = await _projectFunctionGroupStorage.Delete(id);
            return result;
        }
        public async Task<ProjectFunctionGridDto> UpdateGroup(ProjectFunctionGroupDto dto)
        {
            ProjectFunctionGroupEntity dbEntity = await _projectFunctionGroupStorage.GetById(dto.Id);
            dbEntity.Name = dto.Name;
            if (dto.PlanFrom != null)
            {
                dbEntity.PlanFrom = dto.PlanFrom;
            }
            if (dto.PlanUntil != null)
            {
                dbEntity.PlanUntil = dto.PlanUntil;
            }
            if (dto.UseFrom != null)
            {
                dbEntity.UseFrom = dto.UseFrom;
            }
            if (dto.UseUntil != null)
            {
                dbEntity.UseUntil = dto.UseUntil;
            }
            ProjectFunctionGroupEntity entity = await _projectFunctionGroupStorage.Update(dbEntity);
            ProjectFunctionGridDto result = entity.TransferToGridDto();
            return result;
        }

        #endregion
    }
}
