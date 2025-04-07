using System;
using System.Threading.Tasks;
using Prorent24.DAL.Storages.Configuration.SystemSettings;
using Prorent24.Entity.Configuration;
using Prorent24.BLL.Transfers.Configuration;
using Prorent24.Enum.Configuration;
using Prorent24.BLL.Transfers.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.BLL.Transfers.Configuration.Settings;

namespace Prorent24.BLL.Services.Configuration.Settings.TimeAndLocation
{
    internal class TimeAndLocationService : ITimeAndLocationService
    {
        private readonly ISystemSettingStorage _systemSettingStorage;
        public TimeAndLocationService(ISystemSettingStorage systemSettingStorage)
        {
            this._systemSettingStorage = systemSettingStorage;
        }

        public async Task<TimeAndLocationDto> GetAsync()
        {
            SystemSettingEntity entity = await _systemSettingStorage.GetById(SystemSettingsTypeEnum.TimeAndLocations);

            TimeAndLocationDto dto = new TimeAndLocationDto();
            if (entity != null)
            {
                dto = entity.TransferToDto<TimeAndLocationDto>();
            }
            return dto;
        }

        public async Task<TimeAndLocationDto> Update(TimeAndLocationDto model)
        {
            SystemSettingEntity transferEntity = model.TransferToEntity();

            transferEntity.Type = SystemSettingsTypeEnum.TimeAndLocations;
            transferEntity.LastModifiedDate =  DateTime.UtcNow;

            SystemSettingEntity entity = await _systemSettingStorage.Update(transferEntity);
            TimeAndLocationDto dto = entity.TransferToDto<TimeAndLocationDto>();
            return dto;
        }
    }
}
