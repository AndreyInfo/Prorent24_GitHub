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

namespace Prorent24.BLL.Services.Equipment.Alternative
{
    internal class EquipmentAlternativeService : BaseService, IEquipmentAlternativeService
    {

        private readonly IEquipmentAlternativeStorage _equipmentAlternativeStorage;

        public EquipmentAlternativeService(IEquipmentAlternativeStorage equipmentAlternativeStorage, 
            IHttpContextAccessor httpContextAccessor, 
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._equipmentAlternativeStorage = equipmentAlternativeStorage ?? throw new ArgumentNullException(nameof(equipmentAlternativeStorage));
        }

        public async Task<EquipmentAlternativeDto> Create(EquipmentAlternativeDto model)
        {
            EquipmentAlternativeEntity transferEntity = model.TransferToEntity();
            var dbEntity = await _equipmentAlternativeStorage.GetByKeys(model.EquipmentId, model.AlternativeId);
            if (dbEntity == null)
            {
                EquipmentAlternativeEntity entityCreate = await _equipmentAlternativeStorage.Create(transferEntity);
                EquipmentAlternativeEntity entity = await _equipmentAlternativeStorage.GetById(entityCreate.Id);
                EquipmentAlternativeDto dto = entity?.TransferToDto();
                return dto;
            }

            return dbEntity.TransferToDto();
        }
        public async Task<bool> Delete(int id)
        {
            bool result = await _equipmentAlternativeStorage.Delete(id);
            return result;
        }
        public async Task<EquipmentAlternativeDto> GetById(int id)
        {
            EquipmentAlternativeEntity entity = await _equipmentAlternativeStorage.GetById(id);
            EquipmentAlternativeDto dto = entity?.TransferToDto();
            return dto;
        }

        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseListDto> GetPagedList(int equipmentId)
        {
            IPagedList<EquipmentAlternativeEntity> pagedList = await _equipmentAlternativeStorage.GetAllByForeignId(equipmentId);
            List<EquipmentAlternativeEntity> listEntities = pagedList.Items.ToList();
            List<EquipmentAlternativeDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<EquipmentAlternativeDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.EquipmentAlternativeEntity
            };
        }

        public async Task<EquipmentAlternativeDto> Update(EquipmentAlternativeDto model)
        {
            EquipmentAlternativeEntity transferEntity = model.TransferToEntity();
            EquipmentAlternativeEntity entityUpdate = await _equipmentAlternativeStorage.Update(transferEntity);
            EquipmentAlternativeEntity entity = await _equipmentAlternativeStorage.GetById(entityUpdate.Id);
            EquipmentAlternativeDto dto = entity?.TransferToDto();
            return dto;
        }
    }
}
