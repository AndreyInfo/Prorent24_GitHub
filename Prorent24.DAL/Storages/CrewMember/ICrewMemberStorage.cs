using Prorent24.Common.Models.Filters;
using Prorent24.Entity.CrewMember;
using Prorent24.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.CrewMember
{
    public interface ICrewMemberStorage: IBaseStorage<CrewMemberEntity>
    {
        Task<IPagedList<CrewMemberEntity>> GetAllByFilter(IQueryable<CrewMemberEntity> queryableEntity, string searchText);
        Task<List<CrewMemberEntity>> GetList();
        Task<List<CrewMemberEntity>> GetCrewMembers();
        Task<List<CrewMemberEntity>> GetList(List<string> ids);
        bool ExistsByRoleId(string roleId);
    }
}
