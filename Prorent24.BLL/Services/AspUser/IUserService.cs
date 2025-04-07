using Prorent24.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.AspUser
{
   public interface IUserService
    {
        Task<User> Create(User model);
        Task<User> Update(User model);
        
    }
}
