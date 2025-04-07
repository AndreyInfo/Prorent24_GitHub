using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.General;
using System.Collections.Generic;

namespace Prorent24.WebApp.Models.General.Filter
{
    /// <summary>
    /// Selected filter
    /// </summary>
    public class SelectedFilterViewModel
    {
        /// <summary>
        /// Filter type (folder or etc)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public FilterEnum FilterType { get; set; }

        /// <summary>
        /// Selected all for tags
        /// </summary>
        public bool SelectedAll { get; set; }

        /// <summary>
        /// List ids for filters
        /// </summary>
        public List<object> Values { get; set; }
    }
}
