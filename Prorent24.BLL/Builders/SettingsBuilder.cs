using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Builders
{
    internal static class SettingsBuilder
    {
        internal static EquipmentDto BuildEquipmentSettings(this EquipmentDto dto, FinancialSettingDto financialSetting)
        {
            dto.VatClassSchemeRate = dto?.VATClassId > 0 ? financialSetting?.VatScheme?.VatClassSchemeRates?.FirstOrDefault(x => x.VatClassId == dto?.VATClassId)?.Rate : 0;
            return dto;
        }
    }
}
