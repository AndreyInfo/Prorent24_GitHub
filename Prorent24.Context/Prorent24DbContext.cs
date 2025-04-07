using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Prorent24.Context.Data;
using Prorent24.Context.Logger;
using Prorent24.Context.SeedDataExtention;
using Prorent24.Entity;
using Prorent24.Entity.Configuration;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Entity.Configuration.Roles;
using Prorent24.Entity.Configuration.Settings;
using Prorent24.Entity.Contact;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.CrewPlanner;
using Prorent24.Entity.Directory;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.General;
using Prorent24.Entity.Identity;
using Prorent24.Entity.Invoice;
using Prorent24.Entity.Maintenance;
using Prorent24.Entity.Notification;
using Prorent24.Entity.Project;
using Prorent24.Entity.Subhire;
using Prorent24.Entity.Tasks;
using Prorent24.Entity.TimeRegistration;
using Prorent24.Entity.Vehicle;
using System;
using System.Linq;

namespace Prorent24.Context
{
    public class Prorent24DbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRoleEntity,
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {

        public Prorent24DbContext(DbContextOptions<Prorent24DbContext> options) : base(options)
        {
            //this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(DBLogger.DbCommandConsoleLoggerFactory).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<BaseEntity>()
            //   .Property(b => b.CreationDate)
            //   .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<DirectoryLocEntity>()
              .HasKey(c => new { c.DirectoryId, c.Lang });

            modelBuilder.Entity<DirectoryEntity>()
               .Property(b => b.IsActive)
               .HasDefaultValue(true)
               .ValueGeneratedNever();

            modelBuilder.Entity<CrewMemberEntity>()
             .HasMany(t => t.Notes);

            modelBuilder.Entity<CrewMemberEntity>()
            .HasMany(t => t.Tags);

            modelBuilder.Entity<CrewMemberEntity>()
              .HasMany(t => t.Tasks);

            modelBuilder.Entity<CrewMemberEntity>()
              .HasMany(t => t.Files);

            modelBuilder.Entity<TaskEntity>()
             .HasMany(t => t.Tags);

            modelBuilder.Entity<TaskEntity>()
                .HasMany(t => t.Notes);

            modelBuilder.Entity<TaskEntity>()
                .HasMany(t => t.Files);

            modelBuilder.Entity<VehicleEntity>()
                .HasMany(t => t.Tags);

            modelBuilder.Entity<VehicleEntity>()
                .HasMany(t => t.Tasks);

            modelBuilder.Entity<VehicleEntity>()
                .HasMany(t => t.Notes);

            modelBuilder.Entity<VehicleEntity>()
                .HasMany(t => t.Files);

            modelBuilder.Entity<EquipmentEntity>()
                .HasMany(g => g.EquipmentContents)
                .WithOne(q => q.Equipment)
                .HasForeignKey(x => x.EquipmentId);

            modelBuilder.Entity<EquipmentEntity>()
               .HasMany(g => g.EquipmentAlternatives)
               .WithOne(q => q.Equipment)
               .HasForeignKey(x => x.EquipmentId);

            modelBuilder.Entity<EquipmentEntity>()
               .HasMany(g => g.EquipmentAccessories)
               .WithOne(q => q.Equipment)
               .HasForeignKey(x => x.EquipmentId);

            modelBuilder.Entity<User>()
               .HasMany(p => p.Roles)
               .WithOne()
               .HasForeignKey(p => p.UserId)
               .HasPrincipalKey(p => p.Id);

            modelBuilder.Entity<TaskVisibilityCrewMemberEntity>()
                .HasKey(c => new { c.TaskId, c.CrewMemberId });

            modelBuilder.Entity<FolderEntity>()
                .HasMany(page => page.Childs);

            modelBuilder.Entity<ProjectEquipmentEntity>()
             .HasMany(page => page.Children);

            modelBuilder.Entity<ProjectEquipmentMovementEntity>()
              .HasMany(page => page.KitCaseEquipments);

            modelBuilder.Entity<User>() //Use your application user class here
                .ToTable("AspNetUsers");

        }

        public DbSet<ModuleEntity> Modules { get; set; }

        #region IDENTITY

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        #endregion

        #region PROJECTS

        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<ProjectTimeEntity> ProjectTimes { get; set; }
        public DbSet<ProjectEquipmentEntity> ProjectEquipments { get; set; }
        public DbSet<ProjectEquipmentGroupEntity> ProjectEquipmentGroups { get; set; }
        public DbSet<ProjectFunctionEntity> ProjectFunctions { get; set; }
        public DbSet<ProjectFunctionGroupEntity> ProjectFunctionGroups { get; set; }
        public DbSet<ProjectAdditionalCostEntity> ProjectAdditionalCosts { get; set; }
        public DbSet<ProjectPlanningEntity> ProjectPlannings { get; set; }
        public DbSet<ProjectFinancialEntity> ProjectFinancials { get; set; }
        public DbSet<ProjectFinancialCategoryEntity> ProjectFinancialCategories { get; set; }
        public DbSet<ProjectEquipmentMovementEntity> ProjectEquipmentMovements { get; set; }
        public DbSet<EquipmentReservedEntity> EquipmentReserved { get; set; }
        

        #endregion

        #region INVOICE

        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<InvoiceLineEntity> InvoiceLines { get; set; }
        public DbSet<InvoiceRequestEntity> InvoiceRequests { get; set; }
        public DbSet<InvoiceExcludedEntity> InvoiceExcludeds { get; set; }

        #endregion

        #region EQUIPMENTS
        public DbSet<EquipmentEntity> Equipments { get; set; }
        public DbSet<EquipmentContentEntity> EquipmentContents { get; set; }
        public DbSet<EquipmentAccessoryEntity> EquipmentAccessories { get; set; }
        public DbSet<EquipmentAlternativeEntity> EquipmentAlternatives { get; set; }
        public DbSet<EquipmentSerialNumberEntity> EquipmentSerialNumbers { get; set; }
        public DbSet<EquipmentSupplierEntity> EquipmentSuppliers { get; set; }
        public DbSet<EquipmentQRCodeEntity> EquipmentQRCodes { get; set; }
        public DbSet<EquipmentPeriodicInspectionEntity> EquipmentPeriodicInspections { get; set; }
        public DbSet<EquipmentWebShopEntity> EquipmentWebShop { get; set; }
        public DbSet<EquipmentStockEntity> GetEquipmentStocks { get; set; }
        #endregion

        #region CONTACTS
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<ContactPersonEntity> ContactPersons { get; set; }
        public DbSet<ContactPaymentEntity> ContactPayment { get; set; }
        public DbSet<ContactElectronicInvoiceEntity> ContactElectronicInvoice { get; set; }
        #endregion

        #region CREW MEMBERS
        public DbSet<CrewMemberEntity> CrewMembers { get; set; }
        public DbSet<CrewMemberRateEntity> CrewMemberRates { get; set; }
        #endregion

        #region TASKS

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskVisibilityCrewMemberEntity> TaskVisibilityCrewMembers { get; set; }

        #endregion

        #region VEHICLES
        public DbSet<VehicleEntity> Vehicles { get; set; }
        #endregion

        #region TIME REGISTRATION

        public DbSet<TimeRegistrationEntity> TimeRegistrations { get; set; }
        public DbSet<TimeRegistrationActivityEntity> TimeRegistrationActivities { get; set; }

        #endregion

        #region MAINTENANCE

        public DbSet<InspectionEntity> Inspections { get; set; }
        public DbSet<InspectionSerialNumberEntity> InspectionSerialNumbers { get; set; }
        public DbSet<RepairEntity> Repairs { get; set; }
        public DbSet<InspectEqupmentRequestEntity> InspectEqupmentRequests { get; set; }

        #endregion

        #region CONFIGURATIN

        public DbSet<SystemSettingEntity> SystemSettings { get; set; }

        #region Settings
        public DbSet<ProjectTemplateEntity> ProjectTemplates { get; set; }
        public DbSet<PeriodicInspectionEntity> PeriodicInspections { get; set; }
        public DbSet<ProjectTypeEntity> ProjectTypes { get; set; }
        public DbSet<ExtraInputFieldEntity> ExtraInputFields { get; set; }
        #endregion

        #region CustomerCommunication
        public DbSet<SalutationEntity> Salutations { get; set; }
        public DbSet<DocumentTemplateEntity> DocumentTemplates { get; set; }
        // public DbSet<DocumentStructureEntity> DocumentStructures { get; set; }
        public DbSet<DocumentTemplateBlockEntity> DocumentStructureBlocks { get; set; }
        public DbSet<LetterheadEntity> Letterheads { get; set; }

        #endregion

        #region Financial

        public DbSet<FactorGroupEntity> FactorGroups { get; set; }

        public DbSet<FactorGroupOptionEntity> FactorGroupOptions { get; set; }

        public DbSet<DiscountGroupEntity> DiscountGroups { get; set; }

        public DbSet<InvoiceMomentEntity> InvoiceMoments { get; set; }

        public DbSet<PaymentMethodEntity> PaymentMethods { get; set; }

        public DbSet<PaymentConditionEntity> PaymentConditions { get; set; }

        public DbSet<VatSchemeEntity> VatSchemes { get; set; }

        public DbSet<VatClassEntity> VatClasses { get; set; }

        public DbSet<VatClassSchemeRateEntity> VatClassSchemeRates { get; set; }
        public DbSet<LedgerEntity> Ledgers { get; set; }

        public DbSet<AdditionalConditionEntity> AdditionalConditions { get; set; }

        #endregion

        #endregion

        #region GENERAL 

        public DbSet<DirectoryEntity> Directories { get; set; }
        public DbSet<DirectoryLocEntity> DirectoryLocs { get; set; }
        public DbSet<FolderEntity> Folders { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<NoteEntity> Notes { get; set; }
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<DocumentEntity> Documents { get; set; }
        public DbSet<SavedFilterEntity> SavedFilters { get; set; }
        public DbSet<PresetEntity> Presets { get; set; }

        public DbSet<RolePermissionEntity> RolePermissions { get; set; }
        public DbSet<PermissionDirectoryEntity> PermissionDirectories { get; set; }

        public DbSet<UserColumnEntity> UserColumns { get; set; }
        // public DbSet<ColumnEntity> ExtraColumns { get; set; }


        #endregion

        #region SUBHIRE
        public DbSet<SubhireEntity> Subhires { get; set; }
        public DbSet<SubhireEquipmentGroupEntity> SubhireEquipmentGroups { get; set; }
        public DbSet<SubhireEquipmentEntity> SubhireEquipments { get; set; }

        public DbSet<SubhireProjectEntity> SubhireProjects { get; set; }
        #endregion

        #region CREWPLANNER
        public DbSet<CrewPlannerEntity> CrewPlanners { get; set; }
        #endregion

        public DbSet<NotificationEntity> Notifications { get; set; }

        #region SYSTEM

        public DbSet<MigarionDataBase> MigrationDataBase { get; set; }

        #endregion

    }
}
