using Microsoft.EntityFrameworkCore;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.General;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Address
{
    internal class ColumnStorage : BaseStorage<UserColumnEntity>, IColumnStorage
    {
        public ColumnStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

       public async Task<List<UserColumnEntity>> GetColumnsByEntity(EntityEnum entity, string userId)
        {
            var result = await _repos.TableNoTracking.Where(x => x.Entity == entity && x.CreationUserId == userId).ToListAsync();
            result = result.OrderBy(x => x.LastModifiedDate).ThenBy(x => x.Order).ToList();
            return result;
        }

        public async Task<UserColumnEntity> GetColumn(EntityEnum entity, string userId, string column)
        {
            return await _repos.TableNoTracking.Where(x => x.Entity == entity && x.CreationUserId == userId && x.Column == column.Trim()).FirstOrDefaultAsync();
        }

        #region Don't use

        [Obsolete("Don't use")]

        public Task<IPagedList<ColumnEntity>> GetAll(List<Predicate<ColumnEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        Task<List<UserColumnEntity>> IColumnStorage.GetListByEntity(EntityEnum entity)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<UserColumnEntity>> GetAll(List<Predicate<UserColumnEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
