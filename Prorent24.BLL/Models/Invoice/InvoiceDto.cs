using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Document;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Invoice;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Invoice
{
    public class InvoiceDto : InvoiceTotalDto
    {
        // [IncludeToGrid(IsDisplay = false)]
        [JsonConverter(typeof(StringEnumConverter))]
        [IncludeToGrid(Order = 14, ColumnGroup = ColumnGroupEnum.General, KeyType = "enum")]
        public InvoiceStateEnum? State { get; set; }
        public int? ClientId { get; set; }
        public ContactDto Client { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General)]
        public string ClientName { get { return this.Client?.Name; } }

        // [IncludeToGrid(IsDisplay = false)]
        public int? ProjectId { get; set; }
        public ProjectDto Project { get; set; }

        //[IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General)]
        public string ProjectName { get { return this.Project?.Name; } }

        public string OwnerId { get; set; }
        public CrewMemberShortDto Owner { get; set; }

        //[IncludeToGrid(Order = 8, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public string OwnerName { get { return $"{this.Owner?.LastName} {this.Owner?.FirstName}"; } }
        

        //[IncludeToGrid(Order = 14, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string AccountingCode { get; set; }

        //[IncludeToGrid(Order = 15, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public string Name { get; set; } // Authogenerated name of Invoice

        //[IncludeToGrid(Order = 16, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public decimal? CreditDebit { get; set; }

        //[IncludeToGrid(Order = 17, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string FileName { get; set; }

        [IncludeToGrid(Order = 9, IncludeType = "dateshort", IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? GeneratedOn { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OpenKitsAndCasesTypeEnum? OpenKitsAndCases { get; set; } = OpenKitsAndCasesTypeEnum.DefaultKitOrCase;

        public int? PaymentConditionId { get; set; } // Default
        public PaymentConditionDto PaymentCondition { get; set; } // Default
        public int? PaymentMethodId { get; set; } // System default
        public PaymentMethodDto PaymentMethod { get; set; }
        public bool PriceCalculationBasedOnProject { get; set; }

        // public string Subject { get; set; }
        // public string Text { get; set; }
        // public bool UseLetterhead { get; set; }
        public int? VatSchemeId { get; set; } // VATCode
        public VatSchemeDto VatScheme { get; set; }

        // DATES
        [IncludeToGrid(Order = 10, IncludeType = "dateshort", ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? DueDate { get; set; }

        [IncludeToGrid(Order = 11, IncludeType = "dateshort", ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? Date { get; set; }

        //[IncludeToGrid(Order = 12, IncludeType = "dateshort", IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? PaymentDate { get; set; }

        //[IncludeToGrid(Order = 13, IncludeType = "dateshort", IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        public DateTime? SendAtDate { get; set; }
        // може бути декілька відправок, що робити якщо були зміни після відправки інвойсу

        //public int TemplateId { get; set; }
        //public int LetterheadId { get; set; }

        public DocumentDto Document { get; set; }
        public List<InvoiceLineDto> InvoiceLines { get; set; }
        public List<NoteDto> Notes { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<FileDto> Files { get; set; }
    }
}
