using Prorent24.BLL.Models.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Financial.FinancialSetting
{
    public interface IFinancialSettingService
    {
        /// <summary>
        /// Get Financial Setting
        /// </summary>
        /// <returns></returns>
        Task<FinancialSettingDto> Get();

        /// <summary>
        /// Get Advanced
        /// </summary>
        /// <returns></returns>
        Task<FinancialSettingDto> GetAdvanced();

        /// <summary>
        /// Update Financial Setting
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<FinancialSettingDto> Update(FinancialSettingDto model);
    }
}
