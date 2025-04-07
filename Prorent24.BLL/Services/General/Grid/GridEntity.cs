using Prorent24.BLL.Models.Configuration;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Models.Subhire;
using Prorent24.BLL.Models.System;
using Prorent24.BLL.Models.Tasks;
using Prorent24.BLL.Models.TimeRegistration;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.Common.Attributes;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Trees;
using Prorent24.Entity;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Entity.General;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Prorent24.BLL.Services.General.Grid
{
    public static class GridEntity
    {
        public static List<Column> GetColumnsByEntityEnum(this EntityEnum entity)
        {
            List<Column> columns = new List<Column>();

            Type entityType = GetDtoByEntityEnum(entity);

            if (entityType != null)
            {
                PropertyInfo[] properties = entityType.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    IncludeToGrid attribute = property.GetCustomAttribute(typeof(IncludeToGrid)) as IncludeToGrid;
                    if (attribute != null && attribute.Order > -1)
                    {
                        columns.Add(new Column()
                        {
                            Key = property.Name,
                            IsDisplayName = attribute.IsDisplay,
                            Order = attribute.Order
                        });
                    }
                }
            }

            return columns;
        }
        public static List<ColumnGroup> GenerateColumnGroupByEntity(this EntityEnum entity)
        {
            List<ColumnGroup> columnGroups = new List<ColumnGroup>();

            switch (entity)
            {
                case EntityEnum.AdditionalConditionEntity:
                    {
                        columnGroups.AddColumnGroup<AdditionalConditionDto>(ColumnGroupEnum.General);
                        columnGroups.AddColumnGroup<SystemDto>(ColumnGroupEnum.System);
                        break;
                    }
                case EntityEnum.ContactEntity:
                    {
                        columnGroups.AddColumnGroup<ContactDto>(ColumnGroupEnum.General);
                        columnGroups.AddColumnGroup<ContactDto>(ColumnGroupEnum.Address);
                        columnGroups.AddColumnGroup<ContactDto>(ColumnGroupEnum.Requisites);
                        break;
                    }
                case EntityEnum.CrewMemberEntity:
                    {
                        columnGroups.AddColumnGroupAttributed<CrewMemberDto>(ColumnGroupEnum.General);
                        columnGroups.AddColumnGroupAttributed<CrewMemberDto>(ColumnGroupEnum.PerconalData);
                        columnGroups.AddColumnGroupAttributed<CrewMemberDto>(ColumnGroupEnum.Address);
                        columnGroups.AddColumnGroupAttributed<CrewMemberDto>(ColumnGroupEnum.Data);

                        //columnGroups.AddColumnGroup<CrewMemberDto>(ColumnGroupEnum.General);
                        //columnGroups.AddColumnGroup<SystemDto>(ColumnGroupEnum.System);

                        //Type type = typeof(CrewMemberDto);

                        //var columnGroupsList = type.GetProperties().Select(x => (IncludeToGrid)x.GetCustomAttribute(typeof(IncludeToGrid))).Where(x => x != null).Select(x => x.ColumnGroup).Where(x => x != null).Distinct();
                        //foreach (var group in columnGroupsList)
                        //{
                        //    ColumnGroupEnum _group = (ColumnGroupEnum)System.Enum.Parse(typeof(ColumnGroupEnum), group.ToString());
                           
                        //}

                        break;
                    }
                case EntityEnum.UserEntity:
                    {
                        columnGroups.AddColumnGroup<CrewMemberDto>(ColumnGroupEnum.General);
                        columnGroups.AddColumnGroup<SystemDto>(ColumnGroupEnum.System);
                        break;
                    }
            }

            return columnGroups;
        }

        public static List<ColumnEntity> CreateExtraColumns<Model>(this List<ColumnEntity> extraColumns, EntityEnum entity, List<TreeColumn> treeColumns)
        {
            Type currentType = typeof(Model);

            foreach (TreeColumn column in treeColumns)
            {
                //extraColumns.Add(new ExtraColumnEntity()
                //{
                //    Entity = entity,
                //    ColumnId = 
                //    ColumnProperty = JsonConvert.SerializeObject(new ColumnProperty()
                //    {
                //        ColumnId = column.Id,
                //        ModelPath = currentType.AssemblyQualifiedName,
                //        ParentModelPath = column.ParentId != "0" ? currentType.GetProperty(column.ParentId).PropertyType.AssemblyQualifiedName : null
                //    })
                //});
            }

            //  Assembly assembly = Assembly.LoadFile()

            return extraColumns;
        }

        public static Type GetEntityByEntityEnum(this EntityEnum entity)
        {
            switch (entity)
            {
                case EntityEnum.AdditionalConditionEntity:
                    {
                        return typeof(AdditionalConditionEntity);
                    }
                case EntityEnum.SalutationEntity:
                    {
                        return typeof(SalutationEntity);
                    }
                case EntityEnum.UserEntity:
                    {
                        return typeof(User);
                    }
            }

            return null;
        }

        public static Type GetDtoByEntityEnum(this EntityEnum entity)
        {
            switch (entity)
            {
                #region Configuration

                case EntityEnum.UserRoleEntity:
                    {
                        return typeof(UserRoleDto);
                    }
                case EntityEnum.DocumentTemplateEntity:
                    {
                        return typeof(DocumentTemplateDto);
                    }
                case EntityEnum.LetterheadEntity:
                    {
                        return typeof(LetterheadDto);
                    }
                case EntityEnum.SalutationEntity:
                    {
                        return typeof(SalutationDto);
                    }
                case EntityEnum.AdditionalConditionEntity:
                    {
                        return typeof(AdditionalConditionDto);
                    }
                case EntityEnum.DiscountGroupEntity:
                    {
                        return typeof(DiscountGroupDto);
                    }
                case EntityEnum.FactorGroupEntity:
                    {
                        return typeof(FactorGroupDto);
                    }
                case EntityEnum.FactorGroupOptionEntity:
                    {
                        return typeof(FactorGroupOptionDto);
                    }
                case EntityEnum.InvoiceMomentEntity:
                    {
                        return typeof(InvoiceMomentDto);
                    }
                case EntityEnum.LedgerEntity:
                    {
                        return typeof(LedgerDto);
                    }
                case EntityEnum.PaymentConditionEntity:
                    {
                        return typeof(PaymentConditionDto);
                    }
                case EntityEnum.PaymentMethodEntity:
                    {
                        return typeof(PaymentMethodDto);
                    }
                case EntityEnum.VatClassEntity:
                    {
                        return typeof(VatClassDto);
                    }
                case EntityEnum.VatSchemeEntity:
                    {
                        return typeof(VatSchemeDto);
                    }
                case EntityEnum.PeriodicInspectionEntity:
                    {
                        return typeof(PeriodicInspectionDto);
                    }

                #endregion

                #region Contact

                case EntityEnum.ContactEntity:
                    {
                        return typeof(ContactDto);
                    }
                case EntityEnum.ContactPersonEntity:
                    {
                        return typeof(ContactPersonDto);
                    }

                #endregion

                #region Crew Member

                case EntityEnum.CrewMemberEntity:
                    {
                        return typeof(CrewMemberDto);
                    }
                case EntityEnum.ProjectPlanningCrewmember:
                    {
                        return typeof(CrewMemberForProjectPlanningDto);
                    }
                case EntityEnum.CrewMemberRateEntity:
                    {
                        return typeof(CrewMemberRateDto);
                    }

                #endregion

                #region Equipment

                case EntityEnum.EquipmentAccessoryEntity:
                    {
                        return typeof(EquipmentAccessoryDto);
                    }
                case EntityEnum.EquipmentAlternativeEntity:
                    {
                        return typeof(EquipmentAlternativeDto);
                    }
                case EntityEnum.EquipmentContentEntity:
                    {
                        return typeof(EquipmentContentDto);
                    }
                case EntityEnum.EquipmentEntity:
                    {
                        return typeof(EquipmentDto);
                    }
                case EntityEnum.EquipmentPeriodicInspectionEntity:
                    {
                        return typeof(EquipmentPeriodicInspectionDto);
                    }
                case EntityEnum.EquipmentQRCodeEntity:
                    {
                        return typeof(EquipmentQrCodeDto);
                    }
                case EntityEnum.EquipmentSerialNumberEntity:
                    {
                        return typeof(EquipmentSerialNumberDto);
                    }
                case EntityEnum.EquipmentStockEntity:
                    {
                        return typeof(EquipmentStockDto);
                    }
                case EntityEnum.EquipmentSupplierEntity:
                    {
                        return typeof(EquipmentSupplierDto);
                    }

                #endregion

                #region Invoice

                case EntityEnum.InvoiceEntity:
                    {
                        return typeof(InvoiceDto);
                    }

                #endregion

                #region Inspection

                case EntityEnum.InspectionEntity:
                    {
                        return typeof(InspectionDto);
                    }
                case EntityEnum.RepairEntity:
                    {
                        return typeof(RepairDto);
                    }

                #endregion

                #region Project

                case EntityEnum.ProjectFunctionEntity:
                    {
                        return typeof(ProjectFunctionDto);
                    }
                case EntityEnum.ProjectAdditionalCostEntity:
                    {
                        return typeof(ProjectAdditionalCostDto);
                    }
                case EntityEnum.ProjectEntity:
                    {
                        return typeof(ProjectDto);
                    }
                case EntityEnum.ProjectEquipmentEntity:
                    {
                        return typeof(ProjectEquipmentDto);
                    }
                case EntityEnum.ProjectPlanningEntity:
                    {
                        return typeof(ProjectPlanningDto);
                    }

                #endregion

                #region Subhire

                case EntityEnum.SubhireEntity:
                    {
                        return typeof(SubhireDto);
                    }
                case EntityEnum.SubhireEquipmentEntity:
                    {
                        return typeof(SubhireEquipmentDto);
                    }
                case EntityEnum.SubhireShortageEntity:
                    {
                        return typeof(SubhireShortageDto);
                    }

                #endregion

                #region Task

                case EntityEnum.TaskEntity:
                    {
                        return typeof(TaskDto);
                    }

                #endregion

                #region TimeRegistration

                case EntityEnum.TimeRegistrationEntity:
                    {
                        return typeof(TimeRegistrationDto);
                    }

                #endregion

                #region TimeRegistration

                case EntityEnum.VehicleEntity:
                    {
                        return typeof(VehicleDto);
                    }

                    #endregion

            }

            return null;
        }

        private static void AddColumnGroup<Model>(this List<ColumnGroup> columnGroups, ColumnGroupEnum group)
        {
            ColumnGroup columnGroup = new ColumnGroup();

            columnGroup.Key = (int)group;
            columnGroup.Name = group.ToString();
            columnGroup.Columns.AddColumns<Model>();
            columnGroups.Add(columnGroup);
        }

        private static void AddColumnGroupAttributed<Model>(this List<ColumnGroup> columnGroups, ColumnGroupEnum group)
        {
            ColumnGroup columnGroup = new ColumnGroup();

            columnGroup.Key = (int)group;
            columnGroup.Name = group.ToString();
            columnGroup.Columns.AddColumnsByGroup<Model>(group.ToString());
            columnGroups.Add(columnGroup);
        }
    }
}
