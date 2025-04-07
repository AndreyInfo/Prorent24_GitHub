using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Configuration;

namespace Prorent24.WebApp.Models.Configuration.CustomerCommunication
{
    public class LetterheadViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PageSizeTypeEnum? PageSize { get; set; }

        public double? PageWidth { get; set; }

        public double? PageHeight { get; set; }

        public double? TopMargin { get; set; }

        public double? RightMargin { get; set; }

        public double? BottomMargin { get; set; }

        public double? LeftMargin { get; set; }

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