using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Prorent24.Common.Models.Grids
{
    public class EntityGrid
    {
        public string Entity { get; set; } // EntityEnum
        public string EntityKey { get; set; }
        public string[] Columns { get; set; }
        public bool HasColumnKey { get; set; }
    }
}
