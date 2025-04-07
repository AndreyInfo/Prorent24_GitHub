using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.General.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Contact
{
    public interface IContactService : IBaseService<ContactDto, int>
    {
        Task<List<ModuleDetailDto>> GetDetails(int id);

        Task<List<ContactDto>> SearchContacts(string LikeString);
    }
}
