using Prorent24.BLL.Services.AspUser;
using Prorent24.DAL.Storages.AspUser;
using Prorent24.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.AspUser
{
    internal class UserService: IUserService
    {
        private readonly IUserStorage _userStorage;

        public UserService( IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public async Task<User> Create(User model)
        {
            User entity = await _userStorage.InsertUser(model);
            return entity;
        }
        

        public async Task<User> Update(User model)
        {
            User entity = await _userStorage.UpdateUser(model);
            return entity;
        }
    }
}
