using Prorent24.BLL.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Financial.InvoiceMoment
{
    public interface IInvoiceMomentService : IBaseService<InvoiceMomentDto, int>
    {
        /// <summary>
        /// Delete data multiple
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        Task<bool> DeleteMultiple(List<int> Values);
    }
}
