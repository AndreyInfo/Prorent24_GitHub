using System;
using System.Collections.Generic;

namespace Prorent24.Common.Models.Columns
{
    public class Column
    {
        public string EntityKey { get; set; }
        public string EntityKeyType { get; set; }
        public string Type { get; set; }
        public string Key { get; set; }
        // коректно було б використовувати EntityKey, але тут він для іншого
        public string OriginalKey { get; set; }
        public bool IsDisplayName { get; set; }
        public int Order { get; set; }
        public bool IsEntity { get; set; }
        public object Value { get; set; }
        public double Width { get; set; }
        public bool Required { get; set; }
        public object Values { get; set; }
    }
}