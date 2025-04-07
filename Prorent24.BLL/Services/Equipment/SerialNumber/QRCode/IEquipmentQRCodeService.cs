using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Services.Abstract;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Equipment.SerialNumber.QRCode
{
    public interface IEquipmentQRCodeService : IBaseService<EquipmentQRCodeDto, int>
    {
        Task<BaseListDto> GetPagedList(int equipmentId, int? serialNumberId);
    }
}
