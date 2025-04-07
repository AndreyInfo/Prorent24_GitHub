using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Maintenance;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Maintenance.Inspection;
using Prorent24.Entity.Maintenance;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;
using Prorent24.BLL.Transfers.Maintenance.Inspection;
using System.Linq;
using Prorent24.BLL.Extentions;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;

namespace Prorent24.BLL.Services.Maintenance.Inspection
{
    internal class InspectionService : BaseService, IInspectionService
    {
        private readonly IInspectionStorage _inspectionStorage;

        public InspectionService(IHttpContextAccessor httpContextAccessor, 
            IInspectionStorage inspectionStorage, 
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _inspectionStorage = inspectionStorage;
        }

        public async Task<InspectionDto> Create(InspectionDto model)
        {
            InspectionEntity transferEntity = model.TransferToEntity();

            InspectionEntity entity = await _inspectionStorage.Create(transferEntity);
            InspectionDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _inspectionStorage.Delete(id);
            return result;
        }

        public async Task<InspectionDto> GetById(int id)
        {
            InspectionEntity entity = await _inspectionStorage.GetById(id);
            InspectionDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(int id)
        {
            InspectionEntity entity = await _inspectionStorage.GetById(id);
            InspectionDto dto = entity?.TransferToDto();
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<InspectionDto>(EntityEnum.InspectionEntity);

            return moduleDetails;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<InspectionEntity> pagedList = await _inspectionStorage.GetAllByFilter(filterList);
            List<InspectionEntity> listEntities = pagedList.Items.ToList();
            List<InspectionDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<InspectionDto>(await GetUserColumns(EntityEnum.InspectionEntity));

            return new BaseListDto()
            {

                Grid = grid,
                Entity = EntityEnum.InspectionEntity
            };
        }

        public async Task<InspectionDto> Update(InspectionDto model)
        {
            InspectionEntity transferEntity = model.TransferToEntity();
            InspectionEntity entity = await _inspectionStorage.Update(transferEntity);
            InspectionDto dto = entity.TransferToDto();
            return dto;
        }
    }
}
