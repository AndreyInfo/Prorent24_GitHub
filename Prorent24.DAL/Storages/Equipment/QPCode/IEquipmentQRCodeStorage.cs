using Prorent24.Entity.Equipment;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Equipment
{
    public interface IEquipmentQRCodeStorage : IBaseStorage<EquipmentQRCodeEntity>
    {
        Task<IPagedList<EquipmentQRCodeEntity>> GetAllByForeignId(int equipmentId, int? serialNumberId);
    }
}
