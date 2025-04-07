using Prorent24.BLL.Models.Invoice;
using Prorent24.WebApp.Models.Invoice;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.Invoice
{
    public static class InvoiceExcludedTransfer
    {
        /// <summary>
        /// Transfer from List<InvoiceExcludedViewModel> to List<InvoiceExcludedDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<InvoiceExcludedDto> TransferToListDto(this List<InvoiceExcludedViewModel> models)
        {
            List<InvoiceExcludedDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from InvoiceExcludedDto to InvoiceExcludedViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static InvoiceExcludedDto TransferToDto(this InvoiceExcludedViewModel model)
        {
            InvoiceExcludedDto dto = new InvoiceExcludedDto()
            {
                Id = model.Id,
                InvoiceExcludedId = model.ExcludedInvoiceId
            };

            return dto;
        }

        /// <summary>
        /// Transfer from InvoiceExcludedViewModel to InvoiceExcludedDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static InvoiceExcludedViewModel TransferToViewModel(this InvoiceExcludedDto dto)
        {
            InvoiceExcludedViewModel model = new InvoiceExcludedViewModel()
            {
                Id = dto.Id,
                ExcludedInvoiceId = dto.InvoiceExcludedId
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<InvoiceExcludedDto> to List<InvoiceExcludedViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<InvoiceExcludedViewModel> TransferToViewModel(this List<InvoiceExcludedDto> dtos)
        {
            List<InvoiceExcludedViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
