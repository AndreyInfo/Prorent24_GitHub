using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Prorent24.Common.Hubs
{
    public class EventModel
    {
        public EventModel(EventType eventType, EventData data)
        {
            EventType = eventType;
            Data = data;
        } 

        [JsonConverter(typeof(StringEnumConverter))]
        public EventType EventType { get; set; }

        public EventData Data { get; set; }
    }

    public class EventData
    {
        public string Title { get; set; }

        public string Message { get; set; }
    }

    public enum EventType
    {
        [EnumMember(Value = "info")]
        Info,
        [EnumMember(Value = "warning")]
        Warning,
        [EnumMember(Value = "error")]
        Error
    }
}
