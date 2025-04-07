using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.Entity.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.AccountConfiguration
{
    public static class TimeAndLocationTransfer
    {
        /// <summary>
        /// Transfer from CompanyDetailsDto to SystemSettingEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns type="SystemSettingEntity"></returns>
        public static SystemSettingEntity TransferToEntity(this CompanyDetailsDto dto)
        {
            SystemSettingEntity customerCommmunication = new SystemSettingEntity();
            customerCommmunication.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            return customerCommmunication;
        }
    }
}
