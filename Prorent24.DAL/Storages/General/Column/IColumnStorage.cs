using Prorent24.Entity.General;
using Prorent24.Enum.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Column
{
    public interface IColumnStorage : IBaseStorage<UserColumnEntity>
    {
        Task<List<UserColumnEntity>> GetColumnsByEntity(EntityEnum entity, string userId);

        Task<UserColumnEntity> GetColumn(EntityEnum entity, string userId, string column);

        Task<List<UserColumnEntity>> GetListByEntity(EntityEnum entity);
    }
}
