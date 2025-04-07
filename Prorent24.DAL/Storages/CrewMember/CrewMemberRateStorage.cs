using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.CrewMember;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.CrewMember
{
    internal class CrewMemberRateStorage : BaseStorage<CrewMemberRateEntity>, ICrewMemberRateStorage
    {
        public CrewMemberRateStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<CrewMemberRateEntity>> GetAllByCrewMember(string CrewMemeberId)
        {
            var result = _repos.Table.Where(x => x.CrewMemberId == CrewMemeberId).Select(x => x).ToPagedListAsync(0, 100);
            return result;
        }

        public IQueryable<CrewMemberRateEntity> GetAll()
        {
            return _repos.Table;
        }

        public async override Task<bool> Delete(int id)
        {
            var entity = _repos.Table.Include(x => x.User).Where(x => x.Id == id).Select(x => x).FirstOrDefault();
            if (entity.User != null)
            {
                return false;
            }
            else
            {
                return await base.Delete(id);
            }

            //return await base.Delete(id);
        }

        public Task<IPagedList<CrewMemberRateEntity>> GetAll(List<Predicate<CrewMemberRateEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }
    }
}
