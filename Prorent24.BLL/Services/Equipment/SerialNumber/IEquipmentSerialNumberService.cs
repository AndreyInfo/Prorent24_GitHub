using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Services.Abstract;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Equipment.SerialNumber
{
    public interface IEquipmentSerialNumberService: IBaseService<EquipmentSerialNumberDto, int>, IForeignDependencyService<EquipmentSerialNumberDto>
    {
        Task<int?> GetBySerialNumber(string serialNumber);
    }
}
