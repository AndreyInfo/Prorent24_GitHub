using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.Settings.PeriodicInspection;
using Prorent24.DAL.Storages.Equipment;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.Configuration.Settings;
using Prorent24.Entity.Equipment;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Equipment.PeriodicInspection
{
    internal class EquipmentPeriodicInspectionService : BaseService, IEquipmentPeriodicInspectionService
    {
        private readonly IPeriodicInspectionStorage _periodicInspectionStorage;
        private readonly IEquipmentPeriodicInspectionStorage _equipmentPeriodicInspectionStorage;

        public EquipmentPeriodicInspectionService(IHttpContextAccessor httpContextAccessor, 
            IPeriodicInspectionStorage periodicInspectionStorage, 
            IEquipmentPeriodicInspectionStorage equipmentPeriodicInspectionStorage, 
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage): base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._periodicInspectionStorage = periodicInspectionStorage ?? throw new ArgumentNullException(nameof(periodicInspectionStorage));
            this._equipmentPeriodicInspectionStorage = equipmentPeriodicInspectionStorage ?? throw new ArgumentNullException(nameof(equipmentPeriodicInspectionStorage));
        }

        public async Task<BaseListDto> GetPagedList(int equipmentId)
        {
            IPagedList<EquipmentPeriodicInspectionEntity> pagedList = await _equipmentPeriodicInspectionStorage.GetAllByForeignId(equipmentId);
            IPagedList<PeriodicInspectionEntity> periodicInspectionList = await _periodicInspectionStorage.GetAll();

            var inspections = from inspectionList in periodicInspectionList.Items
                              join equipmentInspectionList in pagedList.Items on inspectionList.Id equals equipmentInspectionList.PeriodicInspectionId
                              into savedInspections
                              from inspection in savedInspections.DefaultIfEmpty()
                              select new EquipmentPeriodicInspectionDto()
                              {
                                  Id = inspection == null ? 0 : inspection.Id,
                                  Name = inspectionList.Name,
                                  Frequency = inspectionList.FrequencyUnitType,
                                  Period = inspectionList.FrequencyInterval,
                                  Description = inspectionList.Description,
                                  EquipmentId = equipmentId,
                                  PeriodicInspectionId = inspectionList.Id,
                                  Active = inspection == null ? false : inspection.Active
                              };

            // List<EquipmentPeriodicInspectionEntity> listEntities = pagedList.Items.ToList();
            List<EquipmentPeriodicInspectionDto> listDtos = inspections.ToList(); //listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<EquipmentPeriodicInspectionDto>(await GetUserColumns(EntityEnum.EquipmentPeriodicInspectionEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.EquipmentPeriodicInspectionEntity
            };
        }

        public async Task<EquipmentPeriodicInspectionDto> GetByPeriodicInspectionId(int equipmentId, int periodicInspectionId)
        {
            EquipmentPeriodicInspectionEntity entity = _equipmentPeriodicInspectionStorage.GetByPeriodicInspection(equipmentId, periodicInspectionId);
            EquipmentPeriodicInspectionDto dto = entity == null ? new EquipmentPeriodicInspectionDto() { EquipmentId = equipmentId, PeriodicInspectionId = periodicInspectionId, Active = false } : entity.TransferToDto();

            return dto;
        }

        public async Task<EquipmentPeriodicInspectionDto> Save(EquipmentPeriodicInspectionDto dto)
        {
            EquipmentPeriodicInspectionEntity dbEntity = _equipmentPeriodicInspectionStorage.GetByPeriodicInspection(dto.EquipmentId, dto.PeriodicInspectionId);
            EquipmentPeriodicInspectionEntity transferEntity = dto.TransferToEntity();
            EquipmentPeriodicInspectionDto result;
            if (dbEntity != null)
            {
                dbEntity.Active = transferEntity.Active;
                EquipmentPeriodicInspectionEntity entity = await _equipmentPeriodicInspectionStorage.Update(dbEntity);
                result = entity.TransferToDto();
            }
            else
            {
                EquipmentPeriodicInspectionEntity entity = await _equipmentPeriodicInspectionStorage.Create(transferEntity);
                result = entity.TransferToDto();
            }
            return result;
        }
    }
}
