using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Base
{
    public class BaseEntity : Entity<int>, ICloneable
    {
        [Column(TypeName = "DATETIME")]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? LastModifiedDate { get; set; }

        public string CreationUserId { get; set; }

        [ForeignKey("CreationUserId")]
        public virtual User CreationUser { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
