using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.General;

namespace Prorent24.WebApp.Models.General.Filter
{
    /// <summary>
    /// Filter View Model
    /// </summary>
    public class FilterListViewModel
    {
        /// <summary>
        /// Filter type
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public FilterEnum FilterType { get; set; }

        /// <summary>
        /// List data by filter type 
        /// </summary>
        public object Data { get; set; }
    }
}