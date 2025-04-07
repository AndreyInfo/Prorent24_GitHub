using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.CustomerCommunication.Communication
{
    public interface ICustomerCommmunicationService //: IBaseService<CustomerCommmunicationDto>
    {
        Task<CustomerCommmunicationDto> GetAsync();
        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CustomerCommmunicationDto> Update(CustomerCommmunicationDto model);
    }
}
