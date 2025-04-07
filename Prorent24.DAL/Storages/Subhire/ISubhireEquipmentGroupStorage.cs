using Prorent24.Entity.Subhire;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Subhire
{
    public interface ISubhireEquipmentGroupStorage : IBaseStorage<SubhireEquipmentGroupEntity>
    {
        Task<List<SubhireEquipmentGroupEntity>> GetAllBySubhire(int subhireId);

    }
}
