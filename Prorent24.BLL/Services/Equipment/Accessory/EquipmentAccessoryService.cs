using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Equipment;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.Equipment;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Equipment.Accessory
{
    internal class EquipmentAccessoryService : BaseService, IEquipmentAccessoryService
    {

        private readonly IEquipmentAccessoryStorage _equipmentAccessoryStorage;

        public EquipmentAccessoryService(IEquipmentAccessoryStorage equipmentAccessoryStorage, 
            IHttpContextAccessor httpContextAccessor, 
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._equipmentAccessoryStorage = equipmentAccessoryStorage ?? throw new ArgumentNullException(nameof(equipmentAccessoryStorage));
        }

        public async Task<EquipmentAccessoryDto> Create(EquipmentAccessoryDto model)
        {
            EquipmentAccessoryEntity transferEntity = model.TransferToEntity();

            var dbEntity = await _equipmentAccessoryStorage.GetByKeys(model.EquipmentId, model.AccessoryId);
            if (dbEntity == null)
            {
                EquipmentAccessoryEntity entity = await _equipmentAccessoryStorage.Create(transferEntity);
            }
            else
            {
                dbEntity.Quantity = dbEntity.Quantity + model.Quantity;
                EquipmentAccessoryEntity entity = await _equipmentAccessoryStorage.Update(dbEntity);
                transferEntity.Id = dbEntity.Id;
            }

            EquipmentAccessoryEntity result = await _equipmentAccessoryStorage.GetById(transferEntity?.Id);
            EquipmentAccessoryDto dto = result.TransferToDto();
            return dto;
        }
        public async Task<bool> Delete(int id)
        {
            bool result = await _equipmentAccessoryStorage.Delete(id);
            return result;
        }
        public async Task<EquipmentAccessoryDto> GetById(int id)
        {
            EquipmentAccessoryEntity entity = await _equipmentAccessoryStorage.GetById(id);
            EquipmentAccessoryDto dto = entity?.TransferToDto();
            return dto;
        }


        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseListDto> GetPagedList(int equipmentId)
        {
            IPagedList<EquipmentAccessoryEntity> pagedList = await _equipmentAccessoryStorage.GetAllByForeignId(equipmentId);
            List<EquipmentAccessoryEntity> listEntities = pagedList.Items.ToList();
            List<EquipmentAccessoryDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<EquipmentAccessoryDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.EquipmentAccessoryEntity
            };
        }

        public async Task<EquipmentAccessoryDto> Update(EquipmentAccessoryDto model)
        {
            EquipmentAccessoryEntity transferEntity = model.TransferToEntity();
            EquipmentAccessoryEntity entity = await _equipmentAccessoryStorage.Update(transferEntity);
            EquipmentAccessoryEntity result = await _equipmentAccessoryStorage.GetById(transferEntity?.Id);
            EquipmentAccessoryDto dto = result.TransferToDto();
            return dto;
        }
    }
}
