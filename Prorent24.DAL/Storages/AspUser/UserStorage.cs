using Prorent24.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using Prorent24.Entity;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.AspUser
{
    internal class UserStorage : IUserStorage
    {
        protected readonly IRepository<User> _repositoryUser;

        protected readonly IUnitOfWork _unitOfWork;

        public UserStorage(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryUser = _unitOfWork.GetRepository<User>();
        }

        public async Task<User> InsertUser(User entity)
        {
            try
            {
                await _repositoryUser.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                User user = await _repositoryUser.FindAsync(entity.Id);
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> UpdateUser(User entity)
        {
            _repositoryUser.Update(entity);
            _unitOfWork.SaveChanges();
            User user = await _repositoryUser.FindAsync(entity.Id);
            return user;
        }
    }
}
