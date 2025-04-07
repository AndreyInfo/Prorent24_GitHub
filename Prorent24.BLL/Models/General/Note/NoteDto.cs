using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.General.Note
{
    public class NoteDto : BaseDto<int>
    {
        public ConfidentialTypeEnum Confidential { get; set; }

        public string Text { get; set; }

        public ModuleTypeEnum BelongsTo { get; set; }

        public int? TaskId { get; set; }

        public string CrewMemberId { get; set; }

        public int? ContactId { get; set; }

        public int? VehicleId { get; set; }

        public int? EquipmentId { get; set; }

        public int? ProjectId { get; set; }

        public int? InspectionId { get; set; }
        public int? RepairId { get; set; }
        public int? SubhireId { get; set; }
        public int? InvoiceId { get; set; }
    }
}
