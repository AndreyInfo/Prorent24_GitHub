using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Services.CrewMember;
using Prorent24.BLL.Transfers.Configuration.AccountConfiguration;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.CrewMember;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.General.Module;
using Prorent24.Entity;
using Prorent24.Entity.Configuration.Roles;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.AccountConfiguration.UserRole
{
    internal class UserRoleService : BaseService, IUserRoleService
    {
        private readonly ICrewMemberStorage _crewMemberStorage;
        private readonly IModuleStorage _moduleStorage;
        public UserRoleService(IUserRoleStorage userRoleStorage, 
            IModuleStorage moduleStorage, 
            IHttpContextAccessor httpContextAccessor, 
            ICrewMemberStorage crewMemberStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _moduleStorage = moduleStorage;
            _crewMemberStorage = crewMemberStorage;
        }

        public async Task<UserRoleDto> GetById(string id)
        {
            Role entity = await _userRoleStorage.GetById(id);
            UserRoleDto userRole = entity.TransferToDto();
            return userRole;
        }
        public async Task<BaseListDto> GetPagedList()
        {
            IPagedList<Role> pagedList = await _userRoleStorage.GetAll();
            List<Role> listEntities = pagedList.Items.ToList();
            List<UserRoleDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<UserRoleDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.UserRoleEntity
            };
        }

        public async Task<UserRoleDto> UpdateRole(UserRoleDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.UserRoles);
            if (permission.Add)
            {
                Role entity = model.TransferToEntity();
                Role updateEntity = await _userRoleStorage.UpdateRole(entity);
                UserRoleDto dto = updateEntity.TransferToDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<UserRoleDto> InsertRole(UserRoleDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.UserRoles);
            if (permission.Add)
            {
                Role entity = model.TransferToEntity();
                Role updateEntity = await _userRoleStorage.InsertRole(entity);
                UserRoleDto dto = updateEntity.TransferToDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<List<PermissionDirectoryDto>> GetPermission()
        {
            List<PermissionDirectoryEntity> entity = await _userRoleStorage.GetPermission();
            return entity.Where(x => x.ParentPermissionId is null || x.ParentPermissionId == 0).Select(x => x.TransferToDto()).ToList();

        }

        public async Task<UserRoleDto> GetUserPermissions()
        {
            var role = await _userRoleStorage.GetByUserId(this.GetUserId());
            UserRoleDto userRole = role.TransferToDto();
            return userRole;
        }

        #region Initialize Roles
        public async Task InitPermission()
        {
            List<PermissionDirectoryEntity> permissionEntities = await _userRoleStorage.GetPermission();
            if (!permissionEntities.Any())
            {
                await ImportParentPermission();
                await ImportChildPermission();
            }
        }
        public async Task ImportParentPermission()
        {
            List<ModuleEntity> moduleEntities = await _moduleStorage.GetAll();
            List<PermissionDirectoryEntity> permissionDirectories = new List<PermissionDirectoryEntity>();
            foreach (ModuleEntity element in moduleEntities)
            {
                permissionDirectories.Add(new PermissionDirectoryEntity()
                {
                    PermissionName = element.Name,
                    ValueTypeId = PermissionValueTypeEnum.CheckBox,
                    ModuleType = element.ModuleType

                });
            }

            permissionDirectories.Add(new PermissionDirectoryEntity()
            {
                PermissionName = "General",
                ModuleType = ModuleTypeEnum.General,
                ValueTypeId = PermissionValueTypeEnum.CheckBox,
                //Children = new List<PermissionDirectoryEntity>()
                //{
                //    new PermissionDirectoryEntity()
                //            {
                //                PermissionName = "Own data",
                //                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                //                PermissionField = PermissionFieldEnum.OwnDataGeneral
                //            },
                //     new PermissionDirectoryEntity()
                //            {
                //                PermissionName = "Download files",
                //                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                //                PermissionField = PermissionFieldEnum.DownloadFilesGeneral
                //            }
                //}
            });
            permissionDirectories.Add(new PermissionDirectoryEntity()
            {
                PermissionName = "Messages",
                ModuleType = ModuleTypeEnum.Messages,
                ValueTypeId = PermissionValueTypeEnum.CheckBox

            });
            await _userRoleStorage.ImportPermission(permissionDirectories);
        }

        public async Task ImportChildPermission()
        {
            List<PermissionDirectoryEntity> entity = (await _userRoleStorage.GetPermission()).Where(x => x.ParentPermissionId is null || x.ParentPermissionId == 0).ToList();

            List<PermissionDirectoryEntity> permissionDirectories = new List<PermissionDirectoryEntity>();
            foreach (PermissionDirectoryEntity element in entity)
            {
                ModuleTypeEnum? moduleTypeEnum = element.ModuleType;

                switch (moduleTypeEnum)
                {
                    case ModuleTypeEnum.General:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Own data",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.OwnDataGeneral
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Download files",
                                ValueTypeId = PermissionValueTypeEnum.DownloadFilePermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.DownloadFilesGeneral
                            });
                            break;
                        }
                    case ModuleTypeEnum.MySchedule:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Own appointments",
                                ValueTypeId = PermissionValueTypeEnum.EntityShortModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.OwnAppointmentSchedule
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Own availability",
                                ValueTypeId = PermissionValueTypeEnum.EntityShortModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.OwnAvailabilitySchedule
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Appointments of others",
                                ValueTypeId = PermissionValueTypeEnum.EntityNoShortModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.AppointmentOtherSchedule
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Availability of others",
                                ValueTypeId = PermissionValueTypeEnum.EntityNoShortModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.AvailabilityOtherSchedule
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Project planning of other crew members",
                                ValueTypeId = PermissionValueTypeEnum.ProjectPlanningPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.ProjPlanningCrewMembSchedule
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Planned equipment for projects",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.PlannedEquipmentProjSchedule
                            });
                            break;
                        }
                    case ModuleTypeEnum.Warehouse:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Which projects",
                                ValueTypeId = PermissionValueTypeEnum.WhichProjectWarehousePermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.WhichProjWarehouse
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Change project status",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.ChangeProjectStatusWarehouse
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Add extra or other items",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.AddItemWarehouse
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Create repairs",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.CreateRepairsWarehouse
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Change stock in case of damage/loss",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.ChangeStockWarehouse
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Invoice the costs in case of damage/loss",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.InvoiceCostWarehouse
                            });
                            break;
                        }
                    case ModuleTypeEnum.Projects:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Which projects",
                                ValueTypeId = PermissionValueTypeEnum.WhichProjectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.WhichProjProject
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Change project",
                                ValueTypeId = PermissionValueTypeEnum.ChangeProjectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.ChangeProjProject
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Delete projects",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.DeleteProjProject
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Edit and delete default functions",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.EditDeleteFunctionProject
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Create invoices",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.CreateInvoiceProject
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Create quotations and contracts",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.CreateQuotContractProject
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Availability of equipment",
                                ValueTypeId = PermissionValueTypeEnum.AvailabilityOfEquipmentPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.AvailabilityEquipmentProject
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Change schedule",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.ChangeScheduleProject
                            });
                            break;
                        }
                    case ModuleTypeEnum.CrewPlanner:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Change schedule",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.ChangeScheduleCrewPlann
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Send planning emails",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.SendPlanEmailCrewPlann
                            });
                            break;
                        }
                    case ModuleTypeEnum.Subhire:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Edit subhire",
                                ValueTypeId = PermissionValueTypeEnum.EditSubhirePermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.EditSubhire
                            });

                            break;
                        }
                    case ModuleTypeEnum.Invoices:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Own invoices",
                                ValueTypeId = PermissionValueTypeEnum.EntityModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.OwnInvoice
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Invoices from other account managers",
                                ValueTypeId = PermissionValueTypeEnum.EntityFullModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.OtherAccountInvoice
                            });

                            break;
                        }
                    case ModuleTypeEnum.Equipment:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Equipment database",
                                ValueTypeId = PermissionValueTypeEnum.EntityModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.DatabaseEquipment
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Availability of equipment",
                                ValueTypeId = PermissionValueTypeEnum.AvailabilityOfEquipmentPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.AvailabilityEquipment
                            });

                            break;
                        }
                    case ModuleTypeEnum.Contacts:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Contacts database",
                                ValueTypeId = PermissionValueTypeEnum.EntityModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.DatabaseContract
                            });
                            break;
                        }
                    case ModuleTypeEnum.CrewMembers:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Crew member database",
                                ValueTypeId = PermissionValueTypeEnum.EntityModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.DatabaseCrewMember
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Login information",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.LogiInfoCrewMember
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Timeline crew members",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.TimelineCrewMember
                            });
                            break;
                        }
                    case ModuleTypeEnum.Vehicles:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Vehicles database",
                                ValueTypeId = PermissionValueTypeEnum.EntityModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.DatabaseVehicle
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Timeline vehicles",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.TimelineVehicle
                            });
                            break;
                        }

                    case ModuleTypeEnum.Tasks:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Tasks of others",
                                ValueTypeId = PermissionValueTypeEnum.EntityFullModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.OtherTask
                            });

                            break;
                        }
                    case ModuleTypeEnum.TimeRegistration:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Own registered hours",
                                ValueTypeId = PermissionValueTypeEnum.EntityModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.OwnHourTimeReg
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Hours of other crew members",
                                ValueTypeId = PermissionValueTypeEnum.EntityFullModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.CrewMemberHourTimeReg
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Pre-register hours",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.PreRegHourTimeReg
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Clock in from the app",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.ClockAppTimeReg
                            });
                            break;
                        }
                    case ModuleTypeEnum.Maintenance:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Repairs and inspections",
                                ValueTypeId = PermissionValueTypeEnum.EntityModificationPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.RepairInspectionMaintenance
                            });
                            break;
                        }

                    case ModuleTypeEnum.Configuration:
                        {
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Company info",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.CompanyInfo
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "User roles",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.UserRoles
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Time and location",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.TimeAndLocation
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Project types",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.ProjectTypes
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Communication",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.Communication
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Financial",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.Financial
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Invoice moments",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.InvoiceMoments
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Discount groups",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.DiscountGroups
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Payment conditions",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.PaymentConditions
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Vat schemes",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.VatSchemes
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Payment methods",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.PaymentMethods
                            });
                            permissionDirectories.Add(new PermissionDirectoryEntity()
                            {
                                PermissionName = "Ledger",
                                ValueTypeId = PermissionValueTypeEnum.BooleanSelectPermission,
                                ParentPermissionId = element.Id,
                                PermissionField = PermissionFieldEnum.Ledger
                            });
                            break;
                        }

                    default:
                        break;
                }

            }
            await _userRoleStorage.ImportPermission(permissionDirectories);
        }


        public async Task InitPermissionForDefaultRole()
        {
            List<PermissionDirectoryDto> listPermission = await GetPermission();
            List<Role> listRole = (await _userRoleStorage.GetAll()).Items.ToList();

            //fill default
            string defaultRoleId = listRole.Where(x => x.NormalizedName == "DEFAULT USER ROLE").FirstOrDefault()?.Id;
            Role defaultEntity = await _userRoleStorage.GetById(defaultRoleId);
            if (!defaultEntity.RolePermissions.Any())
            {
                FillDefaultRole(defaultRoleId, listPermission);
            }

            //fill freelancer
            string freelancerRoleId = listRole.Where(x => x.NormalizedName == "FREELANCER").FirstOrDefault()?.Id;
            Role freelancerEntity = await _userRoleStorage.GetById(freelancerRoleId);
            if (!freelancerEntity.RolePermissions.Any())
            {
                FillFreelancerRole(freelancerRoleId, listPermission);
            }

            //fill powerUser
            string powerUserRoleId = listRole.Where(x => x.NormalizedName == "POWERUSER").FirstOrDefault()?.Id;
            Role powerUserEntity = await _userRoleStorage.GetById(powerUserRoleId);
            if (!powerUserEntity.RolePermissions.Any())
            {
                FillPowerUserRole(powerUserRoleId, listPermission);
            }

            //fill office
            string officeRoleId = listRole.Where(x => x.NormalizedName == "OFFICE").FirstOrDefault()?.Id;
            Role officeEntity = await _userRoleStorage.GetById(officeRoleId);
            if (!officeEntity.RolePermissions.Any())
            {
                FillOfficeRole(officeRoleId, listPermission);
            }

        }

        public async void FillDefaultRole(string defaultRoleId, List<PermissionDirectoryDto> listPermission)
        {
            List<RolePermissionEntity> permissions = new List<RolePermissionEntity>();
            foreach (PermissionDirectoryDto elPermission in listPermission)
            {
                ModuleTypeEnum? moduleTypeEnum = elPermission.ModuleTypeId;

                switch (moduleTypeEnum)
                {
                    case ModuleTypeEnum.MySchedule:
                    case ModuleTypeEnum.General:
                        {
                            int permissionDirId = elPermission.Id;
                            RolePermissionEntity permission = CreateNewRolePermission(defaultRoleId, permissionDirId, true.ToString());
                            permissions.Add(permission);
                            if (elPermission.Children?.Count > 0)
                            {
                                foreach (PermissionDirectoryDto elChildPermission in elPermission.Children)
                                {
                                    Type type = elChildPermission.PermissionField.GetType();
                                    //var name = System.Enum.GetName(typeof(FieldValueTypeAttribute), elChildPermission.PermissionField);
                                    var name = elChildPermission.PermissionField.ToString();
                                    FieldValueTypeAttribute attr = type.GetField(name).GetCustomAttribute(typeof(FieldValueTypeAttribute)) as FieldValueTypeAttribute;
                                    RolePermissionEntity childApp = CreateNewRolePermission(defaultRoleId, elChildPermission.Id, attr.DefaultValue);
                                    permissions.Add(childApp);
                                }
                            }
                            break;
                        }
                    case ModuleTypeEnum.Dashboard:
                    case ModuleTypeEnum.Configuration:
                    case ModuleTypeEnum.Statistics:
                    case ModuleTypeEnum.Messages:
                    case ModuleTypeEnum.Warehouse:
                    case ModuleTypeEnum.Projects:
                    case ModuleTypeEnum.CrewPlanner:
                    case ModuleTypeEnum.Subhire:
                    case ModuleTypeEnum.Invoices:
                    case ModuleTypeEnum.Equipment:
                    case ModuleTypeEnum.Contacts:
                    case ModuleTypeEnum.CrewMembers:
                    case ModuleTypeEnum.Vehicles:
                    case ModuleTypeEnum.Tasks:
                    case ModuleTypeEnum.TimeRegistration:
                    case ModuleTypeEnum.Maintenance:
                        {
                            int permissionDirId = elPermission.Id;
                            RolePermissionEntity permission = CreateNewRolePermission(defaultRoleId, permissionDirId, false.ToString());
                            permissions.Add(permission);
                            if (elPermission.Children?.Count > 0)
                            {
                                foreach (PermissionDirectoryDto elChildPermission in elPermission.Children)
                                {
                                    Type type = elChildPermission.PermissionField.GetType();
                                    //var name = System.Enum.GetName(typeof(FieldValueTypeAttribute), elChildPermission.PermissionField);
                                    var name = elChildPermission.PermissionField.ToString();
                                    FieldValueTypeAttribute attr = type.GetField(name).GetCustomAttribute(typeof(FieldValueTypeAttribute)) as FieldValueTypeAttribute;
                                    RolePermissionEntity childApp = CreateNewRolePermission(defaultRoleId, elChildPermission.Id, attr.DefaultValue);
                                    permissions.Add(childApp);

                                }
                            }
                            break;
                        }

                }
            }
            if (permissions.Count > 0)
            {
                await _userRoleStorage.ImportRolePermission(permissions);
            }
        }

        public async void FillFreelancerRole(string freelancerRoleId, List<PermissionDirectoryDto> listPermission)
        {
            List<RolePermissionEntity> permissions = new List<RolePermissionEntity>();
            foreach (PermissionDirectoryDto elPermission in listPermission)
            {
                ModuleTypeEnum? moduleTypeEnum = elPermission.ModuleTypeId;

                switch (moduleTypeEnum)
                {
                    case ModuleTypeEnum.MySchedule:
                    case ModuleTypeEnum.General:
                    case ModuleTypeEnum.TimeRegistration:
                        {
                            int permissionDirId = elPermission.Id;
                            RolePermissionEntity permission = CreateNewRolePermission(freelancerRoleId, permissionDirId, true.ToString());
                            permissions.Add(permission);
                            if (elPermission.Children?.Count > 0)
                            {
                                foreach (PermissionDirectoryDto elChildPermission in elPermission.Children)
                                {
                                    Type type = elChildPermission.PermissionField.GetType();
                                    var name = elChildPermission.PermissionField.ToString();
                                    FieldValueTypeAttribute attr = type.GetField(name).GetCustomAttribute(typeof(FieldValueTypeAttribute)) as FieldValueTypeAttribute;
                                    RolePermissionEntity childApp = CreateNewRolePermission(freelancerRoleId, elChildPermission.Id, attr.FreelancertValue);
                                    permissions.Add(childApp);
                                }
                            }
                            break;
                        }
                    case ModuleTypeEnum.Dashboard:
                    case ModuleTypeEnum.Configuration:
                    case ModuleTypeEnum.Statistics:
                    case ModuleTypeEnum.Messages:
                    case ModuleTypeEnum.Warehouse:
                    case ModuleTypeEnum.Projects:
                    case ModuleTypeEnum.CrewPlanner:
                    case ModuleTypeEnum.Subhire:
                    case ModuleTypeEnum.Invoices:
                    case ModuleTypeEnum.Equipment:
                    case ModuleTypeEnum.Contacts:
                    case ModuleTypeEnum.CrewMembers:
                    case ModuleTypeEnum.Vehicles:
                    case ModuleTypeEnum.Tasks:
                    case ModuleTypeEnum.Maintenance:
                        {
                            int permissionDirId = elPermission.Id;
                            RolePermissionEntity permission = CreateNewRolePermission(freelancerRoleId, permissionDirId, false.ToString());
                            permissions.Add(permission);
                            if (elPermission.Children?.Count > 0)
                            {
                                foreach (PermissionDirectoryDto elChildPermission in elPermission.Children)
                                {
                                    Type type = elChildPermission.PermissionField.GetType();
                                    var name = elChildPermission.PermissionField.ToString();
                                    FieldValueTypeAttribute attr = type.GetField(name).GetCustomAttribute(typeof(FieldValueTypeAttribute)) as FieldValueTypeAttribute;
                                    RolePermissionEntity childApp = CreateNewRolePermission(freelancerRoleId, elChildPermission.Id, attr.FreelancertValue);
                                    permissions.Add(childApp);

                                }
                            }
                            break;
                        }
                }
            }
            if (permissions.Count > 0)
            {
                await _userRoleStorage.ImportRolePermission(permissions);
            }
        }

        public async void FillPowerUserRole(string powerUserRoleId, List<PermissionDirectoryDto> listPermission)
        {
            List<RolePermissionEntity> permissions = new List<RolePermissionEntity>();

            foreach (PermissionDirectoryDto elPermission in listPermission)
            {
                //ModuleTypeEnum? moduleTypeEnum = elPermission.ModuleTypeId;

                int permissionDirId = elPermission.Id;
                RolePermissionEntity permission = CreateNewRolePermission(powerUserRoleId, permissionDirId, true.ToString());
                permissions.Add(permission);
                if (elPermission.Children?.Count > 0)
                {
                    foreach (PermissionDirectoryDto elChildPermission in elPermission.Children)
                    {
                        Type type = elChildPermission.PermissionField.GetType();
                        var name = elChildPermission.PermissionField.ToString();
                        FieldValueTypeAttribute attr = type.GetField(name).GetCustomAttribute(typeof(FieldValueTypeAttribute)) as FieldValueTypeAttribute;
                        RolePermissionEntity childApp = CreateNewRolePermission(powerUserRoleId, elChildPermission.Id, attr.PowerUserValue);
                        permissions.Add(childApp);
                    }
                }
            }
            if (permissions.Count > 0)
            {
                await _userRoleStorage.ImportRolePermission(permissions);
            }
        }

        public async void FillOfficeRole(string officeRoleId, List<PermissionDirectoryDto> listPermission)
        {
            List<RolePermissionEntity> permissions = new List<RolePermissionEntity>();

            foreach (PermissionDirectoryDto elPermission in listPermission)
            {
                ModuleTypeEnum? moduleTypeEnum = elPermission.ModuleTypeId;

                switch (moduleTypeEnum)
                {
                    case ModuleTypeEnum.General:
                    case ModuleTypeEnum.MySchedule:
                    case ModuleTypeEnum.Warehouse:
                    case ModuleTypeEnum.Projects:
                    case ModuleTypeEnum.Subhire:
                    case ModuleTypeEnum.Invoices:
                    case ModuleTypeEnum.Equipment:
                    case ModuleTypeEnum.Contacts:
                    case ModuleTypeEnum.CrewMembers:
                    case ModuleTypeEnum.Vehicles:
                    case ModuleTypeEnum.Tasks:
                    case ModuleTypeEnum.TimeRegistration:
                    case ModuleTypeEnum.Maintenance:
                        {
                            int permissionDirId = elPermission.Id;
                            RolePermissionEntity permission = CreateNewRolePermission(officeRoleId, permissionDirId, true.ToString());
                            permissions.Add(permission);
                            if (elPermission.Children?.Count > 0)
                            {
                                foreach (PermissionDirectoryDto elChildPermission in elPermission.Children)
                                {
                                    Type type = elChildPermission.PermissionField.GetType();
                                    var name = elChildPermission.PermissionField.ToString();
                                    FieldValueTypeAttribute attr = type.GetField(name).GetCustomAttribute(typeof(FieldValueTypeAttribute)) as FieldValueTypeAttribute;
                                    RolePermissionEntity childApp = CreateNewRolePermission(officeRoleId, elChildPermission.Id, attr.FreelancertValue);
                                    permissions.Add(childApp);
                                }
                            }
                            break;
                        }
                    case ModuleTypeEnum.Dashboard:
                    case ModuleTypeEnum.CrewPlanner:
                    case ModuleTypeEnum.Configuration:
                    case ModuleTypeEnum.Statistics:
                    case ModuleTypeEnum.Messages:
                        {
                            int permissionDirId = elPermission.Id;
                            RolePermissionEntity permission = CreateNewRolePermission(officeRoleId, permissionDirId, false.ToString());
                            permissions.Add(permission);
                            if (elPermission.Children?.Count > 0)
                            {
                                foreach (PermissionDirectoryDto elChildPermission in elPermission.Children)
                                {
                                    Type type = elChildPermission.PermissionField.GetType();
                                    var name = elChildPermission.PermissionField.ToString();
                                    FieldValueTypeAttribute attr = type.GetField(name).GetCustomAttribute(typeof(FieldValueTypeAttribute)) as FieldValueTypeAttribute;
                                    RolePermissionEntity childApp = CreateNewRolePermission(officeRoleId, elChildPermission.Id, attr.FreelancertValue);
                                    permissions.Add(childApp);

                                }
                            }
                            break;
                        }
                }
            }
            if (permissions.Count > 0)
            {
                await _userRoleStorage.ImportRolePermission(permissions);
            }
        }
        public RolePermissionEntity CreateNewRolePermission(string roleId, int permissionDirectoryId, string value)
        {
            return new RolePermissionEntity()
            {
                RoleId = roleId,
                PermissionDirectoryId = permissionDirectoryId,
                Value = value
            };

        }
        #endregion

        public async Task<bool> DeleteRole(string id)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.UserRoles);
            if (permission.Add)
            {
                if (!_crewMemberStorage.ExistsByRoleId(id))
                {
                    var result = _userRoleStorage.DeleteRole(id);
                    return result;
                }
                else
                {
                    throw new Exception("errors.role_in_use");
                }
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public Task<UserRoleDto> GetUserPermissionsForModule()
        {
            throw new NotImplementedException();
        }
    }
}
