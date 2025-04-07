using Prorent24.BLL.Builders;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.CrewPlanner;
using Prorent24.BLL.Transfers.CrewMember;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Schedulers;
using Prorent24.Common.Models.Warehouse;
using Prorent24.DAL.Storages.CrewMember;
using Prorent24.DAL.Storages.CrewPlanner;
using Prorent24.DAL.Storages.Project;
using Prorent24.DAL.Storages.Vehicle;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.CrewPlanner;
using Prorent24.Entity.Project;
using Prorent24.Entity.Vehicle;
using Prorent24.Enum.CrewPlanner;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.CrewPlanner
{
    internal class CrewPlannerService : ICrewPlannerService
    {
        private readonly ICrewPlannerStorage _crewPlannerStorage;
        private readonly ICrewMemberStorage _crewMemberStorage;
        private readonly IVehicleStorage _vehicleStorage;
        private readonly IProjectPlanningStorage _projectPlanningStorage;

        public CrewPlannerService(ICrewPlannerStorage crewPlannerStorage, ICrewMemberStorage crewMemberStorage,
            IVehicleStorage vehicleStorage, IProjectPlanningStorage projectPlanningStorage)
        {
            _crewPlannerStorage = crewPlannerStorage;
            _crewMemberStorage = crewMemberStorage;
            _vehicleStorage = vehicleStorage;
            _projectPlanningStorage = projectPlanningStorage;
        }

        public async Task<CrewPlannerDto> Create(CrewPlannerDto createModel)
        {
            List<CrewPlannerEntity> entities = await _crewPlannerStorage.GetAll(createModel.Type, createModel.FunctionIds);

            foreach (string functionId in createModel.FunctionIds)
            {
                CrewPlannerEntity entity = await CreateChangePeriod(createModel, entities, functionId);
                createModel.Id = entity.Id;
            }
            return createModel;
        }

        private async Task<CrewPlannerEntity> CreateChangePeriod(CrewPlannerDto createModel, List<CrewPlannerEntity> entities, string functionId)
        {
            List<CrewPlannerEntity> crewPlanners = new List<CrewPlannerEntity>();
            CrewPlannerEntity model = new CrewPlannerEntity();
            if (createModel.Type == ProjectFunctionTypeEnum.Crew)
            {
                crewPlanners = entities.Where(x => x.CrewMemberId == functionId).ToList();
                model = new CrewPlannerEntity()
                {
                    Action = createModel.Action,
                    From = createModel.From,
                    Until = createModel.Until,
                    CrewMemberId = functionId,
                    Comment = createModel.Comment
                };
            }
            else if (createModel.Type == ProjectFunctionTypeEnum.Transport)
            {
                crewPlanners = entities.Where(x => x.VehicleId == Convert.ToInt32(functionId)).ToList();
                model = new CrewPlannerEntity()
                {
                    Action = createModel.Action,
                    From = createModel.From,
                    Until = createModel.Until,
                    VehicleId = Convert.ToInt32(functionId)
                };
            }
            //видаляємо вкладені повністю об'єкти, так як новий CrewPlanner є більш пріорітетним
            List<CrewPlannerEntity> innerCrewPlanners = crewPlanners.Where(x => x.From >= model.From && x.Until <= model.Until).ToList();
            foreach (CrewPlannerEntity el in innerCrewPlanners)
            {
                bool del = await _crewPlannerStorage.Delete(el.Id);
                crewPlanners.Remove(el);
            }

            //Початок вкладається в елемент CrewPlanner, або співпадає
            CrewPlannerEntity fromCrewPlanner = crewPlanners.Where(x => model.From >= x.From && model.From < x.Until).FirstOrDefault();

            //Кінець вкладається в елемент CrewPlanner, і не дорівнює fromCrewPlanner
            CrewPlannerEntity untilCrewPlanner = crewPlanners.Where(x => model.Until > x.From && model.Until < x.Until && x.Id != fromCrewPlanner?.Id).FirstOrDefault();

            bool isCreatePeriod = true;

            //model CrewPlanner вкладається в fromCrewPlanner, початки спіпадають , тому для fromCrewPlanner початок переноситься
            if (fromCrewPlanner != null && fromCrewPlanner.From == model.From)
            {
                if (fromCrewPlanner.Action != model.Action)
                {
                    fromCrewPlanner.From = model.Until;
                    CrewPlannerEntity updateCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);

                }
                else
                {
                    isCreatePeriod = false;

                }
            }
            // model CrewPlanner повністю вкладається в fromCrewPlanner
            else if (fromCrewPlanner != null && fromCrewPlanner.Until >= model.Until)
            {
                //розбивається на 3 періоди
                if (fromCrewPlanner.Action != model.Action)
                {
                    CrewPlannerEntity thirdCrewPlanner = new CrewPlannerEntity()
                    {
                        Action = fromCrewPlanner.Action,
                        From = model.Until,
                        Until = fromCrewPlanner.Until,
                        CrewMemberId = fromCrewPlanner.CrewMemberId,
                        Comment = fromCrewPlanner.Comment
                    };
                    CrewPlannerEntity until = await _crewPlannerStorage.Create(thirdCrewPlanner);

                    fromCrewPlanner.Until = model.From;
                    CrewPlannerEntity updateCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);
                }
                else
                {
                    isCreatePeriod = false;
                }

            }
            //початок model CrewPlanner вкладається в fromCrewPlanner, кінець поза
            else if (fromCrewPlanner != null && untilCrewPlanner == null)
            {
                if (fromCrewPlanner.Action != model.Action)
                {
                    fromCrewPlanner.Until = model.From;
                    CrewPlannerEntity updateCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);
                }
                else
                {
                    fromCrewPlanner.Until = model.Until;
                    CrewPlannerEntity updateCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);
                    isCreatePeriod = false;
                }

            }
            // початок model CrewPlanner лежить в fromCrewPlanner, кінець model CrewPlanner лежить в untilCrewPlanner 
            else if (fromCrewPlanner != null && untilCrewPlanner != null)
            {
                if (fromCrewPlanner.Action == model.Action && untilCrewPlanner.Action == model.Action)
                {
                    fromCrewPlanner.Until = untilCrewPlanner.Until;
                    CrewPlannerEntity updateFromCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);
                    bool del = await _crewPlannerStorage.Delete(untilCrewPlanner.Id);
                    isCreatePeriod = false;
                }
                else if (fromCrewPlanner.Action != model.Action && untilCrewPlanner.Action == model.Action)
                {
                    fromCrewPlanner.Until = model.From;
                    CrewPlannerEntity updateFromCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);

                    untilCrewPlanner.From = model.From;
                    CrewPlannerEntity updateUntilCrewPlanners = await _crewPlannerStorage.Update(untilCrewPlanner);

                    isCreatePeriod = false;
                }
                else if (fromCrewPlanner.Action == model.Action && untilCrewPlanner.Action != model.Action)
                {
                    fromCrewPlanner.Until = model.Until;
                    CrewPlannerEntity updateFromCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);

                    untilCrewPlanner.From = model.Until;
                    CrewPlannerEntity updateUntilCrewPlanners = await _crewPlannerStorage.Update(untilCrewPlanner);

                    isCreatePeriod = false;
                }
                else if (untilCrewPlanner.Action != model.Action && untilCrewPlanner.Action != model.Action)
                {
                    fromCrewPlanner.Until = model.From;
                    CrewPlannerEntity updateFromCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);

                    untilCrewPlanner.From = model.Until;
                    CrewPlannerEntity updateUntilCrewPlanners = await _crewPlannerStorage.Update(untilCrewPlanner);

                }
            }
            // fromCrewPlanner == null, кінець model CrewPlanner лежить в untilCrewPlanner 
            else if (untilCrewPlanner != null)
            {
                if (untilCrewPlanner.Action != model.Action)
                {
                    untilCrewPlanner.From = model.Until;
                    CrewPlannerEntity updateUntilCrewPlanners = await _crewPlannerStorage.Update(untilCrewPlanner);

                }
                else
                {
                    untilCrewPlanner.From = model.From;
                    CrewPlannerEntity updateUntilCrewPlanners = await _crewPlannerStorage.Update(untilCrewPlanner);
                    isCreatePeriod = false;

                }
            }

            if (isCreatePeriod)
            {
                CrewPlannerEntity entity = await _crewPlannerStorage.Create(model);
                return entity;
            }
            else
            {
                return model;
            }
        }
        public async Task<WarhouseEventResourseModel> GetAllForModuleTimeLine(ProjectFunctionTypeEnum type, List<string> ids, DateTime? dateFrom, DateTime? dateUntil)
        {
            DateTime from = dateFrom ?? DateTime.UtcNow.AddDays(-7);
            DateTime until = dateUntil ?? DateTime.UtcNow.AddDays(7);
            List<CrewPlannerEntity> entities = await _crewPlannerStorage.GetAllForModuleTimeLine(from, until, type, ids);

            List<ProjectPlanningEntity> plannings = await _projectPlanningStorage.GetAllForModuleTimeLine(from, until, type, ids);

            return await GenerateWarhouseEventResourseModel(type, ids, entities, plannings);
        }

        public async Task<WarhouseEventResourseModel> GetAll(ProjectFunctionTypeEnum type, List<string> ids, List<SelectedFilter> filters)
        {
            IQueryable<CrewPlannerEntity> queryableEntity = _crewPlannerStorage.QueryableEntity.CreateFilterForGeneralTimeLineCrewPlanner(filters);
            List<CrewPlannerEntity> entities = await _crewPlannerStorage.GetAllForTimeLine(queryableEntity, type, ids);

            IQueryable<ProjectPlanningEntity> queryablePlanningEntity = _projectPlanningStorage.QueryableEntity.CreateFilterForProjectPlanning(filters);
            List<ProjectPlanningEntity> plannings = await _projectPlanningStorage.GetAllByEntities(queryablePlanningEntity, type, ids);

            return await GenerateWarhouseEventResourseModel(type, ids, entities, plannings);
        }

        private async Task<WarhouseEventResourseModel> GenerateWarhouseEventResourseModel(ProjectFunctionTypeEnum type, List<string> ids, List<CrewPlannerEntity> entities, List<ProjectPlanningEntity> plannings)
        {
            WarhouseEventResourseModel warhouseEventResourseModel = new WarhouseEventResourseModel();
            if (type == ProjectFunctionTypeEnum.Crew)
            {
                List<CrewMemberEntity> crews = await _crewMemberStorage.GetList(ids);
                warhouseEventResourseModel.Resources = crews.Distinct().Select(x => new WarhouseResourceModel()
                {
                    Id = x.UserId,
                    Name = $"{x.User.FirstName} {x.User?.LastName}",
                    Availabilities = entities.Where(y => y.CrewMemberId == x.UserId).Select(y => new AvailabilityResourceModel()
                    {
                        Start = y.From,
                        End = y.Until,
                        Type = Char.ToLowerInvariant(y.Action.ToString()[0]) + y.Action.ToString().Substring(1)
                    }).ToList()
                }).ToList();

                warhouseEventResourseModel.Events = plannings.Select(x => new WarhouseEventModel()
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
                List<int> vehicleIds = ids.Select(x => Convert.ToInt32(x)).ToList();
                List<VehicleEntity> vehicles = await _vehicleStorage.GetByIds(vehicleIds);
                warhouseEventResourseModel.Resources = vehicles.Distinct().Select(x => new WarhouseResourceModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Availabilities = entities.Where(y => y.VehicleId == x.Id).Select(y => new AvailabilityResourceModel()
                    {
                        Start = y.From,
                        End = y.Until,
                        Type = Char.ToLowerInvariant(y.Action.ToString()[0]) + y.Action.ToString().Substring(1)
                    }).ToList()
                }).ToList();

                warhouseEventResourseModel.Events = plannings.Select(x => new WarhouseEventModel()
                {
                    EntityId = x.Id.ToString(),
                    Resource = x.VehicleId.ToString(),
                    Start = x.PlanFrom,

                    End = x.PlanUntil,
                    Text = x.Function.InternalName + " - " + x.Function?.Project?.Name
                }).ToList();

            }


            return warhouseEventResourseModel;
        }

        public async Task<List<SchedulerCrewMember>> GetShortSchedulerCrewMember()
        {
            var result = await _crewMemberStorage.GetList();
            List<CrewMemberDto> dtos = result.TransferToListDto();
            List<SchedulerCrewMember> schedulerCrewMembers = dtos.Select(x => new SchedulerCrewMember()
            {
                Id = x.Id,
                Description = x.Email,
                Location = "",
                Subject = x.Email + x.FirstName,
                Calendar = string.Concat(x.FirstName, " ", x.LastName),
                Start = DateTime.UtcNow,
                End = DateTime.UtcNow.AddDays(1)
            }).ToList();

            return schedulerCrewMembers;
        }

        public async Task<CrewPlannerDto> Update(CrewPlannerDto model)
        {
            CrewPlannerEntity dbEntity = await _crewPlannerStorage.GetById(model.Id);
            dbEntity.Action = model.Action;
            dbEntity.From = model.From;
            dbEntity.Until = model.Until;
            dbEntity.Comment = model.Comment;


            List<CrewPlannerEntity> entities = await _crewPlannerStorage.GetAll(model.Type, model.FunctionIds);
            List<CrewPlannerEntity> entitiesUnique = entities.Where(x => x.Id != model.Id).ToList();
            CrewPlannerEntity entity = await UpdateChangePeriod(dbEntity, entitiesUnique, model.FunctionIds.FirstOrDefault(), model.Type);

            return model;
        }

        private async Task<CrewPlannerEntity> UpdateChangePeriod(CrewPlannerEntity model, List<CrewPlannerEntity> entities, string functionId, ProjectFunctionTypeEnum type)
        {
            List<CrewPlannerEntity> crewPlanners = new List<CrewPlannerEntity>();
           
            if (type == ProjectFunctionTypeEnum.Crew)
            {
                crewPlanners = entities.Where(x => x.CrewMemberId == functionId).ToList();
            
            }
            else if (type == ProjectFunctionTypeEnum.Transport)
            {
                crewPlanners = entities.Where(x => x.VehicleId == Convert.ToInt32(functionId)).ToList();
               
            }
            //видаляємо вкладені повністю об'єкти, так як новий CrewPlanner є більш пріорітетним
            List<CrewPlannerEntity> innerCrewPlanners = crewPlanners.Where(x => x.From >= model.From && x.Until <= model.Until).ToList();
            foreach (CrewPlannerEntity el in innerCrewPlanners)
            {
                bool del = await _crewPlannerStorage.Delete(el.Id);
                crewPlanners.Remove(el);
            }

            //Початок вкладається в елемент CrewPlanner, або співпадає
            CrewPlannerEntity fromCrewPlanner = crewPlanners.Where(x => model.From >= x.From && model.From < x.Until).FirstOrDefault();

            //Кінець вкладається в елемент CrewPlanner, і не дорівнює fromCrewPlanner
            CrewPlannerEntity untilCrewPlanner = crewPlanners.Where(x => model.Until > x.From && model.Until < x.Until && x.Id != fromCrewPlanner?.Id).FirstOrDefault();

            bool isUpdatePeriod = true;

            //model CrewPlanner вкладається в fromCrewPlanner, початки спіпадають , тому для fromCrewPlanner початок переноситься
            if (fromCrewPlanner != null && fromCrewPlanner.From == model.From)
            {
                if (fromCrewPlanner.Action != model.Action)
                {
                    fromCrewPlanner.From = model.Until;
                    CrewPlannerEntity updateCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);

                }
                else
                {
                    isUpdatePeriod = false;

                }
            }
            // model CrewPlanner повністю вкладається в fromCrewPlanner
            else if (fromCrewPlanner != null && fromCrewPlanner.Until >= model.Until)
            {
                //розбивається на 3 періоди
                if (fromCrewPlanner.Action != model.Action)
                {
                    CrewPlannerEntity thirdCrewPlanner = new CrewPlannerEntity()
                    {
                        Action = fromCrewPlanner.Action,
                        From = model.Until,
                        Until = fromCrewPlanner.Until,
                        CrewMemberId = fromCrewPlanner.CrewMemberId,
                        Comment = fromCrewPlanner.Comment
                    };
                    CrewPlannerEntity until = await _crewPlannerStorage.Create(thirdCrewPlanner);

                    fromCrewPlanner.Until = model.From;
                    CrewPlannerEntity updateCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);
                }
                else
                {
                    isUpdatePeriod = false;
                }

            }
            //початок model CrewPlanner вкладається в fromCrewPlanner, кінець поза
            else if (fromCrewPlanner != null && untilCrewPlanner == null)
            {
                if (fromCrewPlanner.Action != model.Action)
                {
                    fromCrewPlanner.Until = model.From;
                    CrewPlannerEntity updateCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);
                }
                else
                {
                    fromCrewPlanner.Until = model.Until;
                    CrewPlannerEntity updateCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);
                    isUpdatePeriod = false;
                }

            }
            // початок model CrewPlanner лежить в fromCrewPlanner, кінець model CrewPlanner лежить в untilCrewPlanner 
            else if (fromCrewPlanner != null && untilCrewPlanner != null)
            {
                if (fromCrewPlanner.Action == model.Action && untilCrewPlanner.Action == model.Action)
                {
                    fromCrewPlanner.Until = untilCrewPlanner.Until;
                    CrewPlannerEntity updateFromCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);
                    bool del = await _crewPlannerStorage.Delete(untilCrewPlanner.Id);
                    isUpdatePeriod = false;
                }
                else if (fromCrewPlanner.Action != model.Action && untilCrewPlanner.Action == model.Action)
                {
                    fromCrewPlanner.Until = model.From;
                    CrewPlannerEntity updateFromCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);

                    untilCrewPlanner.From = model.From;
                    CrewPlannerEntity updateUntilCrewPlanners = await _crewPlannerStorage.Update(untilCrewPlanner);

                    isUpdatePeriod = false;
                }
                else if (fromCrewPlanner.Action == model.Action && untilCrewPlanner.Action != model.Action)
                {
                    fromCrewPlanner.Until = model.Until;
                    CrewPlannerEntity updateFromCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);

                    untilCrewPlanner.From = model.Until;
                    CrewPlannerEntity updateUntilCrewPlanners = await _crewPlannerStorage.Update(untilCrewPlanner);

                    isUpdatePeriod = false;
                }
                else if (untilCrewPlanner.Action != model.Action && untilCrewPlanner.Action != model.Action)
                {
                    fromCrewPlanner.Until = model.From;
                    CrewPlannerEntity updateFromCrewPlanners = await _crewPlannerStorage.Update(fromCrewPlanner);

                    untilCrewPlanner.From = model.Until;
                    CrewPlannerEntity updateUntilCrewPlanners = await _crewPlannerStorage.Update(untilCrewPlanner);

                }
            }
            // fromCrewPlanner == null, кінець model CrewPlanner лежить в untilCrewPlanner 
            else if (untilCrewPlanner != null)
            {
                if (untilCrewPlanner.Action != model.Action)
                {
                    untilCrewPlanner.From = model.Until;
                    CrewPlannerEntity updateUntilCrewPlanners = await _crewPlannerStorage.Update(untilCrewPlanner);

                }
                else
                {
                    untilCrewPlanner.From = model.From;
                    CrewPlannerEntity updateUntilCrewPlanners = await _crewPlannerStorage.Update(untilCrewPlanner);
                    isUpdatePeriod = false;

                }
            }

            if (isUpdatePeriod)
            {
                CrewPlannerEntity entity = await _crewPlannerStorage.Update(model);
                return entity;
            }
            else
            {
                return model;
            }
        }
    }
}
