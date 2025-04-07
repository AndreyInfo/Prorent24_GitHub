using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Configuration.CustomerCommunication
{
    [Table("sys_salutations")]
    public class SalutationEntity: BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string View { get; set; }
    }
}
