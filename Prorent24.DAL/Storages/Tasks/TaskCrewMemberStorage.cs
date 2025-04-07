using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Tasks;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Tasks
{
    internal class TaskCrewMemberStorage : BaseStorage<TaskVisibilityCrewMemberEntity>, ITaskCrewMemberStorage
    {
        public TaskCrewMemberStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async override Task<TaskVisibilityCrewMemberEntity> Create(TaskVisibilityCrewMemberEntity model)
        {
            TaskVisibilityCrewMemberEntity result = await base.Create(model);
            return await _repos.Table.Include(x => x.CrewMember).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAllByTaskId(int Id)
        {
            /// <summary>
            /// Delete Entity
            /// </summary>
            /// <returns></returns>
            var entities = await _repos.Table.Where(x => x.TaskId == Id).Select(x => x).ToListAsync();
            if (entities != null)
            {
                await Task.Run(() =>
                {
                    _repos.Delete(entities);
                    _unitOfWork.SaveChanges();
                });

            }
            else
            {
                return false;
            }

            entities = await _repos.Table.Where(x => x.TaskId == Id).Select(x => x).ToListAsync();

            if (entities != null || entities.Count > 0)
            {
                return false;
            }
            else
            {
                return true;

            }
        }

        public Task<IPagedList<TaskVisibilityCrewMemberEntity>> GetAll(List<Predicate<TaskVisibilityCrewMemberEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }
    }
}
