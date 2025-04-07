using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.Configuration.Financial
{
    [Table("sys_ledgers")]
    public class LedgerEntity:BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(24)]
        public string AccountingCode { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool IsSystem { get; set; }      
    }
}
