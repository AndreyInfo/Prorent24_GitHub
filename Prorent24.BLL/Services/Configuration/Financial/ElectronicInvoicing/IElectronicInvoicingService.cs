using Prorent24.BLL.Models.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Financial.ElectronicInvoicing
{
    public interface IElectronicInvoicingService
    {
        Task<ElectronicInvoicingDto> GetAsync();
        /// <summary>
        /// Update Electronic Invoicing
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ElectronicInvoicingDto> Update(ElectronicInvoicingDto model);
    }
}
