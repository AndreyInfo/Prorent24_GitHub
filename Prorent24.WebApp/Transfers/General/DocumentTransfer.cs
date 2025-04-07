using Prorent24.BLL.Models.General.Document;
using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.General.Document;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.General
{
    public static class DocumentTransfer
    {
        public static DocumentViewModel TransferToViewModel(this DocumentDto dto)
        {
            DocumentViewModel model = new DocumentViewModel()
            {
                Confidential = dto.Confidential,
                Date = dto.Date,
                Description = dto.Description,
                DocumentType = dto.DocumentType,
                DueDate = dto.DueDate,
                //FileId = dto.FileId,
                FileName = dto.FileName,
                GeneratedById = dto.GeneratedById,
                GeneratedOn = dto.GeneratedOn,
                InvoiceId = dto.InvoiceId,
                LetterheadId = dto.LetterheadId,
                Name = dto.Name,
                Number = dto.Number,
                OpenKitsAndCases = dto.OpenKitsAndCases,
                ProjectId = dto.ProjectId,
                Subject = dto.Subject,
                TemplateId = dto.TemplateId,
                Text = dto.Text,
                UseLetterhead = dto.UseLetterhead,
                Id = dto.Id,
                PaymentConditionId = dto.PaymentConditionId
            };

            return model;
        }

        public static DocumentDto TransferToDto(this DocumentViewModel model)
        {
            DocumentDto dto = new DocumentDto()
            {
                Confidential = model.Confidential,
                Date = model.Date,
                Description = model.Description,
                DocumentType = model.DocumentType,
                DueDate = model.DueDate,
                //FileId = model.FileId,
                FileName = model.FileName,
                GeneratedById = model.GeneratedById,
                GeneratedOn = model.GeneratedOn,
                // InvoiceId = model.InvoiceId,
                // на створення можна відправити інформацію по інвойсу
                LetterheadId = model.LetterheadId,
                Name = model.Name,
                Number = model.Number,
                OpenKitsAndCases = model.OpenKitsAndCases,
                // ProjectId = model.ProjectId,
                Subject = model.Subject,
                TemplateId = model.TemplateId,
                Text = model.Text,
                UseLetterhead = model.UseLetterhead,
                Id = model.Id,
                PaymentConditionId = model.PaymentConditionId
            };

            return dto;
        }
    }
}
