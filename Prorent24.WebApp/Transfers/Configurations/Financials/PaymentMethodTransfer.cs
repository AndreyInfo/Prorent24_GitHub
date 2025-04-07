using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.WebApp.Models.Configuration.Financials.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    public static class PaymentMethodTransfer
    {
        /// <summary>
        /// Transfer from PaymentMethodDto to PaymentMethodViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static PaymentMethodViewModel TransferToViewModel(this PaymentMethodDto dto)
        {
            PaymentMethodViewModel viewModel = new PaymentMethodViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                AccountingCode = dto.AccountingCode
                
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from PaymentMethodViewModel to PaymentMethodDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static PaymentMethodDto TransferToDtoModel(this PaymentMethodViewModel view)
        {
            PaymentMethodDto dto = new PaymentMethodDto()
            {
                Id = view.Id,
                Name = view.Name,
                AccountingCode = view.AccountingCode
            };

            return dto;
        }

    }
}
