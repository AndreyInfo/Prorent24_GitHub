using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.DAL.Storages.Configuration.SystemSettings;
using Prorent24.Entity.Configuration;
using Prorent24.Enum.Configuration;
using System;
using System.Threading.Tasks;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.BLL.Transfers.Configuration;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Microsoft.AspNetCore.Http;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.DAL.Storages.General.Column;

namespace Prorent24.BLL.Services.Configuration.Financial.ElectronicInvoicing
{
    internal class ElectronicInvoicingService : BaseService, IElectronicInvoicingService
    {
        private readonly ISystemSettingStorage _systemSettingStorage;
        public ElectronicInvoicingService(ISystemSettingStorage systemSettingStorage, 
            IUserRoleStorage userRoleStorage, 
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._systemSettingStorage = systemSettingStorage;
        }

        public async Task<ElectronicInvoicingDto> GetAsync()
        {
            SystemSettingEntity entity = await _systemSettingStorage.GetById(SystemSettingsTypeEnum.ElectronicInvoicing);

            ElectronicInvoicingDto dto = new ElectronicInvoicingDto();
            if (entity != null)
            {
                dto = entity.TransferToDto<ElectronicInvoicingDto>();
            }
            return dto;
        }

        public async Task<ElectronicInvoicingDto> Update(ElectronicInvoicingDto model)
        {
            // додати відповідні 
            //var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.ElectronicInvoic);
            //if (permission.Add)
            //{
                SystemSettingEntity transferEntity = model.TransferToEntity();

                transferEntity.Type = SystemSettingsTypeEnum.ElectronicInvoicing;
                transferEntity.LastModifiedDate = DateTime.UtcNow;

                SystemSettingEntity entity = await _systemSettingStorage.Update(transferEntity);
                ElectronicInvoicingDto dto = entity.TransferToDto<ElectronicInvoicingDto>();
                return dto;
        }
    }
}
