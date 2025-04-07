using Prorent24.Entity.Base;
using Prorent24.Entity.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Subhire
{
    [Table("dbo_subhire_projects")]
    public class SubhireProjectEntity : BaseEntity
    {
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public ProjectEntity Project { get; set; }

        public int SubhireId { get; set; }

        [ForeignKey("SubhireId")]
        public SubhireEntity Subhire { get; set; }
    }
}
