using Prorent24.Entity;
using Prorent24.Entity.Configuration.Roles;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Account.UserRole
{
    public interface IUserRoleStorage
    {
        Task<List<Role>> GetList();
        Task<List<PermissionDirectoryEntity>> GetPermission();
        Task ImportPermission(List<PermissionDirectoryEntity> permissions);
        Task<IPagedList<Role>> GetAll();
        Task<Role> GetById(string id);
        Task<Role> GetByUserId(string id);
        Task<List<RolePermissionEntity>> GetUserPermissionByModule(string id, ModuleTypeEnum? module=null);
        Task ImportRolePermission(List<RolePermissionEntity> rolePermissions);
        Task<Role> UpdateRole(Role entity);
        Task<Role> InsertRole(Role entity);
        bool DeleteRole(string id);
    }
}
