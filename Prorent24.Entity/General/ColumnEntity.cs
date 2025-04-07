using Prorent24.Entity.Base;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.General
{
    [Table("sys_columns")]
    public class ColumnEntity : Entity<int>
    {
        public EntityEnum Entity { get; set; } //Cущность
        public bool IsEntity { get; set; } // Является сущностю, для вложеных 
        public bool IsKey { get; set; } = false; //Ключ Id или нет
        public string KeyType { get; set; } //Тип если нужно а нет то будет братся с модели
        public string KeyName { get; set; } // Имя поля
        public bool IsDisplay { get; set; } // Будет включен но не отображен в гриде
        public string DisplayName { get; set; } // Подставить ключ  для локаливации
        public short DefaultOrder { get; set; }

        public ColumnGroupEnum ColumnGroup { get; set; } // К какой группе относится
    }
}
