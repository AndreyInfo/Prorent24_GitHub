using Prorent24.Entity.Base;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Settings
{
    [Table("sys_project_inspections")]
    public class PeriodicInspectionEntity : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int FrequencyInterval { get; set; }
        public TimeUnitTypeEnum FrequencyUnitType { get; set; }
    }
}
