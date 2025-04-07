using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Models.Project;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.Invoice;
using System;
using System.Collections.Generic;

namespace Prorent24.BLL.Models.General.Document
{
    public class DocumentDto : BaseDto<int>
    {
        public string Name { get; set; }
        public DocumentTemplateTypeEnum DocumentType { get; set; }
        public string Description { get; set; }
        public bool Confidential { get; set; }
        public string Path { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DueDate { get; set; }
        public int? FileId { get; set; }
        // public virtual FileEntity File { get; set; }
        public DateTime? GeneratedOn { get; set; } // repeat 
        public string GeneratedById { get; set; }
        // public UserDto GenerationBy { get; set; }
        // ========

        public int? TemplateId { get; set; } // беремо при створенні по замовчуванню
        public DocumentTemplateDto Template { get; set; }
        public int? LetterheadId { get; set; } // беремо при створенні по замовчуванню
        public LetterheadDto Letterhead { get; set; }

        public bool UseLetterhead { get; set; }
        public string Subject { get; set; }
        public string FileName { get; set; }
        public string Text { get; set; }

        public int? ProjectId { get; set; }
        public int? InvoiceId { get; set; }
        public OpenKitsAndCasesTypeEnum? OpenKitsAndCases { get; set; } = OpenKitsAndCasesTypeEnum.DisplayAllContent;

        public ProjectDto Project { get; set; }
        public InvoiceDto Invoice { get; set; }
        public CompanyDetailsDto Company { get; set; }
        public int? PaymentConditionId { get; set; } // Default

        // for project te
        public List<ProjectEquipmentGridDto> Equipments { get; set; }
        public List<ProjectPlanningGridDto> PlannedCrewMembers { get; set; }
        public List<ProjectPlanningGridDto> PlannedTransport { get; set; }
        public ProjectFinancialDto FinancialInfo { get; set; }
        public ProjectFinancialCategoryDto ProjectFinancialCategory { get; set; }
        public List<ProjectAdditionalCostDto> AdditionalCosts { get; set; }

        //public ModuleTypeEnum? BelongsTo { get; set; }
    }
}
