using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Services.CrewMember;
using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Trees;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.Financial.AdditionalCondition;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.General;
using Prorent24.Enum.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Grid
{
    internal class GridService : BaseService, IGridService
    {
        private readonly ICrewMemberService _crewMemberService;
        private readonly IAdditionalConditionStorage _additionalCondition;

        public GridService(IColumnStorage сolumnStorage,
            ICrewMemberService crewMemberService,
            IAdditionalConditionStorage additionalCondition,
            IHttpContextAccessor httpContextAccessor,
            IUserRoleStorage userRoleStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _additionalCondition = additionalCondition;
            _crewMemberService = crewMemberService;
        }

        /// <summary>
        /// Get list column groups with include columns
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<List<ColumnGroup>> GetColumnGroupsByEntity(EntityEnum entity)
        {
            return await System.Threading.Tasks.Task.Run(() =>
               {
                   List<ColumnGroup> columnGroup = entity.GenerateColumnGroupByEntity();
                   return columnGroup;
               });

        }

        #region Columns

        /// <summary>
        /// Add new column
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public async Task AddColumns(EntityEnum entity, params TreeColumn[] columns)
        {
            string userId = GetUserId();

            List<UserColumnEntity> userColumnEntities = await _columnStorage.GetColumnsByEntity(entity, userId);
            userColumnEntities.ForEach(x =>
            {
                x.ShowColumn = false;
            });

            for (int i = 0; i < columns.Length; i++)
            {
                string column = columns[i].Id;

                int index = userColumnEntities.FindIndex(x => x.Column == column);

                if (index > -1)
                {
                    userColumnEntities[index].ShowColumn = true;
                }
                else
                {
                    await this._columnStorage.Create(new UserColumnEntity()
                    {
                        Entity = entity,
                        ShowColumn = true,
                        CreationUserId = GetUserId(),
                        CreationDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now,
                        Column = column,
                        Order = -1
                    });
                }
            }

            await this._columnStorage.Update(userColumnEntities);

        }

        public async Task SetDefauilColumns(EntityEnum entity)
        {
            string userId = GetUserId();

            List<UserColumnEntity> userColumnEntities = await _columnStorage.GetColumnsByEntity(entity, userId);

            if (entity == EntityEnum.VehicleEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "folderName", ShowColumn = true, WidthColumn = 180, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "mark", ShowColumn = true, WidthColumn = 170, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "loadCapacity", ShowColumn = true, WidthColumn = 150, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "bodycapacity", ShowColumn = true, WidthColumn = 125, Order = 4 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "lengthDim", ShowColumn = true, WidthColumn = 80, Order = 5 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "widthDim", ShowColumn = true, WidthColumn = 80, Order = 6 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "heightDim", ShowColumn = true, WidthColumn = 80, Order = 7 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "registersign", ShowColumn = true, WidthColumn = 150, Order = 8 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "numberofpassengerseats", ShowColumn = true, WidthColumn = 150, Order = 9 });

                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "dateTO", ShowColumn = false });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "dayilCosts", ShowColumn = false });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "variableCosts", ShowColumn = false });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "displayInPlanner", ShowColumn = false });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "description", ShowColumn = false });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.VehicleEntity, Column = "insuranceDate", ShowColumn = false });

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "folderName": item.WidthColumn = 180; item.Order = 1; item.ShowColumn = true; break;
                            case "mark": item.WidthColumn = 170; item.Order = 2; item.ShowColumn = true; break;
                            case "loadCapacity": item.WidthColumn = 150; item.Order = 3; item.ShowColumn = true; break;
                            case "bodycapacity": item.WidthColumn = 125; item.Order = 4; item.ShowColumn = true; break;
                            case "lengthDim": item.WidthColumn = 80; item.Order = 5; item.ShowColumn = true; break;
                            case "widthDim": item.WidthColumn = 80; item.Order = 6; item.ShowColumn = true; break;
                            case "heightDim": item.WidthColumn = 80; item.Order = 7; item.ShowColumn = true; break;
                            case "registersign": item.WidthColumn = 150; item.Order = 8; item.ShowColumn = true; break;
                            case "numberofpassengerseats": item.WidthColumn = 150; item.Order = 9; item.ShowColumn = true; break;
                            default: item.ShowColumn = false; break;
                        }
                    }
                }
            }

            if (entity == EntityEnum.CrewMemberEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.CrewMemberEntity, Column = "folderName", ShowColumn = true, WidthColumn = 160, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.CrewMemberEntity, Column = "lastName", ShowColumn = true, WidthColumn = 160, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.CrewMemberEntity, Column = "firstName", ShowColumn = true, WidthColumn = 160, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.CrewMemberEntity, Column = "middleName", ShowColumn = true, WidthColumn = 160, Order = 4 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.CrewMemberEntity, Column = "companyName", ShowColumn = true, WidthColumn = 160, Order = 5 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.CrewMemberEntity, Column = "phone", ShowColumn = true, WidthColumn = 130, Order = 6 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.CrewMemberEntity, Column = "city", ShowColumn = true, WidthColumn = 130, Order = 7 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.CrewMemberEntity, Column = "birthdate", ShowColumn = true, WidthColumn = 100, Order = 8 });

                    string[] ar = {"email","address","country","number","postalCode","description","memberRole","drivingLicense","emergencyContact",
                    "rateByDefault","bankAccountNumber","coCNumber","passportNumber","skype","vAT","passportDate","passportCompany","General","Data","Detail","Address"};

                    foreach (var item in ar)
                    {
                        userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.CrewMemberEntity, Column = item, ShowColumn = false });
                    };

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "folderName": item.WidthColumn = 160; item.Order = 1; item.ShowColumn = true; break;
                            case "lastName": item.WidthColumn = 160; item.Order = 2; item.ShowColumn = true; break;
                            case "firstName": item.WidthColumn = 160; item.Order = 3; item.ShowColumn = true; break;
                            case "middleName": item.WidthColumn = 160; item.Order = 4; item.ShowColumn = true; break;
                            
                            case "companyName": item.WidthColumn = 160; item.Order = 5; item.ShowColumn = true; break;
                            case "phone": item.WidthColumn = 130; item.Order = 6; item.ShowColumn = true; break;
                            case "city": item.WidthColumn = 130; item.Order = 7; item.ShowColumn = true; break;
                            case "birthdate": item.WidthColumn = 100; item.Order = 8; item.ShowColumn = true; break;

                            default: item.ShowColumn = false; break;
                        }
                    }

                    
                }
            }

            if (entity == EntityEnum.ContactEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ContactEntity, Column = "folderName", ShowColumn = true, WidthColumn = 160, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ContactEntity, Column = "lastName", ShowColumn = true, WidthColumn = 160, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ContactEntity, Column = "firstName", ShowColumn = true, WidthColumn = 160, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ContactEntity, Column = "middleName", ShowColumn = true, WidthColumn = 160, Order = 4 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ContactEntity, Column = "companyName", ShowColumn = true, WidthColumn = 160, Order = 5 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ContactEntity, Column = "phone", ShowColumn = true, WidthColumn = 130, Order = 6 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ContactEntity, Column = "city", ShowColumn = true, WidthColumn = 130, Order = 7 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ContactEntity, Column = "email", ShowColumn = true, WidthColumn = 100, Order = 8 });

                    string[] arContactEntity = { "country", "postalCode", "address", "number", "name", "bic", "visitingStreet", "visitingCity", "visitingRegion", "visitingCountry", "website", "notes", 
                        "kpp", "ogrn", "checkingAccount", "correspondentAccount", "bank" };

                    foreach (var item in arContactEntity)
                    {
                        userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ContactEntity, Column = item, ShowColumn = false });
                    };

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "folderName": item.WidthColumn = 160; item.Order = 1; item.ShowColumn = true; break;
                            case "lastName": item.WidthColumn = 160; item.Order = 2; item.ShowColumn = true; break;
                            case "firstName": item.WidthColumn = 160; item.Order = 3; item.ShowColumn = true; break;
                            case "middleName": item.WidthColumn = 160; item.Order = 4; item.ShowColumn = true; break;

                            case "companyName": item.WidthColumn = 160; item.Order = 5; item.ShowColumn = true; break;
                            case "phone": item.WidthColumn = 130; item.Order = 6; item.ShowColumn = true; break;
                            case "city": item.WidthColumn = 130; item.Order = 7; item.ShowColumn = true; break;
                            case "email": item.WidthColumn = 100; item.Order = 8; item.ShowColumn = true; break;

                            default: item.ShowColumn = false; break;
                        }
                    }


                }
            }

            if (entity == EntityEnum.TaskEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TaskEntity, Column = "name", ShowColumn = true, WidthColumn = 250, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TaskEntity, Column = "deadLineGroupName", ShowColumn = true, WidthColumn = 150, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TaskEntity, Column = "completedIn", ShowColumn = true, WidthColumn = 120, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TaskEntity, Column = "executorView", ShowColumn = true, WidthColumn = 250, Order = 4 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TaskEntity, Column = "isPublic", ShowColumn = true, WidthColumn = 115, Order = 5 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TaskEntity, Column = "description", ShowColumn = true, WidthColumn = 270, Order = 6 });

                    string[] arContactEntity = { "crewMemberView", "General" };

                    foreach (var item in arContactEntity)
                    {
                        userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TaskEntity, Column = item, ShowColumn = false });
                    };

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "name": item.WidthColumn = 250; item.Order = 1; item.ShowColumn = true; break;
                            case "deadLineGroupName": item.WidthColumn = 150; item.Order = 2; item.ShowColumn = true; break;
                            case "completedIn": item.WidthColumn = 120; item.Order = 3; item.ShowColumn = true; break;
                            case "executorView": item.WidthColumn = 250; item.Order = 4; item.ShowColumn = true; break;
                            case "isPublic": item.WidthColumn = 115; item.Order = 5; item.ShowColumn = true; break;
                            case "description": item.WidthColumn = 270; item.Order = 6; item.ShowColumn = true; break;

                            default: item.ShowColumn = false; break;
                        }
                    }


                }
            }

            if (entity == EntityEnum.TimeRegistrationEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TimeRegistrationEntity, Column = "fio", ShowColumn = true, WidthColumn = 260, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TimeRegistrationEntity, Column = "timestart", ShowColumn = true, WidthColumn = 120, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TimeRegistrationEntity, Column = "timeend", ShowColumn = true, WidthColumn = 120, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TimeRegistrationEntity, Column = "breakDuration", ShowColumn = true, WidthColumn = 100, Order = 4 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TimeRegistrationEntity, Column = "typeofregistration", ShowColumn = true, WidthColumn = 140, Order = 5 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TimeRegistrationEntity, Column = "comment", ShowColumn = true, WidthColumn = 280, Order = 6 });





                    string[] arContactEntity = { "General", "distance", "breakTimeUnit", "travelDuration", "travelTimeUnit", "lunch", "durationText", "travelDurationText", "breakDurationText " };

                    foreach (var item in arContactEntity)
                    {
                        userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.TimeRegistrationEntity, Column = item, ShowColumn = false });
                    };

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "fio": item.WidthColumn = 260; item.Order = 1; item.ShowColumn = true; break;
                            case "timestart": item.WidthColumn = 120; item.Order = 2; item.ShowColumn = true; break;
                            case "timeend": item.WidthColumn = 120; item.Order = 3; item.ShowColumn = true; break;
                            case "breakDuration": item.WidthColumn = 100; item.Order = 4; item.ShowColumn = true; break;
                            case "typeofregistration": item.WidthColumn = 140; item.Order = 5; item.ShowColumn = true; break;
                            case "comment": item.WidthColumn = 280; item.Order = 6; item.ShowColumn = true; break;

                            default: item.ShowColumn = false; break;
                        }
                    }


                }
            }

            if (entity == EntityEnum.EquipmentEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.EquipmentEntity, Column = "folderName", ShowColumn = true, WidthColumn = 160, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.EquipmentEntity, Column = "code", ShowColumn = true, WidthColumn = 75, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.EquipmentEntity, Column = "name", ShowColumn = true, WidthColumn = 250, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.EquipmentEntity, Column = "setType", ShowColumn = true, WidthColumn = 130, Order = 4 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.EquipmentEntity, Column = "quantity", ShowColumn = true, WidthColumn = 100, Order = 5 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.EquipmentEntity, Column = "availableQuantity", ShowColumn = true, WidthColumn = 100, Order = 6 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.EquipmentEntity, Column = "internalRemark", ShowColumn = true, WidthColumn = 220, Order = 7 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.EquipmentEntity, Column = "rentalPrice", ShowColumn = true, WidthColumn = 100, Order = 8 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.EquipmentEntity, Column = "storage", ShowColumn = true, WidthColumn = 200, Order = 9 });


                    string[] arContactEntity = { "id","view","edit","delete","creationDate","supplyType","stockManagement","quantityMode",
                                                "locationInWarehouse","displayInPlanner","measuringUnit","externalRemark","discountGroup","factorGroup","subhirePrice","newPrice",
                                                "salePrice","purchasePrice","marginPrice","vATClass","lengthDim","widthDim","heightDim","weightDim",
                                                "volume","power","current","lengthUnit","widthUnit","heightUnit","weightUnit","volumeUnit",
                                                "powerUnit","currentUnit","surfaceItem","producerCode","model","brand","colour","maintenanceRequired",
                                                "priceEquipment","transportationType ","kitType","powerConnector" };

                    foreach (var item in arContactEntity)
                    {
                        userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.EquipmentEntity, Column = item, ShowColumn = false });
                    };

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "folderName": item.WidthColumn = 160; item.Order = 1; item.ShowColumn = true; break;
                            case "code": item.WidthColumn = 75; item.Order = 2; item.ShowColumn = true; break;
                            case "name": item.WidthColumn = 250; item.Order = 3; item.ShowColumn = true; break;
                            case "setType": item.WidthColumn = 130; item.Order = 4; item.ShowColumn = true; break;
                            case "quantity": item.WidthColumn = 100; item.Order = 5; item.ShowColumn = true; break;
                            case "availableQuantity": item.WidthColumn = 100; item.Order = 6; item.ShowColumn = true; break;
                            case "internalRemark": item.WidthColumn = 220; item.Order = 7; item.ShowColumn = true; break;
                            case "rentalPrice": item.WidthColumn = 100; item.Order = 8; item.ShowColumn = true; break;
                            case "storage": item.WidthColumn = 200; item.Order = 9; item.ShowColumn = true; break;

                            default: item.ShowColumn = false; break;
                        }
                    }


                }
            }

            if (entity == EntityEnum.SubhireEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireEntity, Column = "name", ShowColumn = true, WidthColumn = 260, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireEntity, Column = "number", ShowColumn = true, WidthColumn = 80, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireEntity, Column = "type", ShowColumn = true, WidthColumn = 130, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireEntity, Column = "status", ShowColumn = true, WidthColumn = 120, Order = 4 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireEntity, Column = "supplierContactCompany", ShowColumn = true, WidthColumn = 150, Order = 5 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireEntity, Column = "equipmentCost", ShowColumn = true, WidthColumn = 110, Order = 6 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireEntity, Column = "additionalCost", ShowColumn = true, WidthColumn = 110, Order = 7 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireEntity, Column = "totalCost", ShowColumn = true, WidthColumn = 110, Order = 8 });

                    string[] arContactEntity = { "projectName", "id", "view", "edit", "delete", "creationDate", "reference",
                                                "remark", "usagePeriodStart", "usagePeriodEnd", "planningPeriodStart", "planningPeriodEnd", "deliveryCollectionStart", "deliveryCollectionEnd" };

                    foreach (var item in arContactEntity)
                    {
                        userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireEntity, Column = item, ShowColumn = false });
                    };

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "name": item.WidthColumn = 260; item.Order = 1; item.ShowColumn = true; break;
                            case "number": item.WidthColumn = 80; item.Order = 2; item.ShowColumn = true; break;
                            case "type": item.WidthColumn = 130; item.Order = 3; item.ShowColumn = true; break;
                            case "status": item.WidthColumn = 120; item.Order = 4; item.ShowColumn = true; break;
                            case "supplierContactCompany": item.WidthColumn = 150; item.Order = 5; item.ShowColumn = true; break;
                            case "equipmentCost": item.WidthColumn = 110; item.Order = 6; item.ShowColumn = true; break;
                            case "additionalCost": item.WidthColumn = 110; item.Order = 7; item.ShowColumn = true; break;
                            case "totalCost": item.WidthColumn = 110; item.Order = 8; item.ShowColumn = true; break;

                            default: item.ShowColumn = false; break;
                        }
                    }


                }
            }

            if (entity == EntityEnum.ProjectEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ProjectEntity, Column = "name", ShowColumn = true, WidthColumn = 215, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ProjectEntity, Column = "number", ShowColumn = true, WidthColumn = 80, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ProjectEntity, Column = "status", ShowColumn = true, WidthColumn = 130, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ProjectEntity, Column = "clientName", ShowColumn = true, WidthColumn = 160, Order = 4 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ProjectEntity, Column = "locationName", ShowColumn = true, WidthColumn = 230, Order = 5 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ProjectEntity, Column = "typeName", ShowColumn = true, WidthColumn = 80, Order = 6 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ProjectEntity, Column = "color", ShowColumn = true, WidthColumn = 50, Order = 7 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ProjectEntity, Column = "startPlanPeriod", ShowColumn = true, WidthColumn = 150, Order = 8 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ProjectEntity, Column = "endPlanPeriod", ShowColumn = true, WidthColumn = 150, Order = 9 });

                    string[] arContactEntity = { "startUsePeriod", "endUsePeriod", "General", "Detail", "isPublic" };

                    foreach (var item in arContactEntity)
                    {
                        userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.ProjectEntity, Column = item, ShowColumn = false });
                    };

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "name": item.WidthColumn = 215; item.Order = 1; item.ShowColumn = true; break;
                            case "number": item.WidthColumn = 80; item.Order = 2; item.ShowColumn = true; break;
                            case "status": item.WidthColumn = 130; item.Order = 3; item.ShowColumn = true; break;
                            case "clientName": item.WidthColumn = 160; item.Order = 4; item.ShowColumn = true; break;
                            case "locationName": item.WidthColumn = 230; item.Order = 5; item.ShowColumn = true; break;
                            case "typeName": item.WidthColumn = 80; item.Order = 6; item.ShowColumn = true; break;
                            case "color": item.WidthColumn = 50; item.Order = 7; item.ShowColumn = true; break;
                            case "startPlanPeriod": item.WidthColumn = 150; item.Order = 8; item.ShowColumn = true; break;
                            case "endPlanPeriod": item.WidthColumn = 150; item.Order = 9; item.ShowColumn = true; break;

                            default: item.ShowColumn = false; break;
                        }
                    }
                }
            }

            if (entity == EntityEnum.InvoiceEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.InvoiceEntity, Column = "clientName", ShowColumn = true, WidthColumn = 200, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.InvoiceEntity, Column = "generatedOn", ShowColumn = true, WidthColumn = 130, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.InvoiceEntity, Column = "dueDate", ShowColumn = true, WidthColumn = 130, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.InvoiceEntity, Column = "date", ShowColumn = true, WidthColumn = 130, Order = 4 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.InvoiceEntity, Column = "state", ShowColumn = true, WidthColumn = 130, Order = 5 });

                    string[] arContactEntity = { "totalPrice", "General", "totalNewPrice" };

                    foreach (var item in arContactEntity)
                    {
                        userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.InvoiceEntity, Column = item, ShowColumn = false });
                    };

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "clientName": item.WidthColumn = 200; item.Order = 1; item.ShowColumn = true; break;
                            case "generatedOn": item.WidthColumn = 130; item.Order = 2; item.ShowColumn = true; break;
                            case "dueDate": item.WidthColumn = 130; item.Order = 3; item.ShowColumn = true; break;
                            case "date": item.WidthColumn = 130; item.Order = 4; item.ShowColumn = true; break;
                            case "state": item.WidthColumn = 130; item.Order = 5; item.ShowColumn = true; break;

                            default: item.ShowColumn = false; break;
                        }
                    }
                }
            }

            if (entity == EntityEnum.RepairEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.RepairEntity, Column = "equipmentName", ShowColumn = true, WidthColumn = 400, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.RepairEntity, Column = "from", ShowColumn = true, WidthColumn = 130, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.RepairEntity, Column = "until", ShowColumn = true, WidthColumn = 130, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.RepairEntity, Column = "remark", ShowColumn = true, WidthColumn = 400, Order = 4 });

                    string[] arContactEntity = { "id", "view", "edit", "delete", "creationDate", "serialNumberName", "usable", "equipmentId", "quantity", "cost" };

                    foreach (var item in arContactEntity)
                    {
                        userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.RepairEntity, Column = item, ShowColumn = false });
                    };

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "equipmentName": item.WidthColumn = 400; item.Order = 1; item.ShowColumn = true; break;
                            case "from": item.WidthColumn = 130; item.Order = 2; item.ShowColumn = true; break;
                            case "until": item.WidthColumn = 130; item.Order = 3; item.ShowColumn = true; break;
                            case "remark": item.WidthColumn = 400; item.Order = 4; item.ShowColumn = true; break;

                            default: item.ShowColumn = false; break;
                        }
                    }
                }
            }

            if (entity == EntityEnum.InspectionEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.InspectionEntity, Column = "status", ShowColumn = true, WidthColumn = 200, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.InspectionEntity, Column = "date", ShowColumn = true, WidthColumn = 130, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.InspectionEntity, Column = "periodicInspectionName", ShowColumn = true, WidthColumn = 250, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.InspectionEntity, Column = "description", ShowColumn = true, WidthColumn = 400, Order = 4 });

                    string[] arContactEntity = { "id", "view" };

                    foreach (var item in arContactEntity)
                    {
                        userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.InspectionEntity, Column = item, ShowColumn = false });
                    };

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "status": item.WidthColumn = 200; item.Order = 1; item.ShowColumn = true; break;
                            case "date": item.WidthColumn = 130; item.Order = 2; item.ShowColumn = true; break;
                            case "periodicInspectionName": item.WidthColumn = 250; item.Order = 3; item.ShowColumn = true; break;
                            case "description": item.WidthColumn = 400; item.Order = 4; item.ShowColumn = true; break;

                            default: item.ShowColumn = false; break;
                        }
                    }
                }
            }

            if (entity == EntityEnum.SubhireShortageEntity)
            {
                if (userColumnEntities.Count == 0)
                {
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireShortageEntity, Column = "projectName", ShowColumn = true, WidthColumn = 170, Order = 1 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireShortageEntity, Column = "equipmentName", ShowColumn = true, WidthColumn = 230, Order = 2 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireShortageEntity, Column = "plannedQuantity", ShowColumn = true, WidthColumn = 110, Order = 3 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireShortageEntity, Column = "ownStockQuantity", ShowColumn = true, WidthColumn = 110, Order = 4 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireShortageEntity, Column = "subhiredQuantity", ShowColumn = true, WidthColumn = 110, Order = 5 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireShortageEntity, Column = "shortageQuantity", ShowColumn = true, WidthColumn = 110, Order = 6 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireShortageEntity, Column = "explanation", ShowColumn = true, WidthColumn = 110, Order = 7 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireShortageEntity, Column = "startUsePeriod", ShowColumn = true, WidthColumn = 140, Order = 8 });
                    userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireShortageEntity, Column = "endUsePeriod", ShowColumn = true, WidthColumn = 140, Order = 9 });

                    string[] arContactEntity = { "id", "view" };

                    foreach (var item in arContactEntity)
                    {
                        userColumnEntities.Add(new UserColumnEntity() { Entity = EntityEnum.SubhireShortageEntity, Column = item, ShowColumn = false });
                    };

                    await this._columnStorage.Create(userColumnEntities);
                }
                else
                {
                    foreach (var item in userColumnEntities)
                    {
                        switch (item.Column)
                        {
                            case "projectName": item.WidthColumn = 170; item.Order = 1; item.ShowColumn = true; break;
                            case "equipmentName": item.WidthColumn = 230; item.Order = 2; item.ShowColumn = true; break;
                            case "plannedQuantity": item.WidthColumn = 110; item.Order = 3; item.ShowColumn = true; break;
                            case "ownStockQuantity": item.WidthColumn = 110; item.Order = 4; item.ShowColumn = true; break;
                            case "subhiredQuantity": item.WidthColumn = 110; item.Order = 5; item.ShowColumn = true; break;
                            case "shortageQuantity": item.WidthColumn = 110; item.Order = 6; item.ShowColumn = true; break;
                            case "explanation": item.WidthColumn = 110; item.Order = 7; item.ShowColumn = true; break;
                            case "startUsePeriod": item.WidthColumn = 140; item.Order = 8; item.ShowColumn = true; break;
                            case "endUsePeriod": item.WidthColumn = 140; item.Order = 9; item.ShowColumn = true; break;

                            default: item.ShowColumn = false; break;
                        }
                    }
                }
            }

            await this._columnStorage.Update(userColumnEntities);
        }

            /// <summary>
            /// Update columns
            /// </summary>
            /// <param name="entity"></param>
            /// <param name="columns"></param>
            /// <returns></returns>
            public async Task UpdateColumns(EntityEnum entity, List<string> columns)
        {
            string userId = GetUserId();

            List<UserColumnEntity> userColumnEntities = await _columnStorage.GetColumnsByEntity(entity, userId);
            userColumnEntities.ForEach(x =>
            {
                x.ShowColumn = false;
            });

            for (int i = 0; i < columns.Count; i++)
            {
                string column = columns[i];

                int index = userColumnEntities.FindIndex(x => x.Column == column);

                if (index > -1)
                {
                    userColumnEntities[index].ShowColumn = true;
                }
                else
                {
                    await this._columnStorage.Create(new UserColumnEntity()
                    {
                        Entity = entity,
                        ShowColumn = true,
                        CreationUserId = GetUserId(),
                        CreationDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now,
                        Column = column,
                        Order = -1
                    });
                }
            }

            await this._columnStorage.Update(userColumnEntities);

        }

        /// <summary>
        /// Change Order Column
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        /// <param name="newIndex"></param>
        /// <param name="columnsReorder"></param>
        /// <returns></returns>
        public async Task ChangeOrderColumn(EntityEnum entity, string column, short newIndex, List<ColumnReorder> columnsReorder)
        {
            column = column.Trim();

            List<UserColumnEntity> columnsFromDb = await this._columnStorage.GetColumnsByEntity(entity, GetUserId());

            foreach (ColumnReorder item in columnsReorder)
            {
                int index = columnsFromDb.FindIndex(x => x.Column == item.Column);

                if (index < 0)
                {
                    columnsFromDb.Add(new UserColumnEntity()
                    {
                        Entity = entity,
                        ShowColumn = !item.Hidden,
                        CreationUserId = GetUserId(),
                        CreationDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now,
                        Column = item.Column,
                        WidthColumn = item.Width,
                        Order = item.Column.Equals(column) ? newIndex : item.Index
                    });
                }
                else
                {
                    columnsFromDb[index].Order = item.Index;
                    columnsFromDb[index].WidthColumn = item.Width;
                    columnsFromDb[index].ShowColumn = !item.Hidden;
                    columnsFromDb[index].LastModifiedDate = DateTime.Now;
                }
            }

            await this._columnStorage.Update(columnsFromDb);
        }

        /// <summary>
        /// Change Width Column
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public async Task ChangeWidthColumn(EntityEnum entity, string column, double width)
        {
            column = column.Trim();

            UserColumnEntity columnEntity = await this._columnStorage.GetColumn(entity, GetUserId(), column);

            if (columnEntity != null && columnEntity.Id > 0)
            {
                columnEntity.WidthColumn = width;

                await this._columnStorage.Update(columnEntity);
            }
            else
            {
                await this._columnStorage.Create(new UserColumnEntity()
                {
                    Entity = entity,
                    ShowColumn = true,
                    CreationUserId = GetUserId(),
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    Column = column,
                    WidthColumn = width,
                    Order = -1
                });
            }
        }

        #endregion
    }
}
