using Prorent24.BLL.Models.Configuration;
using Prorent24.WebApp.Models.Configuration.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    public static class InvoiceMomentTransfer
    {
        /// <summary>
        /// Transfer from InvoiceMomentDto to InvoiceMomentViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static InvoiceMomentViewModel TransferToViewModel(this InvoiceMomentDto dto)
        {
            InvoiceMomentViewModel viewModel = new InvoiceMomentViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                AfterAgreement = dto.AfterAgreement,
                BeforeFirstDay = dto.BeforeFirstDay,
                Afterwards = dto.Afterwards,
                Text = dto.Text
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from InvoiceMomentViewModel to InvoiceMomentDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static InvoiceMomentDto TransferToDtoModel(this InvoiceMomentViewModel view)
        {
            InvoiceMomentDto dto = new InvoiceMomentDto()
            {
                Id = view.Id,
                Name = view.Name,
                AfterAgreement = view.AfterAgreement,
                BeforeFirstDay = view.BeforeFirstDay,
                Afterwards = view.Afterwards,
                Text = view.Text
            };

            return dto;
        }
    }
}
