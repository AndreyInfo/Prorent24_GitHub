using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Settings.PeriodicInspection;
using Prorent24.Entity.Configuration.Settings;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using Prorent24.BLL.Transfers.Configuration.Settings;
using System.Linq;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.Common.Models.Filters;

namespace Prorent24.BLL.Services.Configuration.Settings.PeriodicInspection
{
    internal class PeriodicInspectionService : IPeriodicInspectionService
    {
        private readonly IPeriodicInspectionStorage _periodicInspectionStorage;

        public PeriodicInspectionService(IPeriodicInspectionStorage projectInspectionStorage)
        {
            _periodicInspectionStorage = projectInspectionStorage;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<PeriodicInspectionEntity> pagedList = await _periodicInspectionStorage.GetAll();
            List<PeriodicInspectionEntity> listEntities = pagedList.Items.ToList();
            List<PeriodicInspectionDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<PeriodicInspectionDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.PeriodicInspectionEntity
            };
        }

        public async Task<PeriodicInspectionDto> GetById(int id)
        {
            PeriodicInspectionEntity entity = await _periodicInspectionStorage.GetById(id);
            PeriodicInspectionDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<PeriodicInspectionDto> Create(PeriodicInspectionDto model)
        {
            PeriodicInspectionEntity transferEntity = model.TransferToEntity();
            PeriodicInspectionEntity entity = await _periodicInspectionStorage.Create(transferEntity);
            PeriodicInspectionDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<PeriodicInspectionDto> Update(PeriodicInspectionDto model)
        {
            PeriodicInspectionEntity transferEntity = model.TransferToEntity();
            PeriodicInspectionEntity entity = await _periodicInspectionStorage.Update(transferEntity);
            PeriodicInspectionDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _periodicInspectionStorage.Delete(id);
            return result;
        }

        public async Task<bool> DeleteMultiple(List<int> Values)
        {
            bool result = await _periodicInspectionStorage.Delete(Values);
            return result;
        }
    }
}
