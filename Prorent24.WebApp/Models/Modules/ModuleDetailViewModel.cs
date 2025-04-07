using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Entity;

namespace Prorent24.WebApp.Models.Modules
{
    /// <summary>
    /// Module detail ciew model
    /// </summary>
    public class ModuleDetailViewModel
    {
        /// <summary>
        /// Entity
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DetailsEntityEnum Entity { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public object Data { get; set; }
    }
}
