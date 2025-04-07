using Prorent24.BLL.Models.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Financial.Payment
{
    public interface IPaymentConditionService : IBaseService<PaymentConditionDto, int>
    {
        Task<PaymentConditionDefaultDto> UpdatePaymentConditionByDefault(PaymentConditionDefaultDto model);

        Task<PaymentConditionDefaultDto> GetPaymentConditionByDefault();

        /// <summary>
        /// Delete data multiple
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        Task<bool> DeleteMultiple(List<int> Values);
    }
}
