using Prorent24.Common.Attributes;
using Prorent24.Entity.Base;
using Prorent24.Enum.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.BLL.Models.Configuration.CustomerCommunication
{
    public class LetterheadDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 6)]
        public string BackgroundName { get; set; }

        public string BackgroundUrl { get; set; }

        public PageSizeTypeEnum? PageSize { get; set; }

        public double? PageWidth { get; set; }

        public double? PageHeight { get; set; }

        public double? TopMargin { get; set; }

        public double? RightMargin { get; set; }

        public double? BottomMargin { get; set; }

        public double? LeftMargin { get; set; }

        public bool PageNumbers { get; set; }

        [IncludeToGrid(Order = 7)]
        public bool ShowAtInvoices { get; set; }

        [IncludeToGrid(Order = 8)]
        public bool ShowAtQuotations { get; set; }

        public bool DisplayAtContracts { get; set; }

        public bool ShowAtSubhireSlips { get; set; }

        public bool ShowAtReminders { get; set; }

        public bool ShowAtCrewMemberSlips { get; set; }

        public bool ShowAtTransportSlips { get; set; }

        public bool ShowOnEquipmentSlips { get; set; }

        public bool ShowAtRepairSlips { get; set; }

        [IncludeToGrid(Order = 9)]
        public bool ShowAtOtherSlips { get; set; }

    }
}
