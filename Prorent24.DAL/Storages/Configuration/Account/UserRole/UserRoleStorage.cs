using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Entity;
using Prorent24.Entity.Configuration.Roles;
using Prorent24.Entity.Identity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.Configuration.Account.UserRole
{
    internal class UserRoleStorage : IUserRoleStorage
    {
        protected readonly IRepository<PermissionDirectoryEntity> _repositoryPermission;
        protected readonly IRepository<Role> _repositoryRole;
        protected readonly IRepository<UserRoleEntity> _repositoryUserRole;
        protected readonly IRepository<RolePermissionEntity> _repositoryRolePermission;
        protected readonly IUnitOfWork _unitOfWork;
        public UserRoleStorage(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryPermission = _unitOfWork.GetRepository<PermissionDirectoryEntity>();
            _repositoryRole = _unitOfWork.GetRepository<Role>();
            _repositoryRolePermission = _unitOfWork.GetRepository<RolePermissionEntity>();
            _repositoryUserRole = _unitOfWork.GetRepository<UserRoleEntity>();
        }

        /// <summary>
        /// Get list UserRoles
        /// </summary>
        /// <returns></returns>
        public Task<IPagedList<Role>> GetAll()
        {
            return _repositoryRole.GetPagedListAsync();
        }

        /// <summary>
        /// Get list UserRoles
        /// </summary>
        /// <returns></returns>
        public Task<List<Role>> GetList()
        {
            return _repositoryRole.TableNoTracking.ToListAsync();
        }


        public async Task<List<PermissionDirectoryEntity>> GetPermission()
        {
            return await _repositoryPermission.TableNoTracking.Include(x => x.Children).ToListAsync();
        }

        public async Task ImportPermission(List<PermissionDirectoryEntity> permissions)
        {
            await _repositoryPermission.InsertAsync(permissions);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Role> GetById(string id)
        {
            Role role = await _repositoryRole.FindAsync(id);
            List<RolePermissionEntity> rolePermission = await _repositoryRolePermission.TableNoTracking.Include(x => x.PermissionDirectory.Children).Include(y => y.PermissionDirectory.Children).ToListAsync();
            role.RolePermissions = rolePermission.Where(x => x.RoleId == role.Id).ToList();
            return role;
        }

        public async Task<Role> GetByUserId(string id)
        {
            Prorent24.Entity.Identity.UserRoleEntity userRole = _repositoryUserRole.TableNoTracking.FirstOrDefault(x => x.UserId == id);
            Role role = userRole!=null ? await _repositoryRole.FindAsync(userRole.RoleId) : null;
            if(role == null)
            {
                return new Role();
            }
            //Role role = _repositoryRole.TableNoTracking.Include(x => x.UserRoles).FirstOrDefault(x => x.UserRoles!=null? x.UserRoles.FirstOrDefault(y => y.UserId == id) != null : false);
            List<RolePermissionEntity> rolePermission = await _repositoryRolePermission.TableNoTracking.Include(x => x.PermissionDirectory.Children).Include(y => y.PermissionDirectory.Children).ToListAsync();
            role.RolePermissions = rolePermission.Where(x => x.RoleId == role.Id).ToList();
            return role;
        }

        public async Task<List<RolePermissionEntity>> GetUserPermissionByModule(string id, ModuleTypeEnum? module=null)
        {
            Prorent24.Entity.Identity.UserRoleEntity userRole = _repositoryUserRole.TableNoTracking.FirstOrDefault(x => x.UserId == id);

            if (userRole != null)
            {
                Role role = await _repositoryRole.FindAsync(userRole.RoleId);
                //Role role = _repositoryRole.TableNoTracking.Include(x => x.UserRoles).FirstOrDefault(x => x.UserRoles!=null? x.UserRoles.FirstOrDefault(y => y.UserId == id) != null : false);
                List<RolePermissionEntity> rolePermission = await _repositoryRolePermission.TableNoTracking.Include(x => x.PermissionDirectory.Children).ThenInclude(y => y.Children).ToListAsync();
                var permissions = rolePermission.Where(x => x.RoleId == role.Id).ToList(); // && x.PermissionDirectory.ModuleType == module
                return permissions;
            }
            else
            {
                return new List<RolePermissionEntity>();
            }
           
        }

        public async Task ImportRolePermission(List<RolePermissionEntity> rolePermissions)
        {
            await _repositoryRolePermission.InsertAsync(rolePermissions);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Role> UpdateRole(Role model)
        {
            Role role = await _repositoryRole.FindAsync(model.Id);
            role.Name = model.Name;
            role.NormalizedName = model.Name.ToUpper();
            role.Rate = model.Rate;
            _repositoryRole.Update(role);
            if (model.RolePermissions != null)
            {
                _repositoryRolePermission.Update(model.RolePermissions);
            }
            _unitOfWork.SaveChanges();

            return model;
        }

        public async Task<Role> InsertRole(Role entity)
        {
            try
            {
                await _repositoryRole.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                Role role = await _repositoryRole.FindAsync(entity.Id);
                List<RolePermissionEntity> rolePermission = await _repositoryRolePermission.TableNoTracking.Include(x => x.PermissionDirectory.Children).Include(y => y.PermissionDirectory.Children).ToListAsync();
                role.RolePermissions = rolePermission.Where(x => x.RoleId == role.Id).ToList();
                return role;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeleteRole(string id)
        {
            //видаляємо спершу всі permission ролі
            var permissions = _repositoryRolePermission.TableNoTracking.Where(z => z.RoleId == id);
            _repositoryRolePermission.Delete(permissions);
            
            var res = _repositoryRole.Table.First(x => x.Id == id);
            _repositoryRole.Delete(res);
            var result = _unitOfWork.SaveChanges();
            return result > 0;
        }
    }
}
