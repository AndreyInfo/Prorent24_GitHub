using Prorent24.BLL.Builders;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.Project;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.Common.Models.Warehouse;
using Prorent24.DAL.Models.Project;
using Prorent24.DAL.Storages.CrewMember;
using Prorent24.DAL.Storages.Project;
using Prorent24.DAL.Storages.Vehicle;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.Project;
using Prorent24.Entity.Vehicle;
using Prorent24.Enum.Entity;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Project.Planning
{
    internal class ProjectPlanningService : IProjectPlanningService
    {
        private readonly IProjectPlanningStorage _projectPlanningStorage;
        private readonly ICrewMemberRateStorage _crewMemberRateStorage;
        private readonly ICrewMemberStorage _crewMemberStorage;
        private readonly IVehicleStorage _vehicleStorage;
        private readonly IProjectFunctionStorage _projectFunctionStorage;

        public ProjectPlanningService(IProjectPlanningStorage projectPlanningStorage, ICrewMemberRateStorage crewMemberRateStorage,
            ICrewMemberStorage crewMemberStorage, IVehicleStorage vehicleStorage, IProjectFunctionStorage projectFunctionStorage)
        {
            _projectPlanningStorage = projectPlanningStorage;
            _crewMemberRateStorage = crewMemberRateStorage;
            _crewMemberStorage = crewMemberStorage;
            _vehicleStorage = vehicleStorage;
            _projectFunctionStorage = projectFunctionStorage;
        }

        public async Task<ProjectPlanningGridDto> CreatePlanning(ProjectPlanningDto dto)
        {
            ProjectFunctionEntity functionEntity = await _projectFunctionStorage.GetById(dto.FunctionId);
            List<ProjectPlanningEntity>  existEntities = await _projectPlanningStorage.GetAllByProjectFuntionByType(dto.FunctionId, dto.EntityId, dto.Type);

            if (dto.Type == ProjectFunctionTypeEnum.Crew)
            {
               
                if (dto.RateType == PlanningCrewMemberRateEnum.CrewRate)
                {
                    int durationPlanned = dto.CalculateDurationPlanned();
                    if (dto.CrewMemberRateId != null)
                    {
                        CrewMemberRateEntity rateEntity = await _crewMemberRateStorage.GetById(dto.CrewMemberRateId);
                        dto.CrewMemberHourlyRate = rateEntity.DailyRate;
                    }
                    else
                    {
                        CrewMemberEntity crewEntity = await _crewMemberStorage.GetById(dto.EntityId);
                        if (crewEntity.DefaultRate?.HourlyRate > 0)
                        {
                            dto.CrewMemberHourlyRate = Convert.ToDecimal(crewEntity.DefaultRate?.HourlyRate);
                        }
                        else
                        {
                            dto.CrewMemberHourlyRate = 0;
                        }
                        dto.PlannedCosts = dto.CalculatePlannedCosts(durationPlanned);
                        dto.Costs = 0;
                    }
                }

            }
            else
            {
                VehicleEntity vehicle = await _vehicleStorage.GetById(dto.EntityId);
                dto.PlannedCosts = vehicle.DayilCosts + vehicle.VariableCosts * functionEntity.Distance;
                dto.Costs = 0;
            }
            ProjectPlanningEntity transferEntity = dto.TransferToEntity();
            if (transferEntity.PlanFrom == null)
            {
                transferEntity.PlanFrom = functionEntity.ProjectFunctionGroup?.PlanFrom ?? DateTime.UtcNow;
            }
            if (transferEntity.PlanUntil == null)
            {
                transferEntity.PlanUntil = functionEntity.ProjectFunctionGroup?.PlanUntil ?? DateTime.UtcNow;
            }
            if (existEntities == null || existEntities?.Count() == 0)
            {
                ProjectPlanningEntity entity = await _projectPlanningStorage.Create(transferEntity);
                ProjectPlanningEntity resultEntity = await _projectPlanningStorage.GetById(entity.Id);
                ProjectPlanningDto result = resultEntity.TransferToDto();
                ProjectPlanningGridDto gridResult = result.TransferToTreeGridDto();
                return gridResult;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeletePlanning(int id)
        {
            bool result = await _projectPlanningStorage.Delete(id);
            return result;
        }

        public async Task<ProjectPlanningGridDto> UpdatePlanning(ProjectPlanningDto dto)
        {
            ProjectPlanningEntity dbEntity = await _projectPlanningStorage.GetById(dto.Id);

            if (dbEntity.CrewMemberId != null)
            {
                int durationPlanned = dto.CalculateDurationPlanned();
                if (dbEntity.RateType != dto.RateType)
                {
                    if (dto.RateType == PlanningCrewMemberRateEnum.PriceAgreement)
                    {
                        dto.PlannedCosts = dto.Costs;
                    }
                }
                else if (dbEntity.CrewMemberRateId != dto.CrewMemberRateId)
                {
                    CrewMemberRateEntity rateEntity = await _crewMemberRateStorage.GetById(dto.CrewMemberRateId);
                    dto.CrewMemberHourlyRate = rateEntity.HourlyRate;
                    dto.PlannedCosts = dto.CalculatePlannedCosts(durationPlanned);
                }
            }

            ProjectPlanningEntity transferEntity = dto.TransferToEntity();
            transferEntity = dbEntity != null ? transferEntity.MergeEntity(dbEntity) : transferEntity;
            ProjectPlanningEntity entity = await _projectPlanningStorage.Update(transferEntity);
            ProjectPlanningEntity entityRes = await _projectPlanningStorage.GetById(transferEntity.Id);
            ProjectPlanningDto result = entityRes.TransferToDto();
            ProjectPlanningGridDto gridResult = result.TransferToTreeGridDto();
            return gridResult;
        }

        public async Task<BaseListDto> GetGridPlanningByGroupedFunctions(ProjectPlanningFilter filter)
        {
            List<ProjectFunctionEntity> projectFunctions = await _projectFunctionStorage.GetAllByProject(filter.ProjectId, filter.Type); // , ProjectFunctionTypeEnum.Crew
            List<ProjectPlanningEntity> entities = await _projectPlanningStorage.GetAllGroupedByFuctions(filter.ProjectId);

            List<ProjectPlanningEntity> allPlannings = await _projectPlanningStorage.GetAll();


            List<ProjectFunctionDto> projectFunctionsDto = projectFunctions.TransferToListDto(entities);
            List<ProjectPlanningGridDto> projectPlanningTreeGridDtos = projectFunctionsDto.TransferToTreeGridDto();
            foreach (ProjectPlanningGridDto el in projectPlanningTreeGridDtos)
            {
                foreach (ProjectPlanningGridDto child in el.Childrens)
                {
                    child.IsAvailability = child.FunctionType == ProjectFunctionTypeEnum.Crew ?
                        !allPlannings.Any(z => z.CrewMemberId == child.EntityId && z.Id != child.Id
                    && ((child.PlanUntil >= z.PlanFrom && child.PlanUntil <= z.PlanUntil)
                    || (child.PlanFrom >= z.PlanFrom && child.PlanFrom <= z.PlanUntil)
                    || (z.PlanUntil >= child.PlanFrom && z.PlanUntil <= child.PlanUntil)
                    || (z.PlanFrom >= child.PlanFrom && z.PlanFrom <= child.PlanUntil)
                    ))
                    : !allPlannings.Any(z => z.VehicleId.ToString() == child.EntityId && z.Id != child.Id
                   && ((child.PlanUntil >= z.PlanFrom && child.PlanUntil <= z.PlanUntil)
                    || (child.PlanFrom >= z.PlanFrom && child.PlanFrom <= z.PlanUntil)
                    || (z.PlanUntil >= child.PlanFrom && z.PlanUntil <= child.PlanUntil)
                    || (z.PlanFrom >= child.PlanFrom && z.PlanFrom <= child.PlanUntil)
                    ));
                }
            }
            BaseGrid grid = projectPlanningTreeGridDtos.CreateGrid<ProjectPlanningGridDto>();
            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.ProjectPlanningEntity
            };
        }

        public async Task<List<ProjectPlanningGridDto>> GetPlanningGroupedFunctions(int projectId, ProjectFunctionTypeEnum type)
        {
            List<ProjectFunctionEntity> projectFunctions = await _projectFunctionStorage.GetAllByProject(projectId, type); // , ProjectFunctionTypeEnum.Crew
            List<ProjectPlanningEntity> entities = await _projectPlanningStorage.GetAllGroupedByFuctions(projectId);

            List<ProjectPlanningEntity> allPlannings = await _projectPlanningStorage.GetAll();

            List<ProjectFunctionDto> projectFunctionsDto = projectFunctions.TransferToListDto(entities);
            List<ProjectPlanningGridDto> projectPlanningTreeGridDtos = projectFunctionsDto.TransferToTreeGridDto();
            foreach (ProjectPlanningGridDto el in projectPlanningTreeGridDtos)
            {
                foreach (ProjectPlanningGridDto child in el.Childrens)
                {
                    child.IsAvailability = child.FunctionType == ProjectFunctionTypeEnum.Crew ?
                        !allPlannings.Any(z => z.CrewMemberId == child.EntityId && z.Id != child.Id
                    && ((child.PlanUntil >= z.PlanFrom && child.PlanUntil <= z.PlanUntil)
                    || (child.PlanFrom >= z.PlanFrom && child.PlanFrom <= z.PlanUntil)
                    || (z.PlanUntil >= child.PlanFrom && z.PlanUntil <= child.PlanUntil)
                    || (z.PlanFrom >= child.PlanFrom && z.PlanFrom <= child.PlanUntil)
                    ))
                    : !allPlannings.Any(z => z.VehicleId.ToString() == child.EntityId && z.Id != child.Id
                   && ((child.PlanUntil >= z.PlanFrom && child.PlanUntil <= z.PlanUntil)
                    || (child.PlanFrom >= z.PlanFrom && child.PlanFrom <= z.PlanUntil)
                    || (z.PlanUntil >= child.PlanFrom && z.PlanUntil <= child.PlanUntil)
                    || (z.PlanFrom >= child.PlanFrom && z.PlanFrom <= child.PlanUntil)
                    ));
                }
            }
            return projectPlanningTreeGridDtos;
        }


        public async Task<ProjectPlanningDto> GetPlanningById(int id)
        {
            ProjectPlanningEntity entity = await _projectPlanningStorage.GetById(id);
            ProjectPlanningDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<WarhouseEventResourseModel> GetWarhouseTimeLineCrewOrTransport(DateTime date, ProjectFunctionTypeEnum projectFunctionType)
        {
            List<ProjectPlanningEntity> entities = await _projectPlanningStorage.GetAllByDate(date);

            WarhouseEventResourseModel warhouseEventResourseModel = new WarhouseEventResourseModel();
            entities = entities.Where(x => x.Function.Type == projectFunctionType).ToList();

            if (projectFunctionType == ProjectFunctionTypeEnum.Crew)
            {
                warhouseEventResourseModel.Resources = entities.Where(x => x.PlanFrom.HasValue && x.PlanUntil.HasValue).Select(x => x.CrewMember).Distinct().Select(x => x!=null ? new WarhouseResourceModel()
                {
                    Id = x.UserId.ToString(),
                    Name = $"{x.User.FirstName} {x.User?.LastName}"
                } : null).ToList();

                warhouseEventResourseModel.Events = entities.Where(x => x.PlanFrom.HasValue && x.PlanUntil.HasValue).Select(x => x!=null ? new WarhouseEventModel()
                {
                    Id = x.Id.ToString(),
                    Resource = x.CrewMember?.UserId?.ToString(),
                    Start = DateTime.Parse(x.PlanFrom.Value.ToLongDateString()),
                    End = DateTime.Parse(x.PlanUntil.Value.ToLongDateString()),
                    Text = $"{ x.Function?.Project?.Name} - {x.CrewMember?.User?.FirstName} {x.CrewMember?.User?.LastName}"
                } : null).ToList();
            }
            else
            {
                warhouseEventResourseModel.Resources = entities.Where(x => x.PlanFrom.HasValue && x.PlanUntil.HasValue).Select(x => x.Vehicle).Distinct().Select(x => x!=null ? new WarhouseResourceModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name
                } : null).ToList();


                warhouseEventResourseModel.Events = entities.Where(x => x.PlanFrom.HasValue && x.PlanUntil.HasValue).Select(x => x!=null ? new WarhouseEventModel()
                {
                    Id = x.Id.ToString(),
                    Resource = x.VehicleId?.ToString(),
                    Start = DateTime.Parse(x.PlanFrom.Value.ToLongDateString()),
                    End = DateTime.Parse(x.PlanUntil.Value.ToLongDateString()),
                    Text = $"{ x.Function?.Project?.Name} - { x.Vehicle?.Name }"
                } : null).ToList();
            }

            return warhouseEventResourseModel;
        }

        public async Task<WarhouseEventResourseModel> GetForGeneralTimeLine(List<SelectedFilter> filters)
        {
            string searctText;
            IQueryable<ProjectFunctionEntity> projectQueryableEntity = _projectFunctionStorage.QueryableEntity.CreateFilterForGeneralTimeLineProjectFunction(filters, out searctText);
            List<ProjectFunctionEntity> functions = await _projectFunctionStorage.GetAllFunctionForGeneralTimeLine(projectQueryableEntity, searctText);

            //List<ProjectFunctionEntity> functions = await _projectFunctionStorage.GetAllFunctionForGeneralTimeLine(filters);

            List<ProjectFunctionFilterModel> filterFunction = functions.Where(x=>x.Project!=null).Select(x => new ProjectFunctionFilterModel()
            {
                ProjectId = Convert.ToInt32(x.ProjectId),
                FunctionId = x.Id
            }).ToList();
            List<ProjectPlanningEntity> plannings = await _projectPlanningStorage.GetAllByProjectFuntion(filterFunction);
            WarhouseEventResourseModel warhouseEventResourseModel = new WarhouseEventResourseModel()
            {
                Resources = functions.Select(x => x.Project).Distinct().ToList().Select(x => new WarhouseResourceModel()
                {
                    Id = x?.Id.ToString(),
                    Name = x?.Name,
                    Children = new List<WarhouseResourceModel>()
                    {
                        new WarhouseResourceModel(){ Id = x?.Id.ToString()+"_1",    Name = "planning"  }
                    }
                }).ToList()
            };
            if (functions.Count == 0 && plannings.Count > 0)
            {
                warhouseEventResourseModel.Resources = plannings.Select(x => x.Function.Project).Distinct().ToList().Select(x => new WarhouseResourceModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Children = new List<WarhouseResourceModel>()
                    {
                        new WarhouseResourceModel(){ Id = x.Id.ToString()+"_1",    Name = "planning"  }
                    }
                }).ToList();
            }

            warhouseEventResourseModel.Events = functions.Where(x => x.ProjectFunctionGroup?.UseFrom != null && x.ProjectFunctionGroup?.UseUntil != null).Select(x => new WarhouseEventModel()
            {
                EntityId = x.Id.ToString(),
                Resource = x.ProjectId.ToString(),
                Start = x.ProjectFunctionGroup?.UseFrom,
                End = x.ProjectFunctionGroup?.UseUntil,
                Text = x.InternalName,
                Type = x.Type.ToString(),
                NeedQuantity = x.Quantity ?? 0,
                PlannedQuantity = plannings.Where(y => y.FunctionId == x.Id && y.Function.ProjectId == x.ProjectId).ToList()?.Count() ?? 0,
                // BackColor = x.Project?.Color
            }).ToList();


            List<WarhouseEventModel> childrenEvents = plannings.Where(x => x.Function?.ProjectFunctionGroup?.UseFrom != null && x.Function?.ProjectFunctionGroup?.UseUntil != null).Select(x => new WarhouseEventModel()
            {
                EntityId = x.Id.ToString(),
                Resource = x.Function?.ProjectId.ToString() + "_1",
                Start = x.Function?.ProjectFunctionGroup?.UseFrom,
                End = x.Function?.ProjectFunctionGroup?.UseUntil,
                Text = x.Function?.Type == ProjectFunctionTypeEnum.Crew ? $"{x.CrewMember?.User?.FirstName} {x.CrewMember?.User?.LastName}" : x.Vehicle?.Name,
                Type = x.Function?.Type.ToString()
            }).ToList();

            warhouseEventResourseModel.Events.AddRange(childrenEvents);
            return warhouseEventResourseModel;
        }

        public async Task<WarhouseEventResourseModel> CreatePlanningFromCrewPlanner(int projectId, List<ProjectPlanningDto> model, ProjectFunctionTypeEnum type, int functionId)
        {
            List<ProjectPlanningEntity> existPlannings = await _projectPlanningStorage.GetAllByProject(new ProjectPlanningFilterModel() { ProjectId = projectId, Type = type });
            List<ProjectPlanningEntity> listProjPlanning = new List<ProjectPlanningEntity>();
            foreach (ProjectPlanningDto dto in model)
            {
                ProjectFunctionEntity functionEntity = await _projectFunctionStorage.GetById(dto.FunctionId);
                if (type == ProjectFunctionTypeEnum.Crew)
                {
                    if (dto.RateType == PlanningCrewMemberRateEnum.CrewRate)
                    {
                        int durationPlanned = dto.CalculateDurationPlanned();
                        if (dto.CrewMemberRateId != null)
                        {
                            CrewMemberRateEntity rateEntity = await _crewMemberRateStorage.GetById(dto.CrewMemberRateId);
                            dto.CrewMemberHourlyRate = rateEntity.DailyRate;
                        }
                        else
                        {
                            CrewMemberEntity crewEntity = await _crewMemberStorage.GetById(dto.EntityId);
                            if (crewEntity.DefaultRate?.HourlyRate > 0)
                            {
                                dto.CrewMemberHourlyRate = Convert.ToDecimal(crewEntity.DefaultRate?.HourlyRate);
                            }
                            else
                            {
                                dto.CrewMemberHourlyRate = 0;
                            }
                            dto.PlannedCosts = dto.CalculatePlannedCosts(durationPlanned);
                            dto.Costs = 0;
                        }
                    }
                }
                else
                {
                    VehicleEntity vehicle = await _vehicleStorage.GetById(dto.EntityId);
                    dto.PlannedCosts = vehicle.DayilCosts + vehicle.VariableCosts * functionEntity.Distance;
                    dto.Costs = 0;
                }
                ProjectPlanningEntity transferEntity = dto.TransferToEntity();
                if (transferEntity.PlanFrom == null)
                {
                    transferEntity.PlanFrom = functionEntity.ProjectFunctionGroup?.PlanFrom ?? DateTime.UtcNow;
                }
                if (transferEntity.PlanUntil == null)
                {
                    transferEntity.PlanUntil = functionEntity.ProjectFunctionGroup?.PlanUntil ?? DateTime.UtcNow;
                }
                if ((type == ProjectFunctionTypeEnum.Crew && !existPlannings.Any(x => x.FunctionId == transferEntity.FunctionId && x.CrewMemberId == transferEntity.CrewMemberId)) //не допускаємо дублі серед персоналу
                || (type == ProjectFunctionTypeEnum.Transport && !existPlannings.Any(x => x.FunctionId == transferEntity.FunctionId && x.VehicleId == transferEntity.VehicleId)))
                {
                    ProjectPlanningEntity entity = await _projectPlanningStorage.Create(transferEntity);
                    ProjectPlanningEntity resultEntity = await _projectPlanningStorage.GetById(entity.Id);
                    listProjPlanning.Add(resultEntity);
                }


            }
            WarhouseEventResourseModel warhouseEventResourseModel = new WarhouseEventResourseModel();

            if (type == ProjectFunctionTypeEnum.Crew)
            {
                warhouseEventResourseModel.Events = listProjPlanning.Select(x => new WarhouseEventModel()
                {
                    EntityId = x.Id.ToString(),
                    Resource = x.CrewMemberId.ToString(),
                    Start = x.PlanFrom,
                    End = x.PlanUntil,
                    Text = x.Function.InternalName + " - " + x.Function?.Project?.Name
                }).ToList();

            }
            else
            {
                warhouseEventResourseModel.Events = listProjPlanning.Select(x => new WarhouseEventModel()
                {
                    EntityId = x.Id.ToString(),
                    Resource = x.VehicleId.ToString(),
                    Start = x.PlanFrom,
                    End = x.PlanUntil,
                    Text = x.Function.InternalName + " - " + x.Function?.Project?.Name
                }).ToList();

            }
            List<WarhouseEventModel> childrenEvents = listProjPlanning.Where(x => x.Function?.ProjectFunctionGroup?.UseFrom != null && x.Function?.ProjectFunctionGroup?.UseUntil != null).Select(x => new WarhouseEventModel()
            {
                EntityId = x.Id.ToString(),
                Resource = x.Function?.ProjectId.ToString() + "_1",
                Start = x.Function?.ProjectFunctionGroup?.UseFrom,
                End = x.Function?.ProjectFunctionGroup?.UseUntil,
                Text = x.Function?.Type == ProjectFunctionTypeEnum.Crew ? $"{x.CrewMember?.User.FirstName} {x.CrewMember?.User?.LastName}" : x.Vehicle.Name,
                Type = x.Function?.Type.ToString()
            }).ToList();
            warhouseEventResourseModel.Events.AddRange(childrenEvents);
            return warhouseEventResourseModel;
        }
    }
}
