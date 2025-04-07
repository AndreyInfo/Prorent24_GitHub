using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

namespace Prorent24.BLL.Services.Equipment.SerialNumber
{
    internal class EquipmentSerialNumberService : BaseService, IEquipmentSerialNumberService
    {
        private readonly IEquipmentSerialNumberStorage _equipmentSerialNumberStorage;
        private readonly IEquipmentQRCodeStorage _qRCodeStorage;

        public EquipmentSerialNumberService(IEquipmentSerialNumberStorage equipmentSerialNumberStorage,
            IEquipmentQRCodeStorage qRCodeStorage,
            IHttpContextAccessor httpContextAccessor,
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._equipmentSerialNumberStorage = equipmentSerialNumberStorage ?? throw new ArgumentNullException(nameof(equipmentSerialNumberStorage));
            this._qRCodeStorage = qRCodeStorage ?? throw new ArgumentNullException(nameof(qRCodeStorage));
        }
        #region SERIAL NUMBERS
        public async Task<EquipmentSerialNumberDto> Create(EquipmentSerialNumberDto model)
        {
            EquipmentSerialNumberEntity transferEntity = model.TransferToEntity();

            EquipmentSerialNumberEntity entity = await _equipmentSerialNumberStorage.Create(transferEntity);
            EquipmentSerialNumberDto dto = entity.TransferToDto();
            return dto;
        }
        public async Task<bool> Delete(int id)
        {
            bool result = await _equipmentSerialNumberStorage.Delete(id);
            return result;
        }

        public async Task<EquipmentSerialNumberDto> GetById(int id)
        {
            EquipmentSerialNumberEntity entity = await _equipmentSerialNumberStorage.GetById(id);
            EquipmentSerialNumberDto dto = entity?.TransferToDto();
            return dto;
        }

        [Obsolete]
        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseListDto> GetPagedList(int equipmentId)
        {
            IPagedList<EquipmentSerialNumberEntity> pagedList = await _equipmentSerialNumberStorage.GetAllByForeignId(equipmentId);
            List<EquipmentSerialNumberEntity> listEntities = pagedList.Items.ToList();
            List<EquipmentSerialNumberDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<EquipmentSerialNumberDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.EquipmentSerialNumberEntity
            };
        }

        public async Task<EquipmentSerialNumberDto> Update(EquipmentSerialNumberDto model)
        {
            EquipmentSerialNumberEntity transferEntity = model.TransferToEntity();
            EquipmentSerialNumberEntity entity = await _equipmentSerialNumberStorage.Update(transferEntity);
            EquipmentSerialNumberDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<int?> GetBySerialNumber(string serialNumber)
        {
            serialNumber = serialNumber.ToLower().Trim();
            EquipmentSerialNumberEntity equipmentSerialNumber = await _equipmentSerialNumberStorage.QueryableEntity
                                                                                             .Where(x => x.SerialNumber.ToLower().Trim().Equals(serialNumber))
                                                                                             .FirstOrDefaultAsync();
            return equipmentSerialNumber?.EquipmentId;
        }

        #endregion
    }
}
