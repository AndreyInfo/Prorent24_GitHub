using Microsoft.Extensions.Options;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.BLL.Services.Configuration.CustomerCommunication.Communication;
using Prorent24.Common.ApplicationSettings;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Mail
{
    internal class MailService : IMailService
    {
        private readonly ICustomerCommmunicationService _customerCommmunicationService;
        private readonly AppSettings _appSettings;

        public MailService(ICustomerCommmunicationService customerCommmunicationService, IOptions<AppSettings> config)
        {
            _customerCommmunicationService = customerCommmunicationService;
            _appSettings = config.Value;
        }

        public async Task SendMail(string resiverEmail, string body)
        {
            try
            {
                CustomerCommmunicationDto commmunication = await _customerCommmunicationService.GetAsync();

                string emailfrom = !string.IsNullOrWhiteSpace(commmunication?.EmailFrom) ? commmunication?.EmailFrom : _appSettings.EmailConfiguration.MailAddress;

                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress(_appSettings.EmailConfiguration.MailAddress, emailfrom);
                // кому отправляем
                MailAddress to = new MailAddress(resiverEmail);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Prorent24";
                // текст письма
                m.Body = body;
                // письмо представляет код html
                m.IsBodyHtml = false;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient(_appSettings.EmailConfiguration.SmtpAddress, _appSettings.EmailConfiguration.SmtpPort);
                // логин и пароль
                smtp.Credentials = new NetworkCredential(_appSettings.EmailConfiguration.MailAddress, _appSettings.EmailConfiguration.MailPassword);
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(m);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
