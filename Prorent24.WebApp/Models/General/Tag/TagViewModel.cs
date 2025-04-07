using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;

namespace Prorent24.WebApp.Models.General.Tag
{
    /// <summary>
    /// Tag View Model
    /// </summary>
    public class TagViewModel
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Directory Unique Id
        /// </summary>
        public int DirectoryId { get; set; }

        /// <summary>
        /// Tag Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// BelongsTo Enum
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ModuleTypeEnum BelongsTo { get; set; }

        /// <summary>
        /// ReferenceId by EntityEnum
        /// </summary>
        public object ReferenceId { get; set; }

        /// <summary>
        /// Selected current item
        /// </summary>
        public bool Selected { get; set; }

    }
}
