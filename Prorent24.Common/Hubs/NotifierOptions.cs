namespace Prorent24.Common.Hubs
{
    public class NotifierOptions
    {
        public PushType Type { get; set; }
        
        public EventType Event { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public string Module { get; set; }
    }
}
