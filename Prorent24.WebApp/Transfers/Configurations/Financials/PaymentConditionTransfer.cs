using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.WebApp.Models.Configuration.Financials.Payment;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    public static class PaymentConditionTransfer
    {
        /// <summary>
        /// Transfer from PaymentConditionDto to PaymentConditionViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static PaymentConditionViewModel TransferToViewModel(this PaymentConditionDto dto)
        {
            PaymentConditionViewModel viewModel = new PaymentConditionViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                AccountingCode = dto.AccountingCode,
                PaymentMethodId = dto.PaymentMethodId,
                PaymentMethod = dto.PaymentMethod?.TransferToViewModel(),
                TermInDays = dto.TermInDays,
                TextOnInvoice = dto.TextOnInvoice

            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from PaymentConditionViewModel to PaymentConditionDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static PaymentConditionDto TransferToDtoModel(this PaymentConditionViewModel view)
        {
            PaymentConditionDto dto = new PaymentConditionDto()
            {
                Id = view.Id,
                Name = view.Name,
                AccountingCode = view.AccountingCode,
                PaymentMethodId = view.PaymentMethodId,
                PaymentMethod = view.PaymentMethod?.TransferToDtoModel(),
                TermInDays = view.TermInDays,
                TextOnInvoice = view.TextOnInvoice
            };

            return dto;
        }

        /// <summary>
        /// Transfer from PaymentConditionDto to PaymentConditionViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static PaymentConditionDefaultViewModel TransferToViewModel(this PaymentConditionDefaultDto dto)
        {
            PaymentConditionDefaultViewModel viewModel = new PaymentConditionDefaultViewModel()
            {
                Id = dto.Id
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from PaymentConditionDefaultViewModel to PaymentConditionDefaultDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static PaymentConditionDefaultDto TransferToDtoModel(this PaymentConditionDefaultViewModel view)
        {
            PaymentConditionDefaultDto dto = new PaymentConditionDefaultDto()
            {
                Id = view.Id
            };

            return dto;
        }

    }
}
