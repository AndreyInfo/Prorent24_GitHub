using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Transfers.General;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.General.Module;
using Prorent24.Entity;
using Prorent24.Entity.Configuration.Roles;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Module
{
    /// <summary>
    /// Module Service
    /// </summary>
    internal class ModuleService : BaseService, IModuleService
    {
        IModuleStorage _moduleStorage;

        public ModuleService(IHttpContextAccessor httpContextAccessor, IUserRoleStorage userRoleStorage, 
            IModuleStorage moduleStorage, IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _moduleStorage = moduleStorage;
        }


        /// <summary>
        /// Get menu items
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleDto>> GetModuleItems()
        {
            List<ModuleEntity> moduleEntities = await _moduleStorage.GetAll();

            if (!moduleEntities.Any())
            {
                await ImportModules();
                moduleEntities = await _moduleStorage.GetAll();
            }

            return moduleEntities.TransferToModuleDto();
        }



        /// <summary>
        /// Get menu items
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleDto>> GetModuleItemsBaseOnPermission()
        {
            List<ModuleEntity> moduleEntities = await _moduleStorage.GetAll();

            if (!moduleEntities.Any())
            {
                await ImportModules();
                moduleEntities = await _moduleStorage.GetAll();
            }
            string userId = GetUserId();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                List<RolePermissionEntity> permissions = await _userRoleStorage.GetUserPermissionByModule(userId);
                moduleEntities = (from module in moduleEntities
                                  join permsn in permissions on module.ModuleType equals permsn.PermissionDirectory?.ModuleType
                                  where permsn.Value?.ToLower() == "true"
                                  select module).ToList();

            }
            return moduleEntities.Distinct().ToList().TransferToModuleDto();
        }





        public async Task ImportModules()
        {
            var modules = new List<ModuleDto>()
            {
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Dashboard,
                    Name = "Dashboard",
                    Order = (int)ModuleTypeEnum.Dashboard

                },
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Warehouse,
                    Name = "Warehouse",
                    Order = (int)ModuleTypeEnum.Warehouse
                },
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Projects,
                    Name = "Projects",
                    Order = (int)ModuleTypeEnum.Projects
                },
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.CrewPlanner,
                    Name = "Crew Planner",
                    Order = (int)ModuleTypeEnum.CrewPlanner
                },
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Subhire,
                    Name = "Subhire",
                    Order = (int)ModuleTypeEnum.Subhire
                },
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Invoices,
                    Name = "Invoices",
                    Order = (int)ModuleTypeEnum.Invoices
                },
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Equipment,
                    Name = "Equipment",
                    Order = (int)ModuleTypeEnum.Equipment
                },
                 new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Contacts,
                    Name = "Contacts",
                    Order = (int)ModuleTypeEnum.Contacts
                },
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.CrewMembers,
                    Name = "Crew Members",
                    Order = (int)ModuleTypeEnum.CrewMembers
                },
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Vehicles,
                    Name = "Vehicles",
                    Order = (int)ModuleTypeEnum.Vehicles
                },
                 new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Tasks,
                    Name = "Tasks",
                    Order = (int)ModuleTypeEnum.Tasks
                },
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.TimeRegistration,
                    Name = "Time Registration",
                    Order = (int)ModuleTypeEnum.TimeRegistration
                },
                 new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Maintenance,
                    Name = "Maintenance",
                    Order = (int)ModuleTypeEnum.Maintenance
                },
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Statistics,
                    Name = "Statistics",
                    Order = (int)ModuleTypeEnum.Statistics
                },
                new ModuleDto()
                {
                    ModuleType = ModuleTypeEnum.Configuration,
                    Name = "Configuration",
                    Order = (int)ModuleTypeEnum.Configuration
                }
            };

            await _moduleStorage.ImportModules(modules.TransferToModuleEntity());
        }
    }
}
