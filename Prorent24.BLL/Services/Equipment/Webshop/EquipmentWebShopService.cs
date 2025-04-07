using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models.Equipment;
using Prorent24.DAL.Storages.Equipment;
using Prorent24.Entity.Equipment;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;

namespace Prorent24.BLL.Services.Equipment.Webshop
{
    internal class EquipmentWebShopService : BaseService, IEquipmentWebShopService
    {
        private readonly IEquipmentWebShopStorage _equipmentWebShopStorage;
        public EquipmentWebShopService(IHttpContextAccessor httpContextAccessor,
            IEquipmentWebShopStorage equipmentWebShopStorage, 
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _equipmentWebShopStorage = equipmentWebShopStorage;
        }
        public async Task<EquipmentWebShopDto> GetByEquipmentId(int id)
        {
            EquipmentWebShopEntity entity = _equipmentWebShopStorage.GetByEquipmentId(id);
            EquipmentWebShopDto dto = entity == null? new EquipmentWebShopDto() : entity.TransferToDto();
            return dto;
        }

        public async Task<EquipmentWebShopDto> Save(EquipmentWebShopDto model)
        {
            EquipmentWebShopEntity dbEntity =  _equipmentWebShopStorage.GetByEquipmentId(model.EquipmentId);
            EquipmentWebShopEntity transferEntity = model.TransferToEntity();
            EquipmentWebShopDto result;
            if (dbEntity != null)
            {
                EquipmentWebShopEntity entity = await _equipmentWebShopStorage.Update(transferEntity);
                result = entity.TransferToDto();
            }
            else
            {
                EquipmentWebShopEntity entity = await _equipmentWebShopStorage.Create(transferEntity);
                result = entity.TransferToDto();
            }
            return result;
        }
    }
}
