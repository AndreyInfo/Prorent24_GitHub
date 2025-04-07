using Prorent24.BLL.Models.Invoice;
using Prorent24.WebApp.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Invoice
{
    public static class InvoiceLineTransfer
    {
        /// <summary>
        /// Transfer from List<InvoiceLineViewModel> to List<InvoiceLineDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<InvoiceLineDto> TransferToListDto(this List<InvoiceLineViewModel> models)
        {
            List<InvoiceLineDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from InvoiceLineDto to InvoiceLineViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static InvoiceLineDto TransferToDto(this InvoiceLineViewModel model)
        {
            InvoiceLineDto dto = new InvoiceLineDto()
            {
                Id = model.Id,
                LedgerId = model.LedgerId,
                VatClassId = model.VatClassId,
                Description = model.Description,
                Amount = model.Amount,
                Price = model.Price
            };

            return dto;
        }

        /// <summary>
        /// Transfer from InvoiceLineViewModel to InvoiceLineDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static InvoiceLineViewModel TransferToViewModel(this InvoiceLineDto dto)
        {
            InvoiceLineViewModel model = new InvoiceLineViewModel()
            {
                Id = dto.Id,
                LedgerId = dto.LedgerId,
                VatClassId = dto.VatClassId,
                Description = dto.Description,
                Amount = dto.Amount,
                Price = dto.Price
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<InvoiceLineDto> to List<InvoiceLineViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<InvoiceLineViewModel> TransferToViewModel(this List<InvoiceLineDto> dtos)
        {
            List<InvoiceLineViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
