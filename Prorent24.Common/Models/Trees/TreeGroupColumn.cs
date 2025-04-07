using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Common.Models.Trees
{
    public class TreeGroupColumn
    {
        public string GroupName { get; set; }
        public List<TreeColumn> Collection { get; set; }
    }
}
