using MediatR;

namespace Prorent24.BLL.Models.Handler
{
    public class SendPasswordHandlerModel : INotification
    {
        public string Url { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
