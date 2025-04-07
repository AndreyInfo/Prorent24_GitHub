using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Mail
{
    public interface IMailService
    {
        Task SendMail(string resiverEmail, string body);
    }
}
