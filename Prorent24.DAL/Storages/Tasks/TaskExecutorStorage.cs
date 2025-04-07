using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Tasks;
using Prorent24.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Tasks
{
    internal class TaskExecutorStorage: BaseStorage<TaskExecutorCrewMemberEntity>, ITaskExecutorStorage
    {
        public TaskExecutorStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async override Task<TaskExecutorCrewMemberEntity> Create(TaskExecutorCrewMemberEntity model)
        {
            TaskExecutorCrewMemberEntity result = await base.Create(model);
            return await _repos.Table.Include(x => x.CrewMember).Where(x => x.CrewMemberId == result.CrewMemberId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAllByTaskId(int Id)
        {
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

       
    }
}
