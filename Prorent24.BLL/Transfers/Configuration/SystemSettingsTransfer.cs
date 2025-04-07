using Prorent24.Entity.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Transfers.Configuration
{
    public static class SystemSettingsTransfer
    {
        /// <summary>
        /// Transfer from SystemSettingEntity to dto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T TransferToDto<T>(this SystemSettingEntity entity)
        {
            var deserializedObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(entity.Value);
            return deserializedObject;
        }
    }
}
