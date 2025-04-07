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
using Prorent24.Enum.Equipment;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Equipment.Stock
{
    internal class EquipmentStockService : BaseService, IEquipmentStockService
    {
        private readonly IEquipmentStorage _equipmentStorage;
        private readonly IEquipmentStockStorage _equipmentStockStorage;

        public EquipmentStockService(IEquipmentStorage equipmentStorage, 
            IEquipmentStockStorage equipmentStockStorage, 
            IHttpContextAccessor httpContextAccessor, 
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._equipmentStorage = equipmentStorage ?? throw new ArgumentNullException(nameof(equipmentStorage));
            this._equipmentStockStorage = equipmentStockStorage ?? throw new ArgumentNullException(nameof(equipmentStockStorage));
        }

        public async Task<EquipmentStockDto> Create(EquipmentStockDto model)
        {
            EquipmentStockEntity transferEntity = model.TransferToEntity();

            EquipmentStockEntity entityCreate = await _equipmentStockStorage.Create(transferEntity);
            EquipmentStockEntity entity = await _equipmentStockStorage.GetById(entityCreate.Id);

            if (entity != null)
            {
                this.TrackStock(entity.EquipmentId, entity.Quantity);
            }

            EquipmentStockDto dto = entity?.TransferToDto();
            // update quantity

            return dto;
        }
        public async Task<bool> Delete(int id)
        {
            EquipmentStockEntity dbEntity = await _equipmentStockStorage.GetById(id);
            // прибрати кількість яку внесли в поставку
            this.TrackStock(dbEntity.EquipmentId, dbEntity.Quantity * -1);

            bool result = await _equipmentStockStorage.Delete(id);
            return result;
        }
        public async Task<EquipmentStockDto> GetById(int id)
        {
            EquipmentStockEntity entity = await _equipmentStockStorage.GetById(id);
            EquipmentStockDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<EquipmentStockDto> Correct(EquipmentStockCorrectDto dto)
        {
            var equipmentEntity = await _equipmentStorage.GetById(dto.EquipmentId);
            EquipmentStockEntity stockEntity = dto.TransferToEntityFromCorrect();
            stockEntity.Quantity = dto.Quantity - (equipmentEntity.Quantity.HasValue ? equipmentEntity.Quantity.Value : 0);

            EquipmentStockEntity entityCreate = await _equipmentStockStorage.Create(stockEntity);
            EquipmentStockEntity entity = await _equipmentStockStorage.GetById(entityCreate.Id);

            if (entity != null)
            {
                this.TrackStock(entity.EquipmentId, entity.Quantity);
            }

            EquipmentStockDto result = entity?.TransferToDto();
            return result;
        }

        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseListDto> GetPagedList(int equipmentId)
        {
            IPagedList<EquipmentStockEntity> pagedList = await _equipmentStockStorage.GetAllByForeignId(equipmentId);
            List<EquipmentStockEntity> listEntities = pagedList.Items.ToList();
            List<EquipmentStockDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<EquipmentStockDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.EquipmentStockEntity
            };
        }

        public async Task<EquipmentStockDto> Update(EquipmentStockDto model)
        {
            EquipmentStockEntity dbEntity = await _equipmentStockStorage.GetById(model.Id);
            this.TrackStock(model.EquipmentId, dbEntity.Quantity * -1);

            EquipmentStockEntity transferEntity = model.TransferToEntity();
            dbEntity.TransferToEntityForUpdate(transferEntity);

            EquipmentStockEntity entityUpdate = await _equipmentStockStorage.Update(dbEntity);
            EquipmentStockEntity entity = await _equipmentStockStorage.GetById(entityUpdate.Id);

            this.TrackStock(entity.EquipmentId, entity.Quantity);

            EquipmentStockDto dto = entity?.TransferToDto();
            return dto;
        }

        private async void TrackStock(int equipmentId, int quantity)
        {
            EquipmentEntity entity = await _equipmentStorage.GetById(equipmentId);

            if (entity != null
               && entity.SupplyType == SupplyTypeEnum.Sale
               && entity.StockManagement == StockManagementEnum.TrackStock)
            {
                entity.Quantity += quantity;
                EquipmentEntity updateResult = await _equipmentStorage.Update(entity);
            }
        }
    }
}