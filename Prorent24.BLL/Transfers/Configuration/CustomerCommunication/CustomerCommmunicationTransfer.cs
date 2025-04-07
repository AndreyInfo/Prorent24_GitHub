using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration.CustomerCommunication
{
    public static class CustomerCommmunicationTransfer
    {
        /// <summary>
        /// Transfer from CustomerCommmunicationDto to SystemSettingEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns type="SystemSettingEntity"></returns>
        public static SystemSettingEntity TransferToEntity(this CustomerCommmunicationDto dto)
        {
            SystemSettingEntity customerCommmunication = new SystemSettingEntity();
            customerCommmunication.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            return customerCommmunication;
        }
    }
}
