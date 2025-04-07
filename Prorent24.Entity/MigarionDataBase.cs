using Prorent24.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity
{
    [Table("sys_migration_database")]
    public class MigarionDataBase : Entity<int>
    {
        public string MigrationName { get; set; }
        public string MigrationData { get; set; }
        public bool Executed { get; set; }
    }
}
