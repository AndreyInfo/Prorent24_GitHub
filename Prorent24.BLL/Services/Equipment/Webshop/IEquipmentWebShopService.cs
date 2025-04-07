using Prorent24.BLL.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Equipment.Webshop
{
    public interface IEquipmentWebShopService
    {
        Task<EquipmentWebShopDto> GetByEquipmentId(int id);
        Task<EquipmentWebShopDto> Save(EquipmentWebShopDto model);
    }
}
