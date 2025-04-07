using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Transfers.Maintenance.Repair;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Maintenance.Repair;
using Prorent24.Entity.Maintenance;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Maintenance.Repair
{
    internal class RepairService : BaseService, IRepairService
    {
        private readonly IRepairStorage _repairStorage;
        public RepairService(IHttpContextAccessor httpContextAccessor, 
            IRepairStorage repairStorage, 
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._repairStorage = repairStorage;
        }

        public async Task<RepairDto> Create(RepairDto model)
        {
            RepairEntity transferEntity = model.TransferToEntity();

            RepairEntity entity = await _repairStorage.Create(transferEntity);
            RepairDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _repairStorage.Delete(id);
            return result;
        }

        public async Task<RepairDto> GetById(int id)
        {
            RepairEntity entity = await _repairStorage.GetById(id);
            RepairDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(int id)
        {
            RepairEntity entity = await _repairStorage.GetById(id);
            RepairDto dto = entity?.TransferToDto();
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<RepairDto>(EntityEnum.RepairEntity);

            return moduleDetails;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<RepairEntity> pagedList = await _repairStorage.GetAllByFilter(filterList);
            List<RepairEntity> listEntities = pagedList.Items.ToList();
            List<RepairDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<RepairDto>(await GetUserColumns(EntityEnum.RepairEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.RepairEntity
            };
        }

        public async Task<RepairDto> Update(RepairDto model)
        {
            RepairEntity transferEntity = model.TransferToEntity();
            RepairEntity entity = await _repairStorage.Update(transferEntity);
            RepairDto dto = entity.TransferToDto();
            return dto;
        }
    }
}
