using System;
using System.Threading.Tasks;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.BLL.Transfers.Configuration;
using Prorent24.DAL.Storages.Configuration.SystemSettings;
using Prorent24.Entity.Configuration;
using Prorent24.Enum.Configuration;
using Prorent24.BLL.Transfers.Configuration.Settings;

namespace Prorent24.BLL.Services.Configuration.Settings.TimeRegistrationSettings
{
    internal class TimeRegistrationSettingsService : ITimeRegistrationSettingsService
    {
        private readonly ISystemSettingStorage _systemSettingStorage;
        public TimeRegistrationSettingsService(ISystemSettingStorage systemSettingStorage)
        {
            this._systemSettingStorage = systemSettingStorage;
        }

        public async Task<TimeRegistrationSettingsDto> GetAsync()
        {
            SystemSettingEntity entity = await _systemSettingStorage.GetById(SystemSettingsTypeEnum.TimeRegistration);

            TimeRegistrationSettingsDto dto = new TimeRegistrationSettingsDto();
            if (entity != null)
            {
                dto = entity.TransferToDto<TimeRegistrationSettingsDto>();
            }
            return dto;
        }

        public async Task<TimeRegistrationSettingsDto> Update(TimeRegistrationSettingsDto model)
        {
            SystemSettingEntity transferEntity = model.TransferToEntity();

            transferEntity.Type = SystemSettingsTypeEnum.TimeRegistration;
            transferEntity.LastModifiedDate =  DateTime.UtcNow;

            SystemSettingEntity entity = await _systemSettingStorage.Update(transferEntity);
            TimeRegistrationSettingsDto dto = entity.TransferToDto<TimeRegistrationSettingsDto>();
            return dto;
        }
    }
}
