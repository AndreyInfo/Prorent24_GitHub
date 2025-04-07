using MediatR;
using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Services.General.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Prorent24.BLL.Consumers.EmailConsumers
{
    public class SendPasswordHandler : INotificationHandler<SendPasswordHandlerModel>
    {
        private readonly IMailService _mailService;

        public SendPasswordHandler(IMailService mailService)
        {
            _mailService = mailService;
        }

        public Task Handle(SendPasswordHandlerModel notification, CancellationToken cancellationToken)
        {
            string body = $"Добро пожаловать в PRORENT24. Адрес платформы {notification.Url} Логин: {notification.Email} Пароль: {notification.Password}";
            return _mailService.SendMail(notification.Email, body);
        }
    }
}
