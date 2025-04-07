using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Project;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.Project.Movement
{
    internal class ProjectEquipmentMovementStorage : BaseStorage<ProjectEquipmentMovementEntity>, IProjectEquipmentMovementStorage
    {
        public ProjectEquipmentMovementStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Task<IPagedList<ProjectEquipmentMovementEntity>> GetAll(List<Predicate<ProjectEquipmentMovementEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public override async Task<ProjectEquipmentMovementEntity> GetById(object id)
        {
            ProjectEquipmentMovementEntity result = await _repos.Table
                .Include(x => x.Project)
                .Include(x => x.ProjectEquipmentGroup)
                .Include(x => x.ProjectEquipment)
                .ThenInclude(x => x.Equipment)
                .Include(x => x.KitCaseEquipments)
                .ThenInclude(x => x.Equipment)
                .FirstOrDefaultAsync(x => x.Id == (int)id && x.IsRemoved != true);

            if (result.KitCaseEquipments != null && result.KitCaseEquipments.Count > 0)
            {
                result.KitCaseEquipments = result.KitCaseEquipments?.Where(y => y.IsRemoved != true).ToList();
            }
            return result;

        }

        public async Task<List<ProjectEquipmentMovementEntity>> GetEquipmentMovementByProjectId(int projectId)
        {
            List<ProjectEquipmentMovementEntity> result =  await _repos.Table
                .Include(x => x.Project)
                .Include(x => x.ProjectEquipmentGroup)
                .Include(x => x.ProjectEquipment)
                .ThenInclude(x => x.Equipment)
                .Include(y => y.KitCaseEquipments)
                .ThenInclude(x => x.Equipment)
                .Include(x => x.Equipment)
                .ThenInclude(x => x.EquipmentSerialNumbers)
                .Where(x => x.ProjectId == projectId && x.KitCaseEquipmentId == null && x.IsRemoved!=true && x.SelectedQuantity != 0).ToListAsync();
            foreach (ProjectEquipmentMovementEntity el in result)
            {
                if (el.KitCaseEquipments != null && el.KitCaseEquipments.Count > 0)
                {
                    el.KitCaseEquipments = el.KitCaseEquipments?.Where(y => y.IsRemoved != true && y.SelectedQuantity!=0).ToList();
                }
            }

            return result;
        }

        public async Task<List<ProjectEquipmentMovementEntity>> GetEquipmentMovementByProjectEquipmentId(int projectEquipmentId)
        {
            List<ProjectEquipmentMovementEntity> result = await _repos.Table
                .Include(x => x.Project)
                .Include(x => x.ProjectEquipmentGroup)
                .Include(x => x.ProjectEquipment)
                .ThenInclude(x => x.Equipment)
                .Include(x => x.KitCaseEquipments)
                .Where(x => x.ProjectEquipmentId == projectEquipmentId && x.IsRemoved != true).ToListAsync();

            return result;
        }

        public async Task MoveEquipment(ProjectEquipmentMovementEntity entity)
        {
            var res_ = _repos.Table.Any(x => entity.ProjectEquipmentId == entity.ProjectEquipmentId && x.MovementStatus == entity.MovementStatus);
            if (!res_)
            {
                await this.Create(entity);
            }
            else
            {
                var res = _repos.Table.First(x => x.ProjectId == entity.ProjectId && x.GroupId == entity.GroupId && entity.ProjectEquipmentId == entity.ProjectEquipmentId && x.MovementStatus == entity.MovementStatus);

                await this.Update(entity);
            }
        }

        public async Task MoveEquipments(List<ProjectEquipmentMovementEntity> entities)
        {
            List<ProjectEquipmentMovementEntity> entitiesForCreate = entities.Where(x => !_repos.Table.Any(r => x.ProjectId == r.ProjectId && x.GroupId == r.GroupId && x.EquipmentId == r.EquipmentId)).ToList();
            List<ProjectEquipmentMovementEntity> entitiesForUpdate = entities.Where(x => _repos.Table.Any(r => x.ProjectId == r.ProjectId && x.GroupId == r.GroupId && x.EquipmentId == r.EquipmentId)).ToList();

            if (entitiesForCreate.Any())
            {
                await this.Create(entitiesForCreate);
            }

            if (entitiesForUpdate.Any())
            {
                await this.Update(entitiesForUpdate);
            }
        }

        public async Task UpdateMoveEquipments(List<ProjectEquipmentMovementEntity> entities)
        {
            await this.Update(entities);
        }

        public async Task CreateMoveEquipments(List<ProjectEquipmentMovementEntity> entities)
        {
            await this.Create(entities);
        }
    }
}
