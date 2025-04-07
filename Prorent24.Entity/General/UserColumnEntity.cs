using Prorent24.Entity.Base;
using Prorent24.Enum.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.General
{
    [Table("dbo_user_columns")]
    public class UserColumnEntity : BaseEntity
    {
        public EntityEnum Entity { get; set; }
        public string Column { get; set; }
        public int Order { get; set; } = -1;

        public bool ShowColumn { get; set; }

        public double WidthColumn { get; set; }
    }
}
