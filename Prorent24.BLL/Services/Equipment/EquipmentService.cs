using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prorent24.BLL.Builders;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Services.Configuration.Financial.FinancialSetting;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Equipment;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.Equipment;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Equipment
{
    internal class EquipmentService : BaseService, IEquipmentService
    {
        private readonly IEquipmentStorage _equipmentStorage;
        private readonly IFinancialSettingService _financialSettingService;
        public EquipmentService(IEquipmentStorage equipmentStorage,
            IFinancialSettingService financialSettingService,
            IHttpContextAccessor httpContextAccessor,
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _equipmentStorage = equipmentStorage;
            _financialSettingService = financialSettingService;
        }

        public async Task<BaseListDto> GetEquipmentGroupsByFolder(List<SelectedFilter> filterList)
        {
            string search = filterList.FirstOrDefault(x => x.FilterType == "SearchText")?.Values?.FirstOrDefault().ToString();
            IPagedList<EquipmentEntity> pagedList = await _equipmentStorage.GetAll();
            List<EquipmentEntity> listEntities = pagedList.Items.ToList();
           
            if(search!=null && search.Length > 0)
            {
                listEntities = listEntities.Where(x => x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            List<EquipmentGroupGridDto> listDtos = listEntities.TransferToListGroupDto();
            BaseGrid grid = listDtos.CreateGrid<EquipmentGroupGridDto>(await GetUserColumns(EntityEnum.EquipmentEntity));
            grid.DataGroups = listDtos.GroupBy(x=>x.Category).Select(x=>new
            {
                Key = x.Key,
                Values = x.Take(500).ToList()
            });
            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.EquipmentEntity
            };
        }

        public async Task<EquipmentDto> Create(EquipmentDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Equipment, PermissionFieldEnum.DatabaseEquipment);
            if (permission.Add)
            {
                EquipmentEntity transferEntity = model.TransferToEntity();
                transferEntity.Code = string.IsNullOrWhiteSpace(transferEntity.Code) ? GeneratorExtention.GenerateUniqueCode() : transferEntity.Code;
                EquipmentEntity entity = await _equipmentStorage.Create(transferEntity);
                EquipmentDto dto = entity.TransferToDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Equipment, PermissionFieldEnum.DatabaseEquipment);
            if (permission.Delete)
            {
                bool result = await _equipmentStorage.Delete(id);
                return result;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<EquipmentDto> GetById(int id)
        {
            EquipmentEntity entity = await _equipmentStorage.GetById(id);
            EquipmentDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(int id)
        {
            EquipmentEntity entity = await _equipmentStorage.GetById(id);
            EquipmentDto dto = entity?.TransferToDto();
            FinancialSettingDto financialSetting = await _financialSettingService.GetAdvanced();
            dto?.BuildEquipmentSettings(financialSetting);
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<EquipmentDto>(EntityEnum.EquipmentEntity, GetUserId());

            return moduleDetails;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Equipment, PermissionFieldEnum.DatabaseEquipment);
            string searchText;
            IQueryable<EquipmentEntity> queryableEntity = _equipmentStorage.QueryableEntity.CreateFilterForEquipmentEntity(filterList, out searchText);
            IPagedList<EquipmentEntity> pagedList = await _equipmentStorage.GetAllByFilter(queryableEntity, searchText);
            List<EquipmentEntity> listEntities = pagedList.Items.ToList();
            List<EquipmentDto> listDtos = listEntities.TransferToListDto(permission);
            BaseGrid grid = listDtos.CreateGrid<EquipmentDto>(await GetUserColumns(EntityEnum.EquipmentEntity));

            return new BaseListDto()
            {

                Grid = grid,
                Entity = EntityEnum.EquipmentEntity
            };
        }

        public async Task<EquipmentDto> Update(EquipmentDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Equipment, PermissionFieldEnum.DatabaseEquipment);
            if (permission.Edit)
            {
                EquipmentEntity transferEntity = model.TransferToEntity();
                transferEntity.Code = string.IsNullOrWhiteSpace(transferEntity.Code) ? GeneratorExtention.GenerateUniqueCode().ToString() : transferEntity.Code;
                EquipmentEntity entity = await _equipmentStorage.Update(transferEntity);
                EquipmentDto dto = entity.TransferToDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<List<EquipmentDto>> Search(string text)
        {
            List<EquipmentEntity> entities = await _equipmentStorage.Search(text);
            return entities.TransferToListDto();
        }

        public async Task SetFolderId(int equipmentId, int folderId)
        {
            await _equipmentStorage.SetFolderId(equipmentId, folderId);
        }

        public async Task Import(List<EquipmentEntity> equipments)
        {
            var entities = _equipmentStorage.QueryableEntity.Select(x => new { x.Id, x.Code })
                                                              .Where(x => equipments.Any(e => !string.IsNullOrEmpty(e.Code) && e.Code.Equals(x.Code)))
                                                              .ToList();

            List<EquipmentEntity> entitiesForInsert = new List<EquipmentEntity>();
            List<EquipmentEntity> entitiesForUpdate = new List<EquipmentEntity>();

            for (int i = 0; i < equipments.Count; i++)
            {
                var data = entities.FirstOrDefault(x => x.Code.Equals(equipments[i].Code));
                if (data != null && data.Id > 0)
                {
                    equipments[i].Id = data.Id;
                    entitiesForUpdate.Add(equipments[i]);
                }
                else
                {
                    equipments[i].Code = GeneratorExtention.GenerateUniqueCode();
                    entitiesForInsert.Add(equipments[i]);
                }
            }

            if (entitiesForInsert.Count > 0)
            {
                await _equipmentStorage.Import(entitiesForInsert);
            }

            if (entitiesForUpdate.Count > 0)
            {
                await _equipmentStorage.Update(entitiesForUpdate);
            }
        }

        // <summary>
        /// Download tamplate 
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task<string> DownloadTemplate(ModuleTypeEnum moduleType, string path)
        {
            return await Task.Run<string>(() =>
            {
                List<Column> columns = new List<Column>();

                columns.AddColumns<EquipmentDto>(null, true);

                string urlFile = columns.CreateEmptyExcelWorksheetWithHeader(moduleType.ToString(), path);
                return urlFile;
            });
        }

        public async Task<string> Export(string[] columns, List<SelectedFilter> filterList)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Equipment, PermissionFieldEnum.DatabaseEquipment);
            string searchText;
            IQueryable<EquipmentEntity> queryableEntity = _equipmentStorage.QueryableEntity.CreateFilterForEquipmentEntity(filterList, out searchText);
            IPagedList<EquipmentEntity> pagedList = await _equipmentStorage.GetAllByFilter(queryableEntity, searchText);
            List<EquipmentEntity> listEntities = pagedList.Items.ToList();
            List<EquipmentDto> listDtos = listEntities.TransferToListDto(permission);

            List<Column> sheetColumns = new List<Column>();
            sheetColumns.AddColumns<EquipmentDto>(columns);

            if (sheetColumns.Count > 0)
            {
                // listDtos.
                List<Dictionary<string, object>> Data = new List<Dictionary<string, object>>();
                Data.AddData<EquipmentDto>(listDtos);
                string path = sheetColumns.CreatExcelWorksheetWithHeader(Data, $"EquipmentExport{DateTime.Now.ToString("yyyy-MM-dd")}", "");
                return path;
            }
            return string.Empty;
        }
    }
}
