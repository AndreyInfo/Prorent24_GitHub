using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.WebApp.Models.Configuration.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    public static class LedgerTransfer
    {
        /// <summary>
        /// Transfer from LedgerDto to LedgerViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static LedgerViewModel TransferToViewModel(this LedgerDto dto)
        {
            LedgerViewModel viewModel = new LedgerViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                AccountingCode = dto.AccountingCode,
                IsSystem = dto.IsSystem
                
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from LedgerViewModel to LedgerDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static LedgerDto TransferToDtoModel(this LedgerViewModel view)
        {
            LedgerDto dto = new LedgerDto()
            {
                Id = view.Id,
                Name = view.Name,
                AccountingCode = view.AccountingCode,
                IsSystem = view.IsSystem
            };

            return dto;
        }

    }
}
