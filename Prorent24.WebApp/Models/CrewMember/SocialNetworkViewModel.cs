using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.CrewMember;

namespace Prorent24.WebApp.Models.CrewMember
{
    public class SocialNetworkViewModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SocialNetworkTypeEnum Type { get; set; }

        public string Name { get; set; }

    }
}
