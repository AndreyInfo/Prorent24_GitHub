using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.General;

namespace Prorent24.BLL.Models.General.Modules
{
    public class ModuleDto
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ModuleTypeEnum ModuleType { get; set; }
        public string Name { get; set; }
        public short Order { get; set; }
    }
}
