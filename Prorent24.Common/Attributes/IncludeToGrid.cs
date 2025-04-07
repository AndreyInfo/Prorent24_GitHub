//using Prorent24.Enum.Directory;
using System;

namespace Prorent24.Common.Attributes
{
    public class IncludeToGrid : Attribute
    {
        public bool IsKey = false;

        public bool IsEntity = false;

        public string ColumnName { get; set; }

        public string EntityKey = "";

        public string EntityKeyType = "";

        public string IncludeType = "";

        public bool IsDisplay = true;

        public string KeyType = string.Empty;

        public object ColumnGroup = null; //"General"; //ColumnGroupEnum.General

        public short Order = -1;

        public string DisplayName = string.Empty;

        public bool IsSystem = false;

        public bool Imported = false;

        public bool Required = false;

        public string InfoMsg { get; set; }

        public bool TreeColumn { get; set; }
        public int TreeColumnOrder { get; set; }
    }
}
