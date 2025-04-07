using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Services.Maintenance.Inspection.SerialNumber;
using Prorent24.BLL.Transfers.Maintenance.InspectionSerialNumber;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Maintenance.Inspection;
using Prorent24.Entity.Maintenance;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Maintenance.InspectionSerialNumber.SerialNumber
{
    internal class InspectionSerialNumberService: BaseService, IInspectionSerialNumberService
    {
        private readonly IInspectionSerialNumberStorage _inspectionSerialNumberStorage;

        public InspectionSerialNumberService(IHttpContextAccessor httpContextAccessor, 
            IInspectionSerialNumberStorage inspectionSerialNumberStorage, 
            IUserRoleStorage userRoleStorag,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorag, сolumnStorage)
        {
            this._inspectionSerialNumberStorage = inspectionSerialNumberStorage;
        }

        public async Task<InspectionSerialNumberDto> Create(InspectionSerialNumberDto model)
        {
            InspectionSerialNumberEntity transferEntity = model.TransferToEntity();

            InspectionSerialNumberEntity entity = await _inspectionSerialNumberStorage.Create(transferEntity);
            InspectionSerialNumberDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _inspectionSerialNumberStorage.Delete(id);
            return result;
        }

        public async Task<InspectionSerialNumberDto> GetById(int id)
        {
            InspectionSerialNumberEntity entity = await _inspectionSerialNumberStorage.GetById(id);
            InspectionSerialNumberDto dto = entity?.TransferToDto();
            return dto;
        }

        [Obsolete]
        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new Exception();
        }

        public async Task<BaseListDto> GetPagedList(int id)
        {
            IPagedList<InspectionSerialNumberEntity> pagedList = await _inspectionSerialNumberStorage.GetAllByForeignId(id);
            List<InspectionSerialNumberEntity> listEntities = pagedList.Items.ToList();
            List<InspectionSerialNumberDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<InspectionSerialNumberDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.InspectionSerialNumberEntity
            };
        }

        public async Task<InspectionSerialNumberDto> Update(InspectionSerialNumberDto model)
        {
            InspectionSerialNumberEntity transferEntity = model.TransferToEntity();
            InspectionSerialNumberEntity entity = await _inspectionSerialNumberStorage.Update(transferEntity);
            InspectionSerialNumberDto dto = entity.TransferToDto();
            return dto;
        }
    }
}
