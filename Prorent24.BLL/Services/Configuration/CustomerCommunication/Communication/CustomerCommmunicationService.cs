using System;
using System.Threading.Tasks;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.DAL.Storages.Configuration.SystemSettings;
using Prorent24.BLL.Transfers.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration;
using Prorent24.BLL.Transfers.Configuration;
using Prorent24.Enum.Configuration;
using Prorent24.DAL.Storages.General.Module;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Microsoft.AspNetCore.Http;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.DAL.Storages.General.Column;

namespace Prorent24.BLL.Services.Configuration.CustomerCommunication.Communication
{
    // change history
    internal class CustomerCommmunicationService : BaseService, ICustomerCommmunicationService
    {
        private readonly ISystemSettingStorage _systemSettingStorage;
        public CustomerCommmunicationService(ISystemSettingStorage systemSettingStorage, 
            IUserRoleStorage userRoleStorage,  
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._systemSettingStorage = systemSettingStorage;
        }

        public async Task<CustomerCommmunicationDto> GetAsync() {
            SystemSettingEntity entity = await _systemSettingStorage.GetById(SystemSettingsTypeEnum.Communication);

            CustomerCommmunicationDto dto = new CustomerCommmunicationDto();
            if (entity != null)
            {
                dto = entity.TransferToDto<CustomerCommmunicationDto>();
            }
            return dto;
        }

        public async Task<CustomerCommmunicationDto> Update(CustomerCommmunicationDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.Communication);
            if (permission.Add)
            {
                SystemSettingEntity transferEntity = model.TransferToEntity();

                transferEntity.Type = SystemSettingsTypeEnum.Communication;
                transferEntity.LastModifiedDate = DateTime.UtcNow;

                SystemSettingEntity entity = await _systemSettingStorage.Update(transferEntity);
                CustomerCommmunicationDto dto = entity.TransferToDto<CustomerCommmunicationDto>();
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
