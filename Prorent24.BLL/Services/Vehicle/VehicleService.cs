using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.BLL.Transfers.Vehicle;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Vehicle;
using Prorent24.Entity.Vehicle;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prorent24.BLL.Builders;

namespace Prorent24.BLL.Services.Vehicle
{
    internal class VehicleService : BaseService, IVehicleService
    {
        private readonly IVehicleStorage _vehicleStorage;
        private readonly IVehicleCrewMemberStorage _vehicleCrewMemberStorage;


        public VehicleService(IVehicleStorage vehicleStorage,
            IVehicleCrewMemberStorage vehicleCrewMemberStorage,
            IUserRoleStorage userRoleStorage,
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _vehicleStorage = vehicleStorage;
            _vehicleCrewMemberStorage = vehicleCrewMemberStorage;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Vehicles, PermissionFieldEnum.DatabaseVehicle);
            string userId = GetUserId();
            string searchText;
            IQueryable<VehicleEntity> queryableEntity = _vehicleStorage.QueryableEntity.CreateFilterForVehicleEntity(filterList, out searchText);
            IPagedList<VehicleEntity> pagedList = await _vehicleStorage.GetAllByFilter(queryableEntity, searchText, userId);

            //IPagedList<VehicleEntity> pagedList = await _vehicleStorage.GetAllByFilter(filterList, userId);
            List<VehicleEntity> listEntities = pagedList.Items.ToList();
            List<VehicleDto> listDtos = listEntities.TransferToListDto(permission);
            BaseGrid grid = listDtos.CreateGrid<VehicleDto>(await GetUserColumns(EntityEnum.VehicleEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.VehicleEntity,
                Add = permission.Add,
                Edit = permission.Edit,
                Delete = permission.Delete
            };
        }

        public async Task<VehicleDto> GetById(int id)
        {
            VehicleEntity entity = await _vehicleStorage.GetById(id);
            VehicleDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(int id)
        {
            VehicleEntity entity = await _vehicleStorage.GetDetails(id);
            VehicleDto dto = entity?.TransferToDto();
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<VehicleDto>(EntityEnum.VehicleEntity);

            return moduleDetails;
        }

        public async Task<VehicleDto> Create(VehicleDto model)
        {
            var permission = await this.GetModulePermission(ModuleTypeEnum.Vehicles);
            if (permission.Add)
            {
                VehicleEntity transferEntity = model.TransferToEntity();
                transferEntity.CrewMembers = model.CrewMembers.TransferToVehicleVisibilityEntity();
                VehicleEntity entity = await _vehicleStorage.Create(transferEntity);
                VehicleDto dto = entity.TransferToDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<VehicleDto> Update(VehicleDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Vehicles, PermissionFieldEnum.DatabaseVehicle);
            if (permission.Edit)
            {
                VehicleEntity transferEntity = model.TransferToEntity();
                VehicleEntity entity = await _vehicleStorage.Update(transferEntity);
                VehicleDto dto = entity.TransferToDto();
                if (model.CrewMembers != null)
                {
                    var isDeleted = await this._vehicleCrewMemberStorage.DeleteAllByVehicleId(dto.Id);

                    List<VehicleVisibilityCrewMemberEntity> visibilityTransferEntities = model.CrewMembers.TransferToVehicleVisibilityEntity();
                    List<CrewMemberShortDto> crewMembersList = new List<CrewMemberShortDto>();

                    foreach (var _entity in visibilityTransferEntities)
                    {
                        _entity.VehicleId = entity.Id;
                        VehicleVisibilityCrewMemberEntity visibilityEntity = await this._vehicleCrewMemberStorage.Create(_entity);
                        var transferedDto = visibilityEntity.TransferToDto();
                        crewMembersList.Add(transferedDto);
                    }

                    dto.CrewMembers = crewMembersList;
                }
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task Import(List<VehicleDto> vehicleDtos)
        {
            List<VehicleEntity> entities = vehicleDtos.TransferToListEntity();
            await _vehicleStorage.Import(entities);
        }

        public async Task<bool> Delete(int id)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Vehicles, PermissionFieldEnum.DatabaseVehicle);

            if (permission.Delete)
            {
                bool result = await _vehicleStorage.Delete(id);
                return result;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<BaseListDto> GetForPlanning()
        {
            string userId = GetUserId();
            List<VehicleEntity> entities = await _vehicleStorage.GetForPlanning(userId);
            List<VehiclePlanningDto> list = entities.TransferToListForPlanningDto();
            BaseGrid grid = list.CreateGrid<VehiclePlanningDto>();
            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.ProjectPlanningVehicle
            };
        }

        public async Task<string> Export(string[] columns, List<SelectedFilter> filterList)
        {
            string userId = GetUserId();

            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Equipment, PermissionFieldEnum.DatabaseEquipment);

            IPagedList<VehicleEntity> pagedList = await _vehicleStorage.GetAllByFilter(filterList, userId);
            List<VehicleEntity> listEntities = pagedList.Items.ToList();
            List<VehicleDto> listDtos = listEntities.TransferToListDto(permission);

            List<Column> sheetColumns = new List<Column>();
            sheetColumns.AddColumns<VehicleDto>(columns);

            if (sheetColumns.Count > 0)
            {
                // listDtos.
                List<Dictionary<string, object>> Data = new List<Dictionary<string, object>>();
                Data.AddData<VehicleDto>(listDtos);
                string path = sheetColumns.CreatExcelWorksheetWithHeader(Data, $"VehicleExport{DateTime.Now.ToString("yyyy-MM-dd")}", "");
                return path;            
            }
            return string.Empty;
        }
    }
}
