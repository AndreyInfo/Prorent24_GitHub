using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Directory
{
    [Table("sys_directory_locs")] //  , Schema = "sys" not working for sqlite
    public class DirectoryLocEntity
    {
        public int DirectoryId { get; set; }

        [ForeignKey("DirectoryId")]
        public virtual DirectoryEntity Directory { get; set; }
        public string Lang { get; set; }
        public string Name { get; set; }
    }
}
