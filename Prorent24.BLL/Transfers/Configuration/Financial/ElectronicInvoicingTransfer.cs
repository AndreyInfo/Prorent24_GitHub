using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.Entity.Configuration;

namespace Prorent24.BLL.Transfers.Configuration.Financial
{
    public static class ElectronicInvoicingTransfer
    {
        /// <summary>
        /// Transfer from ElectronicInvoicingDto to SystemSettingEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns type="SystemSettingEntity"></returns>
        public static SystemSettingEntity TransferToEntity(this ElectronicInvoicingDto dto)
        {
            SystemSettingEntity customerCommmunication = new SystemSettingEntity();
            customerCommmunication.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            return customerCommmunication;
        }
    }
}
