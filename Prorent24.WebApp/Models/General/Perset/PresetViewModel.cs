using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.General.Filter;
using System.Collections.Generic;

namespace Prorent24.WebApp.Models.General.Perset
{
    /// <summary>
    /// Preset View Model
    /// </summary>
    public class PresetViewModel
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Preset Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Default Preset
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Module Type
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ModuleTypeEnum ModuleType { get; set; }

        /// <summary>
        /// Filters
        /// </summary>
        public List<SelectedFilterViewModel> Filters { get; set; }


    }
}
