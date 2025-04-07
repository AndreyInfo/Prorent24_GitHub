using System.Runtime.Serialization;

namespace Prorent24.Enum.CrewMember
{
    public enum SocialNetworkTypeEnum
    {
        [EnumMember(Value = "viber")]
        Viber = 1,
        [EnumMember(Value = "skype")]
        Skype = 2,
        [EnumMember(Value = "telegram")]
        Telegram = 3,
        [EnumMember(Value = "whatsapp")]
        Whatsapp = 4,
        [EnumMember(Value = "facebook")]
        Facebook = 5,
        [EnumMember(Value = "instagram")]
        Instagram = 6
    }
}
