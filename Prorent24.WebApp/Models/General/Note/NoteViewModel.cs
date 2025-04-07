using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;

namespace Prorent24.WebApp.Models.General.Note
{
    public class NoteViewModel
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ConfidentialTypeEnum Confidential { get; set; }

        public string Text { get; set; }

        /// <summary>
        /// BelongsTo Enum
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ModuleTypeEnum BelongsTo { get; set; }

        /// <summary>
        /// ReferenceId by EntityEnum
        /// </summary>
        public string ReferenceId { get; set; }
    }
}
