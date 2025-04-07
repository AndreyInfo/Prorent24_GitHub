using Prorent24.BLL.Models.General.Document;
using Prorent24.BLL.Models.Invoice;
using Prorent24.Enum.Configuration;
using Prorent24.WebApp.Models.Invoice;
using Prorent24.WebApp.Transfers.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Invoice
{
    public static class InvoiceTransfer
    {
        /// <summary>
        /// Transfer from List<InvoiceViewModel> to List<InvoiceDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<InvoiceDto> TransferToListDto(this List<InvoiceViewModel> models)
        {
            List<InvoiceDto> dtos = models.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from InvoiceDto to InvoiceViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static InvoiceDto TransferToDto(this InvoiceViewModel model)
        {
            InvoiceDto dto = new InvoiceDto()
            {
                Id = model.Id,
                Name = model.Name,
                ClientId = model.ClientId,
                State = model.State,

                ProjectId = model.ProjectId,
                //OwnerId = model.OwnerId,
                AccountingCode = model.AccountingCode,
                Date = model.Date,
                DueDate = model.DueDate,
                //SendAtDate = model.SendAt,
                PaymentDate = model.PaymentDate,

                PaymentConditionId = model.PaymentConditionId,
                PaymentMethodId = model.PaymentMethodId,
                VatSchemeId = model.VatSchemeId,
                // Template
                //UseLetterhead = model.UseLetterhead,
                //LetterheadId = model.LetterheadId,
                //TemplateId = model.TemplateId,
                //Text = model.Text,

                CreditDebit = model.CreditDebit,

                OpenKitsAndCases = model.OpenKitsAndCases,
                PercentagePartialInvoice = model.PercentagePartialInvoice,
                PriceCalculationBasedOnProject = model.PriceCalculationBasedOnProject,
                PriceExcludeVAT = model.PriceExcludeVAT,
                PriceIncludeVAT = model.PriceIncludeVAT,
                TotalNewPrice = model.TotalNewPrice,

                VAT = model.VAT,
                FileName = model.FileName,
                InvoiceLines = model.InvoiceLines?.TransferToListDto()
            };

            dto.Document = new DocumentDto()
            {
                TemplateId = model.TemplateId,
                LetterheadId = model.LetterheadId,
                // Description = model.
                Number = model.Number,
                UseLetterhead = model.UseLetterhead,
                Subject = model.Subject,
                FileName = model.FileName,
                Text = model.Text,
                Date = model.Date,
                Name = model.Name,
                DocumentType = DocumentTemplateTypeEnum.Invoice
                
            };

            return dto;
        }

        /// <summary>
        /// Transfer from InvoiceViewModel to InvoiceDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static InvoiceViewModel TransferToViewModel(this InvoiceDto dto)
        {
            InvoiceViewModel model = new InvoiceViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                State = dto.State,
                ClientId = dto.ClientId,
                Client = dto.Client?.TransferToViewModel(),

                ProjectId = dto.ProjectId,
                //OwnerId = dto.OwnerId,
                AccountingCode = dto.AccountingCode,
                Date = dto.Document?.Date,
                DueDate = dto.DueDate,
                SendAtDate = dto.SendAtDate,
                PaymentDate = dto.PaymentDate,

                PaymentConditionId = dto.PaymentConditionId,
                PaymentMethodId = dto.PaymentMethodId,
                VatSchemeId = dto.VatSchemeId,
                // Template
                UseLetterhead = dto.Document != null ? dto.Document.UseLetterhead : true,
                LetterheadId = dto.Document?.LetterheadId,
                TemplateId = dto.Document?.TemplateId,
                Number = dto.Document?.Number,
                Subject = dto.Document?.Subject,
                FileName = dto.Document?.FileName,
                Text = dto.Document?.Text,


                CreditDebit = dto.CreditDebit,

                OpenKitsAndCases = dto.OpenKitsAndCases.HasValue? dto.OpenKitsAndCases.Value : Enum.Invoice.OpenKitsAndCasesTypeEnum.DefaultKitOrCase,
                PercentagePartialInvoice = dto.PercentagePartialInvoice,
                PriceCalculationBasedOnProject = dto.PriceCalculationBasedOnProject,
                PriceExcludeVAT = dto.PriceExcludeVAT,
                PriceIncludeVAT = dto.PriceIncludeVAT,
                TotalNewPrice = dto.TotalNewPrice,

                VAT = dto.VAT,
                InvoiceLines = dto.InvoiceLines?.TransferToViewModel()
                // FileName = dto.FileName,
            };
            return model;
        }

        /// <summary>
        /// Transfer from List<InvoiceDto> to List<InvoiceViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<InvoiceViewModel> TransferToViewModel(this List<InvoiceDto> dtos)
        {
            List<InvoiceViewModel> models = dtos.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }
    }
}
