using Prorent24.Entity.Base;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.Configuration.Settings
{
    [Table("sys_extra_input_fields")]
    public class ExtraInputFieldEntity : BaseEntity
    {
        public string Name { get; set; }
        public EntityEnum BelongsTo { get; set; }
        public EntryFieldTypeEnum EntryFieldType { get; set; }
        public string ChoiceValues { get; set; } // for EntryFieldTypeEnum.DropDownList
        public string DefaultValue { get; set; }
        public bool UseInSearches { get; set; }
    }
}
