using System;
using System.Threading.Tasks;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.DAL.Storages.Configuration.SystemSettings;
using Prorent24.Entity.Configuration;
using Prorent24.BLL.Transfers.Configuration;
using Prorent24.Enum.Configuration;
using Prorent24.BLL.Transfers.Configuration.AccountConfiguration;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Microsoft.AspNetCore.Http;
using Prorent24.Enum.General;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.DAL.Storages.General.Column;

namespace Prorent24.BLL.Services.Configuration.AccountConfiguration.CompanyDetails
{
    internal class CompanyDetailsService : BaseService, ICompanyDetailsService
    {
        private readonly ISystemSettingStorage _systemSettingStorage;
        public CompanyDetailsService(ISystemSettingStorage systemSettingStorage, 
            IUserRoleStorage userRoleStorage, 
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._systemSettingStorage = systemSettingStorage;
        }

        public async Task<CompanyDetailsDto> GetAsync()
        {
            SystemSettingEntity entity = await _systemSettingStorage.GetById(SystemSettingsTypeEnum.CompanyDetails);

            CompanyDetailsDto dto = new CompanyDetailsDto();
            if (entity != null)
            {
                dto = entity.TransferToDto<CompanyDetailsDto>();
            }
            return dto;
        }

        public async Task<CompanyDetailsDto> Update(CompanyDetailsDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.CompanyInfo);
            if (permission.Add)
            {
                SystemSettingEntity transferEntity = model.TransferToEntity();

                transferEntity.Type = SystemSettingsTypeEnum.CompanyDetails;
                transferEntity.LastModifiedDate = DateTime.UtcNow;

                SystemSettingEntity entity = await _systemSettingStorage.Update(transferEntity);
                CompanyDetailsDto dto = entity.TransferToDto<CompanyDetailsDto>();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }
    }
}

