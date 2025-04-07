using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Project;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;

namespace Prorent24.DAL.Storages.Project
{
    internal class ProjectEquipmentStorage : BaseStorage<ProjectEquipmentEntity>, IProjectEquipmentStorage
    {
        public ProjectEquipmentStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public override async Task<bool> Delete(int id)
        {
            //видаляємо спершу всі Children по групі
            var children = _repos.TableNoTracking.Where(z => z.ParentId == id);
            _repos.Delete(children);
            var entity = await _repos.FindAsync(id);
            if (entity != null)
            {
                await Task.Run(() =>
                {
                    _repos.Delete(entity);
                    _unitOfWork.SaveChanges();
                });
            }
            else
            {
                return false;
            }
            entity = await _repos.FindAsync(id);

            if (entity != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async override Task<ProjectEquipmentEntity> GetById(object id)
        {
            return await _repos.TableNoTracking
                .Include(x => x.Equipment)
                .ThenInclude(x => x.EquipmentContents)
                .ThenInclude(x => x.Content)
                .Include(x => x.ProjectEquipmentGroup)
                .Include(x => x.CreationUser)
                .Include(x=>x.Children)
                .ThenInclude(x => x.Equipment)
                .Include(x => x.Equipment)
                .ThenInclude(x => x.ProjectEquipments)
                .FirstOrDefaultAsync(x => x.Id == (int)id);
        }

        public async Task<ProjectEquipmentEntity> GetForEquipmentMovement(int projectId, int groupId, int projEquipmentId)
        {
            return await _repos.TableNoTracking
                .Include(x => x.Equipment)
                .Include(x => x.ProjectEquipmentGroup)
                .Include(x => x.CreationUser)
                .Include(x=>x.Children)
                .FirstOrDefaultAsync(x => x.ProjectId == projectId && x.ProjectEquipmentGroupId == groupId && x.Id == projEquipmentId);
        }

        public async Task<List<ProjectEquipmentEntity>> GetByGroupId(int id)
        {
            return await _repos.TableNoTracking
                .Include(x => x.Equipment)
                .Include(x => x.ProjectEquipmentGroup)
                .Include(x => x.CreationUser)
                .Where(x => x.ProjectEquipmentGroupId == id).ToListAsync();
        }

        public async Task<List<ProjectEquipmentEntity>> GetAllByProject(int projectId)
        {
            return await _repos.TableNoTracking
                .Include(x => x.Project)
                .Include(x => x.Equipment)
                .ThenInclude(x => x.ProjectEquipments)
                .Include(x => x.Equipment)
                .ThenInclude(x => x.EquipmentContents)
                .ThenInclude(x => x.Equipment)
                .Include(x => x.ProjectEquipmentGroup)
                .Include(x => x.CreationUser)
                .Include(x => x.Children)
                .ThenInclude(x => x.Equipment)
                .Where(x => !x.ParentId.HasValue && x.ProjectId.HasValue ? x.ProjectId.Value == projectId : false)
                .ToListAsync();
        }


        public async Task<List<ProjectEquipmentEntity>> GetAllByFilter(IQueryable<ProjectEquipmentEntity> queryableEntity, string searchText)
        {
            var projects = await queryableEntity.Include(c => c.ProjectEquipmentGroup).ThenInclude(y => y.Project).Include(x => x.Equipment).ToListAsync();
            if (searchText.Length > 0)
            {
                return projects.Where(x =>
                    x.Equipment?.Name?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                  || x.ProjectEquipmentGroup?.Project?.Name?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                  .ToList();
            }
            return projects;
        }
    }
}
