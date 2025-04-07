using Prorent24.BLL.Models.General.Document;
using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.Configuration.CustomerCommunication;
using Prorent24.BLL.Transfers.Configuration.Settings;
using Prorent24.BLL.Transfers.Invoice;
using Prorent24.BLL.Transfers.Project;
using Prorent24.Entity.General;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.General;
using Prorent24.Enum.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.General
{
    public static class DocumentTransfer
    {
        /// <summary>
        /// Transfer from List<DocumentEntity> to List<DocumentDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<DocumentDto> TransferToListDto(this List<DocumentEntity> entities, string exclude = "")
        {
            List<DocumentDto> dtos = entities.Select(x => x.TransferToDto(exclude)).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from DocumentDto to DocumentEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DocumentDto TransferToDto(this DocumentEntity entity, string exclude = "")
        {
            DocumentDto dto = new DocumentDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Number = entity.Number,
                DocumentType = entity.DocumentType,
                Date = entity.Date, // дата документу
                LetterheadId = entity.LetterheadId,
                Letterhead = entity.Letterhead?.TransferToDto(),
                TemplateId = entity.TemplateId,
                Template = entity.Template?.TransferToDto(),
                Confidential = entity.Confidential,
                Description = entity.Description,
                GeneratedById = entity.GeneratedById,
                GeneratedOn = entity.GeneratedOn,
                OpenKitsAndCases = entity.OpenKitsAndCases,

                FileName = entity.FileName,
                Text = entity.Text,
                UseLetterhead = entity.UseLetterhead,
                Subject = entity.Subject,
                ProjectId = entity.ProjectId,
                InvoiceId = entity.InvoiceId,
                Project = exclude == "Project" ? null : entity.Project?.TransferToDto("Project"),
                Invoice = exclude == "Document" ? null : entity.Invoice?.TransferToDto("Document"),
                PaymentConditionId = entity.Invoice?.PaymentConditionId
            };

            return dto;
        }

        /// <summary>
        /// Transfer from DocumentEntity to DocumentDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static DocumentEntity TransferToEntity(this DocumentDto dto)
        {
            DocumentEntity entity = new DocumentEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Number = dto.Number,
                DocumentType = dto.DocumentType,
                Date = dto.Date, // дата документу
                LetterheadId = dto.LetterheadId,
                TemplateId = dto.TemplateId,
                Confidential = dto.Confidential,
                Description = dto.Description,
                GeneratedById = dto.GeneratedById,
                GeneratedOn = dto.GeneratedOn,
                FileName = dto.FileName,
                Text = dto.Text,
                UseLetterhead = dto.UseLetterhead,
                Subject = dto.Subject,
                ProjectId = dto.ProjectId,
                InvoiceId = dto.InvoiceId,
                OpenKitsAndCases = dto.OpenKitsAndCases,
            };

            if (dto.PaymentConditionId != null) {
                entity.Invoice = new Entity.Invoice.InvoiceEntity() { PaymentConditionId = dto.PaymentConditionId };
            } 
            return entity;
        }

        /// <summary>
        /// Transfer from List<DocumentDto> to List<DocumentEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<DocumentEntity> TransferToDto(this List<DocumentDto> dto)
        {
            List<DocumentEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }

        public static DocumentEntity TransferToEntityForUpdate(this DocumentEntity entity, DocumentEntity entityUpdate)
        {
            entity.LetterheadId = entityUpdate.LetterheadId;
            entity.TemplateId = entityUpdate.TemplateId;
            entity.LastModifiedDate = DateTime.UtcNow;
            entity.Name = entityUpdate.Name;
            entity.Number = entityUpdate.Number;
            entity.UseLetterhead = entityUpdate.UseLetterhead;
            entity.Description = entityUpdate.Description;
            entity.FileName = entityUpdate.FileName;
            entity.Subject = entityUpdate.Subject;
            entity.Date = entityUpdate.Date;
            entity.Text = entityUpdate.Text;
            entity.OpenKitsAndCases = entityUpdate.OpenKitsAndCases;

            if (entityUpdate.Invoice != null && entity.Invoice !=null)
            {
                entity.Invoice.PaymentConditionId = entityUpdate.Invoice?.PaymentConditionId;
            }

            return entity;
        }

        public static List<ProjectDocumentGroupDto> TransferToProjectDocumentGroupDto(this List<DocumentDto> dto)
        {
            //List<ProjectDocumentGroupDto> groupDtos =
            //dto.GroupBy(x => x.DocumentType).Select(x => new ProjectDocumentGroupDto()
            //{
            //    Type = x.Key,
            //    Data = x.ToList().TransferToProjectDocumentDtos()
            //}).ToList();

            List<ProjectDocumentGroupDto> groupDtos = new List<ProjectDocumentGroupDto>();
            // by order...
            //Quotation
            groupDtos.Add(new ProjectDocumentGroupDto() { Type = DocumentTemplateTypeEnum.Quotation, Data = dto.Where(x => x.DocumentType == DocumentTemplateTypeEnum.Quotation).ToList().TransferToProjectDocumentDtos() });
            //
            groupDtos.Add(new ProjectDocumentGroupDto() { Type = DocumentTemplateTypeEnum.Contract, Data = dto.Where(x => x.DocumentType == DocumentTemplateTypeEnum.Contract).ToList().TransferToProjectDocumentDtos() });
            //
            groupDtos.Add(new ProjectDocumentGroupDto() { Type = DocumentTemplateTypeEnum.Invoice, Data = dto.Where(x => x.DocumentType == DocumentTemplateTypeEnum.Invoice).ToList().TransferToProjectDocumentDtos() });

            return groupDtos;
        }

        public static ProjectDocumentDto TransferToProjectDocumentDto(this DocumentDto dto)
        {
            ProjectDocumentDto result = new ProjectDocumentDto()
            {
                Id = dto.Id,
                DocumentType = dto.DocumentType,
                Description = dto.Description,
                Name = dto.Name
            };

            return result;
        }

        public static List<ProjectDocumentDto> TransferToProjectDocumentDtos(this List<DocumentDto> dtos)
        {
            List<ProjectDocumentDto> result = dtos.Select(x => x.TransferToProjectDocumentDto()).ToList();
            return result;
        }

        public static DocumentDto TransferToDocumentDto(this DocumentStructureDto structureDto)
        {
            DocumentDto result = new DocumentDto()
            {
                Date = structureDto.Version.Date,
                Description = structureDto.Layout.Description,
                DocumentType = structureDto.Type,
                FileName = structureDto.Output.FileName,
                TemplateId = structureDto.Layout.TemplateId,
                LetterheadId = structureDto.Layout.LetterheadId,
                Name = structureDto.Version.Number,
                Subject = structureDto.Output.Subject,
                Number = structureDto.Version.Number,
                OpenKitsAndCases = structureDto.Output.OpenKitsAndCases,
                Text = structureDto.Layout.Description,
                UseLetterhead = structureDto.Output.UseLetterhead,
                ProjectId = structureDto.LinkedItem.LinkId,
                PaymentConditionId = structureDto.Financial?.PaymentConditionId,
                DueDate = structureDto.Financial?.DueDate
            };

            return result;
        }

        public static InvoiceDto TransferToInvoiceDto(this DocumentDto document)
        {
            InvoiceDto result = new InvoiceDto()
            {
                CreationDate = document.CreationDate,
                FileName = document.FileName,
                DueDate = document.DueDate,
                Name = document.Name,
                OpenKitsAndCases = document.OpenKitsAndCases,
                State = InvoiceStateEnum.New,
                Document = document,
                ProjectId = document.ProjectId
            };

            return result;
        }

        public static InvoiceDto PrepareInvoiceDto(this DocumentDto document, ProjectDto project)
        {
            InvoiceDto result = new InvoiceDto()
            {
                CreationDate = document.CreationDate,
                FileName = document.FileName,
                DueDate = document.DueDate,
                Name = document.Name,
                OpenKitsAndCases = document.OpenKitsAndCases,
                State = InvoiceStateEnum.New,
                Document = document,
                ProjectId = document.ProjectId,
                ClientId = project.ClientContactId,
                Date = DateTime.Now,
                GeneratedOn = DateTime.Now,
                PaymentConditionId = document.PaymentConditionId,
            };

            return result;
        }
    }
}