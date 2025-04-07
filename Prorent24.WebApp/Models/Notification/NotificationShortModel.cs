using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Notification
{
    public class NotificationShortModel
    {
        public int Id { get; set; }

        public string Theme { get; set; }

        public string Message { get; set; }

        public bool IsRead { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]

        public ModuleTypeEnum? ModuleType { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
