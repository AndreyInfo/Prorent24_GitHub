using Prorent24.Entity.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Subhire
{
    public interface ISubhireEquipmentStorage
    {
        Task<SubhireEquipmentEntity> Create(SubhireEquipmentEntity model);

        Task<SubhireEquipmentEntity> Update(SubhireEquipmentEntity model);

        Task<bool> Delete(int id);

        Task<SubhireEquipmentEntity> GetById(object id);

        Task<List<SubhireEquipmentEntity>> GetAllBySubhire(int subhireId);

        Task<List<SubhireEquipmentEntity>> GetAllByFilter(IQueryable<SubhireEquipmentEntity> queryableEntity);

        IQueryable<SubhireEquipmentEntity> QueryableEntity { get; }

    }
}
