using Prorent24.Entity.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Tasks
{
    public interface ITaskCrewMemberStorage: IBaseStorage<TaskVisibilityCrewMemberEntity>
    {
        Task<bool> DeleteAllByTaskId(int Id);
    }
}
