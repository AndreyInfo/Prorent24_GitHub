using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Invoice
{
    public class InvoiceTotalDto : BaseDto<int>
    {
        // FINANCIAL
        [IncludeToGrid(Order = 5 , DisplayName = "TotalPrice", ColumnGroup = ColumnGroupEnum.General)]
        public decimal? TotalNewPrice { get; set; }

        // AMOUNTS
        public decimal? PercentagePartialInvoice { get; set; }
        public decimal? PriceExcludeVAT { get; set; }
        public decimal? PriceIncludeVAT { get; set; }
        public decimal? TotalSeparateInvoiceLines { get; set; }
        public decimal? VAT { get; set; } // рассчитывается
        public decimal? ProjectSum { get; set; }

        public List<InvoiceVATDto> InvoiceVATs { get; set; } = new List<InvoiceVATDto>();
    }
}
