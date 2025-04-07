using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Prorent24.BLL.Services.Equipment.SerialNumber.QRCode
{
    internal class EquipmentQRCodeService : BaseService, IEquipmentQRCodeService
    {
        private readonly IEquipmentQRCodeStorage _equipmentQRCodeStorage;

        public EquipmentQRCodeService(IEquipmentQRCodeStorage equipmentQRCodeStorage, 
            IHttpContextAccessor httpContextAccessor, 
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._equipmentQRCodeStorage = equipmentQRCodeStorage ?? throw new ArgumentNullException(nameof(equipmentQRCodeStorage));
        }

        #region SERIAL NUMBERS QRCodes
        public async Task<EquipmentQRCodeDto> Create(EquipmentQRCodeDto model)
        {
            EquipmentQRCodeEntity transferEntity = model.TransferToEntity();

            EquipmentQRCodeEntity entity = await _equipmentQRCodeStorage.Create(transferEntity);
            EquipmentQRCodeDto dto = entity.TransferToDto();
            return dto;
        }
        public async Task<bool> Delete(int id)
        {
            bool result = await _equipmentQRCodeStorage.Delete(id);
            return result;
        }

        public async Task<EquipmentQRCodeDto> GetById(int id)
        {
            EquipmentQRCodeEntity entity = await _equipmentQRCodeStorage.GetById(id);
            EquipmentQRCodeDto dto = entity?.TransferToDto();
            return dto;
        }

        [Obsolete]
        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseListDto> GetPagedList(int equipmentId, int? serialNumberId)
        {
            IPagedList<EquipmentQRCodeEntity> pagedList = await _equipmentQRCodeStorage.GetAllByForeignId(equipmentId, serialNumberId);
            List<EquipmentQRCodeEntity> listEntities = pagedList.Items.ToList();
            List<EquipmentQRCodeDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<EquipmentQRCodeDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.EquipmentSerialNumberQRCodeEntity
            };
        }

        public async Task<EquipmentQRCodeDto> Update(EquipmentQRCodeDto model)
        {
            EquipmentQRCodeEntity transferEntity = model.TransferToEntity();
            EquipmentQRCodeEntity entity = await _equipmentQRCodeStorage.Update(transferEntity);
            EquipmentQRCodeDto dto = entity.TransferToDto();
            return dto;
        }

        #endregion
    }
}
