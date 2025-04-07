using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Common.Models.Filters
{
   public class SelectedFilter
    {
        /// <summary>
        /// Filter type (folder or etc)
        /// </summary>
        public string FilterType { get; set; }

        /// <summary>
        /// Selected all for tags
        /// </summary>
        public bool SelectedAll { get; set; }

        /// <summary>
        /// List Values for filters
        /// </summary>
        public List<object> Values { get; set; }
    }
}
