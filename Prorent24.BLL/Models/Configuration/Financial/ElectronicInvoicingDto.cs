using Prorent24.BLL.Models.Directory;
using Prorent24.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Configuration.Financial
{
    public class ElectronicInvoicingDto
    {
        [IncludeToGrid(Order = 5)]
        public string IdentificationNumber { get; set; }

        [IncludeToGrid(Order = 6)]
        public string IdentificationScheme { get; set; }

        [IncludeToGrid(Order = 7 ,IsEntity = true, EntityKey = "Directory")]
        public int Currency { get; set; } //DirectoryId

        public virtual DirectoryDto Directory { get; set; }

    }
}