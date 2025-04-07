using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Equipment;
using Prorent24.WebApp.Models.CrewMember;
using Prorent24.WebApp.Models.General.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Vehicle
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FolderId { get; set; }
        public string FolderName { get; set; }
        public bool DeployedMultipleTimesSimultaneously { get; set; }

        // Technical information
        public string RegistrationNumber { get; set; }

        public decimal LoadCapacity { get; set; } // in KG

        [JsonConverter(typeof(StringEnumConverter))]
        public WeightUnitEnum? LoadCapacityUnit { get; set; }  //kg

        public DateTime ? MOTDate { get; set; }

        // FINACIAL
        public decimal DayilCosts { get; set; }
        public decimal VariableCosts { get; set; }

        // DETAILS
        public bool DisplayInPlanner { get; set; }
        public string LoadingSurface { get; set; } // in ??
        public decimal Length { get; set; } // cm
        public decimal Width { get; set; } // cm
        public decimal Height { get; set; } // cm

        [JsonConverter(typeof(StringEnumConverter))]
        public LenghtUnitEnum? LengthUnit { get; set; }// сm

        [JsonConverter(typeof(StringEnumConverter))]
        public LenghtUnitEnum? WidthUnit { get; set; }  // cm

        [JsonConverter(typeof(StringEnumConverter))]
        public LenghtUnitEnum? HeightUnit { get; set; }  // cm 

        public int Seats { get; set; }
        public string Description { get; set; } // HTML

        public NoteViewModel Note { get; set; }

        public DateTime? InsuranceDate { get; set; }

        public bool IsPublic { get; set; }

        public List<CrewMemberShortViewModel> CrewMembers { get; set; }
    }
}
