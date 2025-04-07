using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.WebApp.Models.Configuration.CustomerCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.CustomerCommunication
{
    public static class CustomerCommmunicationTransfer
    {
        /// <summary>
        /// Transfer from  CustomerCommmunicationDto to  CustomerCommmunicationViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static CustomerCommmunicationViewModel TransferToViewModel(this CustomerCommmunicationDto dto)
        {
            CustomerCommmunicationViewModel viewModel = new CustomerCommmunicationViewModel()
            {
                CompanyName = dto.CompanyName,
                Email = dto.Email,
                EmailBodyTemplate = dto.EmailBodyTemplate,
                EmailFrom = dto.Email
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from  CustomerCommmunicationViewModel to  CustomerCommmunicationDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static CustomerCommmunicationDto TransferToDto(this CustomerCommmunicationViewModel view)
        {
            CustomerCommmunicationDto dto = new CustomerCommmunicationDto()
            {
                CompanyName = view.CompanyName,
                Email = view.Email,
                EmailBodyTemplate = view.EmailBodyTemplate,
                EmailFrom = view.Email
            };

            return dto;
        }
    }
}
