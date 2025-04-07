using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.BLL.Transfers.Contact;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Project;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.Entity.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Invoice
{
    public static class InvoiceTransfer
    {
        /// <summary>
        /// Transfer from List<InvoiceEntity> to List<InvoiceDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<InvoiceDto> TransferToListDto(this List<InvoiceEntity> entities, string exclude = "")
        {
            List<InvoiceDto> dtos = entities.Select(x => x.TransferToDto(exclude)).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from InvoiceDto to InvoiceEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static InvoiceDto TransferToDto(this InvoiceEntity entity, string exclude = "")
        {
            InvoiceDto dto = new InvoiceDto()
            {
                Id = entity.Id,

                Name = entity.Name,
                State = entity.State,
                ClientId = entity.ClientId,
                Client = entity.Client?.TransferToDto(),

                ProjectId = entity.ProjectId,
                Project = exclude=="Project"? null: entity.Project?.TransferToDto("Project"),
                //OwnerId = entity.OwnerId,
                AccountingCode = entity.AccountingCode,

                //Date = entity.Date,
                //DueDate = entity.DueDate,
                // SendAtDate = entity.SendAtDate,
                //PaymentDate = entity.PaymentDate,

                PaymentConditionId = entity.PaymentConditionId,
                PaymentCondition = entity.PaymentCondition?.TransferToConditionDto(),

                PaymentMethodId = entity.PaymentMethodId,
                PaymentMethod = entity.PaymentMethod?.TransferToMethodDto(),

                VatSchemeId = entity.VatSchemeId,
                VatScheme = entity.VatScheme?.TransferToVatSchemeDto(),
                // Template
                //UseLetterhead = entity.UseLetterhead,
                // LetterheadId = entity.LetterheadId,
                // TemplateId = entity.TemplateId,
                CreditDebit = entity.CreditDebit,
                //Text = entity.Text,

                // OpenKitsAndCases = entity.OpenKitsAndCases,
                PercentagePartialInvoice = entity.PercentagePartialInvoice,
                PriceCalculationBasedOnProject = entity.PriceCalculationBasedOnProject,
                PriceExcludeVAT = entity.PriceExcludeVAT,
                PriceIncludeVAT = entity.PriceIncludeVAT,
                TotalNewPrice = entity.InvoiceLines?.Sum(x=>x.Price),

                VAT = entity.VAT,
                FileName = entity.FileName,

                Document = exclude == "Document" ? null : entity.Document?.TransferToDto("Document"),

                InvoiceLines = entity.InvoiceLines?.ToList().TransferToListDto(),

                Date = entity.Date,
                DueDate = entity.DueDate,


                Tasks = exclude == "Invoice" ? null : entity.Tasks?.ToList().TransferToListDto("Invoice"),
                Notes = entity.Notes?.ToList().TransferToListDto(),
                Tags = entity.Tags?.ToList().TransferToListTagDto(),
                Files = entity.Files?.ToList().TransferToListDto(),
                GeneratedOn = entity.LastModifiedDate?? entity.CreationDate
            };

            return dto;
        }

        /// <summary>
        /// Transfer from InvoiceEntity to InvoiceDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static InvoiceEntity TransferToEntity(this InvoiceDto dto, string method = "")
        {
            InvoiceEntity entity = new InvoiceEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                State = dto.State,
                ClientId = dto.ClientId,

                ProjectId = dto.ProjectId,
                //OwnerId = dto.OwnerId,
                AccountingCode = dto.AccountingCode,
                Date = dto.Date,
                DueDate = dto.DueDate,
                // SendAtDate = dto.SendAtDate,
                PaymentDate = dto.PaymentDate,

                PaymentConditionId = dto.PaymentConditionId,
                PaymentMethodId = dto.PaymentMethodId,
                VatSchemeId = dto.VatSchemeId,
                // Template
                // UseLetterhead = dto.UseLetterhead,

                // LetterheadId = dto.LetterheadId,
                // TemplateId = dto.TemplateId,

                CreditDebit = dto.CreditDebit,
                //Text = dto.Text,

                // OpenKitsAndCases = dto.OpenKitsAndCases,
                PercentagePartialInvoice = dto.PercentagePartialInvoice,
                PriceCalculationBasedOnProject = dto.PriceCalculationBasedOnProject,
                PriceExcludeVAT = dto.PriceExcludeVAT,
                PriceIncludeVAT = dto.PriceIncludeVAT,
                TotalNewPrice = dto.TotalNewPrice,

                VAT = dto.VAT,
                FileName = dto.FileName,

                Document = dto.Document?.TransferToEntity(),
                InvoiceLines = method == "create" ? dto.InvoiceLines?.TransferToListEntityModel() : null

            };
            return entity;
        }

        /// <summary>
        /// Transfer from List<InvoiceDto> to List<InvoiceEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<InvoiceEntity> TransferToListEntityModel(this List<InvoiceDto> dto)
        {
            List<InvoiceEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }

        public static InvoiceEntity TransferToEntityForUpdate(this InvoiceEntity entity, InvoiceEntity entityUpdate)
        {
            entity.ClientId = entityUpdate.ClientId;
            entity.ContactPersonId = entityUpdate.ContactPersonId;
            entity.VatSchemeId = entityUpdate.VatSchemeId;
            entity.PaymentConditionId = entityUpdate.PaymentConditionId;
            entity.PaymentMethodId = entityUpdate.PaymentMethodId;
            entity.LastModifiedDate = DateTime.UtcNow;
            entity.DueDate = entityUpdate.DueDate;
            // entity.OpenKitsAndCases = entityUpdate.OpenKitsAndCases;
            entity.ContactPersonId = entityUpdate.ContactPersonId;
            entity.Date = entityUpdate.Date;
            entity.Client = null;
            entity.Document.TransferToEntityForUpdate(entityUpdate.Document);
            entity.State = entityUpdate.State;
            return entity;
        }
    }
}
