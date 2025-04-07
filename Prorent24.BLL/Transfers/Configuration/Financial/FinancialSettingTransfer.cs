using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Entity.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.Financial
{
    public static class FinancialSettingTransfer
    {
        /// <summary>
        /// Transfer from FinancialSettingDto to SystemSettingEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns type="SystemSettingEntity"></returns>
        public static SystemSettingEntity TransferToEntity(this FinancialSettingDto dto)
        {
            SystemSettingEntity settingEntity = new SystemSettingEntity();
            settingEntity.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            return settingEntity;
        }
    }
}
