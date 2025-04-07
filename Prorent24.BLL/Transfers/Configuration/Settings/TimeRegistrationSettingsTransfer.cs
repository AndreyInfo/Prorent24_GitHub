using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.Entity.Configuration;

namespace Prorent24.BLL.Transfers.Configuration.Settings
{
    public static class TimeRegistrationSettingsTransfer
    {
        /// <summary>
        /// Transfer from TimeRegistrationSettingsDto to SystemSettingEntity
        /// </summary>
        /// <param name="dto" type="TimeRegistrationSettingsDto"></param>
        /// <returns type="SystemSettingEntity"></returns>
        public static SystemSettingEntity TransferToEntity(this TimeRegistrationSettingsDto dto)
        {
            SystemSettingEntity customerCommmunication = new SystemSettingEntity();
            customerCommmunication.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            return customerCommmunication;
        }
    }
}
