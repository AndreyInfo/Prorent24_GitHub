using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Equipment;
using Prorent24.Entity.Equipment;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.Common.Extentions;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;

namespace Prorent24.BLL.Services.Equipment.Supplier
{
    internal class EquipmentSupplierService: BaseService,  IEquipmentSupplierService
    {
        private readonly IEquipmentSupplierStorage _equipmentSupplierStorage;

        public EquipmentSupplierService(IEquipmentSupplierStorage equipmentSupplierStorage, 
            IHttpContextAccessor httpContextAccessor, 
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _equipmentSupplierStorage = equipmentSupplierStorage ?? throw new ArgumentNullException(nameof(equipmentSupplierStorage));
        }

        public async Task<EquipmentSupplierDto> Create(EquipmentSupplierDto model)
        {
            EquipmentSupplierEntity transferEntity = model.TransferToEntity();

            EquipmentSupplierEntity entity = await _equipmentSupplierStorage.Create(transferEntity);
            entity = await _equipmentSupplierStorage.GetById(entity.Id);
            EquipmentSupplierDto dto = entity.TransferToDto();
            return dto;
        }
        public async Task<bool> Delete(int id)
        {
            bool result = await _equipmentSupplierStorage.Delete(id);
            return result;
        }
        public async Task<EquipmentSupplierDto> GetById(int id)
        {
            EquipmentSupplierEntity entity = await _equipmentSupplierStorage.GetById(id);
            EquipmentSupplierDto dto = entity?.TransferToDto();
            return dto;
        }

        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseListDto> GetPagedList(int id)
        {
            IPagedList<EquipmentSupplierEntity> pagedList = await _equipmentSupplierStorage.GetAllByForeignId(id);
            List<EquipmentSupplierEntity> listEntities = pagedList.Items.ToList();
            List<EquipmentSupplierDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<EquipmentSupplierDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.EquipmentSupplierEntity
            };
        }

        public async Task<EquipmentSupplierDto> Update(EquipmentSupplierDto model)
        {
            EquipmentSupplierEntity transferEntity = model.TransferToEntity();
            EquipmentSupplierEntity entity = await _equipmentSupplierStorage.Update(transferEntity);
            entity = await _equipmentSupplierStorage.GetById(entity.Id);
            EquipmentSupplierDto dto = entity.TransferToDto();
            return dto;
        }
    }
}
