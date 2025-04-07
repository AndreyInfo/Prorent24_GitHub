using Prorent24.Entity.CrewMember;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.CrewMember
{
    public interface ICrewMemberRateStorage : IBaseStorage<CrewMemberRateEntity>
    {
        Task<IPagedList<CrewMemberRateEntity>> GetAllByCrewMember(string CrewMemeberId);
    }
}
