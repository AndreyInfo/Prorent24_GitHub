using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.General.File
{
    /// <summary>
    /// FileViewModel
    /// </summary>
    public class FileViewModel
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string File
        {
            get
            {
                return Name;
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Confidential
        /// </summary>
        public bool Confidential { get; set; }

        /// <summary>
        /// Path
        /// </summary>
        public string Path { get; set; }

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
