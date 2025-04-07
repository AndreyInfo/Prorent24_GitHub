using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Invoice;
using Prorent24.WebApp.Models.Contact;
using Prorent24.WebApp.Models.CrewMember;
using Prorent24.WebApp.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Invoice
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public ContactViewModel Client { get; set; }
        public int? ContactPersonId { get; set; }
        public ContactPersonViewModel ContactPerson { get; set; }
        public int? ProjectId { get; set; }
        public ProjectViewModel Project { get; set; }
        // даный проект был создан
        public string OwnerId { get; set; }
        public CrewMemberShortViewModel Owner { get; set; }
        //

        public string AccountingCode { get; set; }
        public string Name { get; set; } // Authogenerated name of Invoice
        public decimal? CreditDebit { get; set; }
        public string FileName { get; set; }
        public DateTime? GeneratedOn { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OpenKitsAndCasesTypeEnum OpenKitsAndCases { get; set; }

        public int? PaymentConditionId { get; set; } // Default
        public int? PaymentMethodId { get; set; } // System default
        public bool PriceCalculationBasedOnProject { get; set; }

        public string Subject { get; set; }
        public string Text { get; set; }
        public bool UseLetterhead { get; set; }
        public int? VatSchemeId { get; set; } // VATCode

        // FINANCIAL
        public decimal? TotalNewPrice { get; set; }

        // AMOUNTS
        public decimal? PercentagePartialInvoice { get; set; }
        public decimal? PriceExcludeVAT { get; set; }
        public decimal? PriceIncludeVAT { get; set; }
        public int? TotalSeparateInvoiceLines { get; set; }
        public decimal? VAT { get; set; }

        // DATES
        public DateTime? Date { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? SendAtDate { get; set; }
        // може бути декілька відправок, що робити якщо були зміни після відправки інвойсу


        public int? TemplateId { get; set; }
        public int? LetterheadId { get; set; }
        public string Number { get; set; }

        //
        public List<InvoiceLineViewModel> InvoiceLines { get; set; }
        public List<InvoiceExcludedViewModel> ExcludedInvoices { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public InvoiceStateEnum? State { get; set; }
    }
}
