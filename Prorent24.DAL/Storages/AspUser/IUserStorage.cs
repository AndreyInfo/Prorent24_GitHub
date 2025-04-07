using Prorent24.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.AspUser
{
    public interface IUserStorage
    {
        Task<User> UpdateUser(User entity);
        Task<User> InsertUser(User entity);
    }
}
