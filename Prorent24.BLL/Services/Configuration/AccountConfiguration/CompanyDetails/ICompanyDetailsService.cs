using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.AccountConfiguration.CompanyDetails
{
    public interface ICompanyDetailsService
    {
        Task<CompanyDetailsDto> GetAsync();
        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CompanyDetailsDto> Update(CompanyDetailsDto model);
    }
}
