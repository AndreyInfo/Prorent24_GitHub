using Prorent24.BLL.Models.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Financial.Vat
{
    public interface IVatClassService : IBaseService<VatClassDto, int>
    {
        /// <summary>
        /// Delete data multiple
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        Task<bool> DeleteMultiple(List<int> Values);

        /// <summary>
        /// Get list
        /// </summary>
        /// <returns></returns>
        Task<List<VatClassDto>> GetList();
    }
}
