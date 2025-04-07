using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prorent24.Common.Models;
using Prorent24.Entity;
using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Entity.Configuration.Roles;
using Prorent24.Entity.Configuration.Settings;
using Prorent24.Entity.Contact;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.Directory;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.General;
using Prorent24.Entity.Maintenance;
using Prorent24.Entity.Tasks;
using Prorent24.Entity.TimeRegistration;
using Prorent24.Entity.Vehicle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Prorent24.Context.Data
{
    public static class MigrateDataBase
    {
        public static void WriteData(this ModelBuilder modelBuilder, string migartionName = null)
        {
            if (!string.IsNullOrWhiteSpace(migartionName))
            {
                var path = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot\\Backups",
                       string.Concat(migartionName, ".json"));

                Assembly assem = Assembly.GetAssembly(typeof(BaseEntity));
                foreach (var item in TempMigrationData.data)
                {
                    Type currentType = assem.DefinedTypes.FirstOrDefault(x => x.FullName.ToLower() == item.Key.ToLower());
                    if (currentType != null)
                    {
                        try
                        {
                            modelBuilder.SetNewData(item.Key, item.Value);
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                }
            }
        }


        //public static void RecreateDataBase(this Prorent24DbContext context)
        //  {
        //bool updatedMigrations = context.Database.GetPendingMigrations().Any();
        //if (updatedMigrations)
        //{
        //context.Database.CloseConnection();

        //bool deleted = context.Database.EnsureDeleted();

        //if (deleted)
        //{
        //    context.Database.Migrate();
        //}
        //}

        //return false;
        //  }

        private static ModelBuilder SetNewData(this ModelBuilder modelBuilder,string key,object[] data)
        {
            if (key == typeof(User).FullName)
            {
                modelBuilder.Entity<User>().HasData(data);
            }
            else if (key == typeof(EquipmentEntity).FullName)
            {
                modelBuilder.Entity<EquipmentEntity>().HasData(data);
            }
            else if (key == typeof(EquipmentContentEntity).FullName)
            {
                modelBuilder.Entity<EquipmentContentEntity>().HasData(data);
            }
            else if (key == typeof(EquipmentAccessoryEntity).FullName)
            {
                modelBuilder.Entity<EquipmentAccessoryEntity>().HasData(data);
            }
            else if (key == typeof(EquipmentAlternativeEntity).FullName)
            {
                modelBuilder.Entity<EquipmentAlternativeEntity>().HasData(data);
            }
            else if (key == typeof(EquipmentSerialNumberEntity).FullName)
            {
                modelBuilder.Entity<EquipmentSerialNumberEntity>().HasData(data);
            }
            else if (key == typeof(EquipmentSupplierEntity).FullName)
            {
                modelBuilder.Entity<EquipmentSupplierEntity>().HasData(data);
            }
            else if (key == typeof(EquipmentQRCodeEntity).FullName)
            {
                modelBuilder.Entity<EquipmentQRCodeEntity>().HasData(data);
            }
            else if (key == typeof(ContactEntity).FullName)
            {
                modelBuilder.Entity<ContactEntity>().HasData(data);
            }
            else if (key == typeof(ContactPersonEntity).FullName)
            {
                modelBuilder.Entity<ContactPersonEntity>().HasData(data);
            }
            else if (key == typeof(ContactPaymentEntity).FullName)
            {
                modelBuilder.Entity<ContactPaymentEntity>().HasData(data);
            }
            else if (key == typeof(CrewMemberEntity).FullName)
            {
                modelBuilder.Entity<CrewMemberEntity>().HasData(data);
            }
            else if (key == typeof(CrewMemberRateEntity).FullName)
            {
                modelBuilder.Entity<CrewMemberRateEntity>().HasData(data);
            }
            else if (key == typeof(TaskEntity).FullName)
            {
                modelBuilder.Entity<TaskEntity>().HasData(data);
            }
            else if (key == typeof(TaskVisibilityCrewMemberEntity).FullName)
            {
                modelBuilder.Entity<TaskVisibilityCrewMemberEntity>().HasData(data);
            }
            else if (key == typeof(VehicleEntity).FullName)
            {
                modelBuilder.Entity<VehicleEntity>().HasData(data);
            }
            else if (key == typeof(TimeRegistrationEntity).FullName)
            {
                modelBuilder.Entity<TimeRegistrationEntity>().HasData(data);
            }
            else if (key == typeof(TimeRegistrationActivityEntity).FullName)
            {
                modelBuilder.Entity<TimeRegistrationActivityEntity>().HasData(data);
            }
            else if (key == typeof(SystemSettingEntity).FullName)
            {
                modelBuilder.Entity<SystemSettingEntity>().HasData(data);
            }
            else if (key == typeof(ProjectTemplateEntity).FullName)
            {
                modelBuilder.Entity<ProjectTemplateEntity>().HasData(data);
            }
            else if (key == typeof(PeriodicInspectionEntity).FullName)
            {
                modelBuilder.Entity<PeriodicInspectionEntity>().HasData(data);
            }
            else if (key == typeof(ProjectTypeEntity).FullName)
            {
                modelBuilder.Entity<ProjectTypeEntity>().HasData(data);
            }
            else if (key == typeof(ExtraInputFieldEntity).FullName)
            {
                modelBuilder.Entity<ExtraInputFieldEntity>().HasData(data);
            }
            else if (key == typeof(SalutationEntity).FullName)
            {
                modelBuilder.Entity<SalutationEntity>().HasData(data);
            }
            else if (key == typeof(DocumentTemplateEntity).FullName)
            {
                foreach(var item in data)
                {
                    modelBuilder.Entity<DocumentTemplateEntity>().HasData(Convert.ChangeType(item, typeof(DocumentTemplateEntity)));
                }
            }
            else if (key == typeof(LetterheadEntity).FullName)
            {
                modelBuilder.Entity<LetterheadEntity>().HasData(data);
            }
            else if (key == typeof(FactorGroupEntity).FullName)
            {
                modelBuilder.Entity<FactorGroupEntity>().HasData(data);
            }
            else if (key == typeof(FactorGroupOptionEntity).FullName)
            {
                modelBuilder.Entity<FactorGroupOptionEntity>().HasData(data);
            }
            else if (key == typeof(DiscountGroupEntity).FullName)
            {
                modelBuilder.Entity<DiscountGroupEntity>().HasData(data);
            }
            else if (key == typeof(InvoiceMomentEntity).FullName)
            {
                modelBuilder.Entity<InvoiceMomentEntity>().HasData(data);
            }
            else if (key == typeof(PaymentMethodEntity).FullName)
            {
                modelBuilder.Entity<PaymentMethodEntity>().HasData(data);
            }
            else if (key == typeof(PaymentConditionEntity).FullName)
            {
                modelBuilder.Entity<PaymentConditionEntity>().HasData(data);
            }
            else if (key == typeof(VatSchemeEntity).FullName)
            {
                modelBuilder.Entity<VatSchemeEntity>().HasData(data);
            }
            else if (key == typeof(VatClassEntity).FullName)
            {
                modelBuilder.Entity<VatClassEntity>().HasData(data);
            }
            else if (key == typeof(VatClassSchemeRateEntity).FullName)
            {
                modelBuilder.Entity<VatClassSchemeRateEntity>().HasData(data);
            }
            else if (key == typeof(LedgerEntity).FullName)
            {
                modelBuilder.Entity<LedgerEntity>().HasData(data);
            }
            else if (key == typeof(AdditionalConditionEntity).FullName)
            {
                modelBuilder.Entity<AdditionalConditionEntity>().HasData(data);
            }
            else if (key == typeof(DirectoryEntity).FullName)
            {
                modelBuilder.Entity<DirectoryEntity>().HasData(data);
            }
            else if (key == typeof(DirectoryLocEntity).FullName)
            {
                modelBuilder.Entity<DirectoryLocEntity>().HasData(data);
            }
            else if (key == typeof(FolderEntity).FullName)
            {
                modelBuilder.Entity<FolderEntity>().HasData(data);
            }
            else if (key == typeof(TagEntity).FullName)
            {
                modelBuilder.Entity<TagEntity>().HasData(data);
            }
            else if (key == typeof(NoteEntity).FullName)
            {
                modelBuilder.Entity<NoteEntity>().HasData(data);
            }
            else if (key == typeof(FileEntity).FullName)
            {
                modelBuilder.Entity<FileEntity>().HasData(data);
            }
            else if (key == typeof(SavedFilterEntity).FullName)
            {
                modelBuilder.Entity<SavedFilterEntity>().HasData(data);
            }
            else if (key == typeof(PresetEntity).FullName)
            {
                modelBuilder.Entity<PresetEntity>().HasData(data);
            }
            else if (key == typeof(RolePermissionEntity).FullName)
            {
                modelBuilder.Entity<RolePermissionEntity>().HasData(data);
            }
            else if (key == typeof(PermissionDirectoryEntity).FullName)
            {
                modelBuilder.Entity<PermissionDirectoryEntity>().HasData(data);
            }
            else if (key == typeof(InspectionEntity).FullName)
            {
                modelBuilder.Entity<InspectionEntity>().HasData(data);
            }
            else if (key == typeof(InspectionSerialNumberEntity).FullName)
            {
                modelBuilder.Entity<InspectionSerialNumberEntity>().HasData(data);
            }
            else if (key == typeof(RepairEntity).FullName)
            {
                modelBuilder.Entity<RepairEntity>().HasData(data);
            }
            else if (key == typeof(ModuleEntity).FullName)
            {
                modelBuilder.Entity<ModuleEntity>().HasData(data);
            }

            return modelBuilder;
        }

        //private static void SetData(this Prorent24DbContext context)
        //{
        //    foreach (var item in data)
        //    {

        //        context.Users.AddRange(item.Value as List<User>);
        //        context.SaveChanges();
        //    }
        //}
    }
}
