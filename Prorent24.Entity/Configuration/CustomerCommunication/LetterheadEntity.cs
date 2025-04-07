using Prorent24.Entity.Base;
using Prorent24.Enum.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.CustomerCommunication
{
    [Table("sys_letterheads")]
    public class LetterheadEntity : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public PageSizeTypeEnum? PageSize { get; set; }

        public double? PageWidth { get; set; } // in cm

        public double? PageHeight { get; set; } // in cm

        public double? TopMargin { get; set; } // in cm

        public double? RightMargin { get; set; } // in cm

        public double? BottomMargin { get; set; } // in cm

        public double? LeftMargin { get; set; } // in cm

        public bool PageNumbers { get; set; }

        public bool ShowAtInvoices { get; set; }

        public bool ShowAtQuotations { get; set; }

        public bool DisplayAtContracts { get; set; }

        public bool ShowAtSubhireSlips { get; set; }

        public bool ShowAtReminders { get; set; }

        public bool ShowAtCrewMemberSlips { get; set; }

        public bool ShowAtTransportSlips { get; set; }

        public bool ShowOnEquipmentSlips { get; set; }

        public bool ShowAtRepairSlips { get; set; }

        public bool ShowAtOtherSlips { get; set; }


    }
}
