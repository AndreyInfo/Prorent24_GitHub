using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.WebApp.Models.Configuration.Financials;
using Prorent24.WebApp.Transfers.Directory;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    public static class ElectronicInvoicingTransfer
    {
        /// <summary>
        /// Transfer from  ElectronicInvoicingDto to  ElectronicInvoicingViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ElectronicInvoicingViewModel TransferToViewModel(this ElectronicInvoicingDto dto)
        {
            ElectronicInvoicingViewModel viewModel = new ElectronicInvoicingViewModel()
            {
                Currency = dto.Currency,
                Directory = dto.Directory?.TransferToViewModel(),
                IdentificationNumber = dto.IdentificationNumber,
                IdentificationScheme = dto.IdentificationScheme

            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from  ElectronicInvoicingViewModel to  ElectronicInvoicingDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static ElectronicInvoicingDto TransferToDto(this ElectronicInvoicingViewModel view)
        {
            ElectronicInvoicingDto dto = new ElectronicInvoicingDto()
            {
                Currency = view.Currency,
                Directory = view.Directory?.TransferToDtoModel(),
                IdentificationNumber = view.IdentificationNumber,
                IdentificationScheme = view.IdentificationScheme
            };

            return dto;
        }
    }
}
