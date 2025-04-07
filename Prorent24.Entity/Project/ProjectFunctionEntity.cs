using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Project
{
    [Table("dbo_project_functions")]
    public class ProjectFunctionEntity : BaseEntity
    {
        public int? ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }

        public int? ParentFunctionId { get; set; }

        [ForeignKey("ParentFunctionId")]
        public virtual ProjectFunctionEntity ParentFunction { get; set; }

        public int? ProjectFunctionGroupId { get; set; }

        [ForeignKey("ProjectFunctionGroupId")]
        public virtual ProjectFunctionGroupEntity ProjectFunctionGroup { get; set; }

        public ProjectFunctionTypeEnum Type { get; set; }

        public string ExternalName { get; set; }

        public string InternalName { get; set; }

        public int TimeBeforeInMinutes { get; set; }

        public TimeTypeEnum TimeBeforeType { get; set; }

        public int TimeAfterInMinutes { get; set; }

        public TimeTypeEnum TimeAfterType { get; set; }

        public bool TakeTimeFromLocation { get; set; }

        public int DurationInMinutes { get; set; }

        public TimeTypeEnum DurationType { get; set; }

        public int BreakInMinutes { get; set; }

        public TimeTypeEnum BreakType { get; set; }

        public decimal? Distance { get; set; }

        [Column(TypeName = "DECIMAL")]
        public decimal RentalHourRate { get; set; }

        [Column(TypeName = "DECIMAL")]
        public decimal RentalFixed { get; set; }

        [Column(TypeName = "DECIMAL")]
        public decimal SubhireHourRate { get; set; }

        [Column(TypeName = "DECIMAL")]
        public decimal SubhireFixed { get; set; }

        public int? VatClassId { get; set; }

        [ForeignKey("VatClassId")]
        public VatClassEntity VatClass { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool IncludeInPrice { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool ShowInPlaner { get; set; }

        public string CustomerRemark { get; set; }

        public string PlannerRemark { get; set; }

        public string CrewMemberRemark { get; set; }

        public int? Quantity { get; set; }

        public EntryTimeTypeEnum? PlanFromTimeType { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? PlanFrom { get; set; }

        public EntryTimeTypeEnum? PlanUntilTimeType { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? PlanUntil { get; set; }


        public EntryTimeTypeEnum? UseFromTimeType { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? UseFrom { get; set; }

        public EntryTimeTypeEnum? UseUntilTimeType { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? UseUntil { get; set; }

        //public int? DurationTime { get; set; }

        //public TimeTypeEnum? DurationTimeType { get; set; }

        public int? TimeFrameId { get; set; }

        [ForeignKey("TimeFrameId")]
        public virtual ProjectTimeEntity ProjectTime { get; set; }

        public decimal TotalCosts { get; set; }

        public decimal TotalPrice { get; set; }

        public ProjectFunctionAddTimeEnum GlobalAddTimeType { get; set; }
        public decimal? TotalIncVat { get; set; }

    }
}
