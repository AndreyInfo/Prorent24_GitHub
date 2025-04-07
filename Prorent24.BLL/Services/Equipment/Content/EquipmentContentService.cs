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

namespace Prorent24.BLL.Services.Equipment.Content
{
    internal class EquipmentContentService : BaseService, IEquipmentContentService
    {

        private readonly IEquipmentContentStorage _equipmentContentStorage;

        public EquipmentContentService(IEquipmentContentStorage equipmentContentStorage, 
            IHttpContextAccessor httpContextAccessor, 
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._equipmentContentStorage = equipmentContentStorage ?? throw new ArgumentNullException(nameof(equipmentContentStorage));
        }

        public async Task<EquipmentContentDto> Create(EquipmentContentDto model)
        {
            EquipmentContentEntity transferEntity = model.TransferToEntity();

            var dbEntity = await _equipmentContentStorage.GetByKeys(model.EquipmentId, model.ContentId);
            if (dbEntity == null)
            {
                transferEntity.Quantity = model.Quantity;
                EquipmentContentEntity entity = await _equipmentContentStorage.Create(transferEntity);
            }
            else
            {
                dbEntity.Quantity = dbEntity.Quantity + model.Quantity;
                EquipmentContentEntity entity = await _equipmentContentStorage.Update(dbEntity);
                transferEntity.Id = dbEntity.Id;
            }

            EquipmentContentEntity result = await _equipmentContentStorage.GetById(transferEntity?.Id);
            EquipmentContentDto dto = result.TransferToDto();
            return dto;
        }
        public async Task<bool> Delete(int id)
        {
            bool result = await _equipmentContentStorage.Delete(id);
            return result;
        }
        public async Task<EquipmentContentDto> GetById(int id)
        {
            EquipmentContentEntity entity = await _equipmentContentStorage.GetById(id);
            EquipmentContentDto dto = entity?.TransferToDto();
            return dto;
        }

        [Obsolete]
        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseListDto> GetPagedList(int id)
        {
            IPagedList<EquipmentContentEntity> pagedList = await _equipmentContentStorage.GetAllByForeignId(id);
            List<EquipmentContentEntity> listEntities = pagedList.Items.ToList();
            List<EquipmentContentDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<EquipmentContentDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.EquipmentContentEntity
            };
        }

        public async Task<EquipmentContentDto> Update(EquipmentContentDto model)
        {
            EquipmentContentEntity transferEntity = model.TransferToEntity();
            EquipmentContentEntity entity = await _equipmentContentStorage.Update(transferEntity);
            EquipmentContentEntity result = await _equipmentContentStorage.GetById(model.Id);
            EquipmentContentDto dto = result.TransferToDto();
            return dto;
        }
    }
}
