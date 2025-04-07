using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Services.Configuration.Financial.Vat;
using Prorent24.BLL.Transfers.Configuration;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.SystemSettings;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.Configuration;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using System;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Financial.FinancialSetting
{
    internal class FinancialSettingService : BaseService, IFinancialSettingService
    {
        private readonly ISystemSettingStorage _systemSettingStorage;
        private readonly IVatSchemeService _vatSchemeService;
        public FinancialSettingService(ISystemSettingStorage systemSettingStorage, 
            IVatSchemeService vatSchemeService, 
            IUserRoleStorage userRoleStorage, 
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _systemSettingStorage = systemSettingStorage;
            _vatSchemeService = vatSchemeService;
        }

        public async Task<FinancialSettingDto> Get()
        {
            SystemSettingEntity entity = await _systemSettingStorage.GetById(SystemSettingsTypeEnum.FinancialSetting);

            FinancialSettingDto dto = new FinancialSettingDto();
            if (entity != null)
            {
                dto = entity.TransferToDto<FinancialSettingDto>();
            }
            return dto;
        }

        public async Task<FinancialSettingDto> GetAdvanced()
        {
            FinancialSettingDto dto = await Get();
            dto.VatScheme = dto.DefaultVatSchemeId > 0 ? await _vatSchemeService.GetById(dto.DefaultVatSchemeId) : dto.VatScheme;
            return dto;
        }

        public async Task<FinancialSettingDto> Update(FinancialSettingDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.Financial);
            if (permission.Add)
            {
                SystemSettingEntity transferEntity = model.TransferToEntity();

                transferEntity.Type = SystemSettingsTypeEnum.FinancialSetting;
                transferEntity.LastModifiedDate = DateTime.UtcNow;

                SystemSettingEntity entity = await _systemSettingStorage.Update(transferEntity);
                FinancialSettingDto dto = entity.TransferToDto<FinancialSettingDto>();
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
