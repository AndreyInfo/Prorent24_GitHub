using Microsoft.Extensions.DependencyInjection;
using Prorent24.DAL.Storages.AspUser;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.CustomerCommunication.DocumentTemplate;
using Prorent24.DAL.Storages.Configuration.CustomerCommunication.Letterhead;
using Prorent24.DAL.Storages.Configuration.CustomerCommunication.Salutation;
using Prorent24.DAL.Storages.Configuration.Directory;
using Prorent24.DAL.Storages.Configuration.Financial.AdditionalCondition;
using Prorent24.DAL.Storages.Configuration.Financial.DiscountGroup;
using Prorent24.DAL.Storages.Configuration.Financial.FactorGroup;
using Prorent24.DAL.Storages.Configuration.Financial.InvoiceMoment;
using Prorent24.DAL.Storages.Configuration.Financial.Ledger;
using Prorent24.DAL.Storages.Configuration.Financial.Payment;
using Prorent24.DAL.Storages.Configuration.Financial.Vat;
using Prorent24.DAL.Storages.Configuration.Settings.PeriodicInspection;
using Prorent24.DAL.Storages.Configuration.Settings.ProjectType;
using Prorent24.DAL.Storages.Configuration.SystemSettings;
using Prorent24.DAL.Storages.Contact;
using Prorent24.DAL.Storages.CrewMember;
using Prorent24.DAL.Storages.CrewPlanner;
using Prorent24.DAL.Storages.Equipment;
using Prorent24.DAL.Storages.Equipment.EquipmentReserved;
using Prorent24.DAL.Storages.General.Address;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.General.Document;
using Prorent24.DAL.Storages.General.Filter;
using Prorent24.DAL.Storages.General.Folder;
using Prorent24.DAL.Storages.General.Module;
using Prorent24.DAL.Storages.General.Note;
using Prorent24.DAL.Storages.General.Preset;
using Prorent24.DAL.Storages.General.Tag;
using Prorent24.DAL.Storages.Invoice;
using Prorent24.DAL.Storages.Maintenance.Inspection;
using Prorent24.DAL.Storages.Maintenance.Repair;
using Prorent24.DAL.Storages.Notification;
using Prorent24.DAL.Storages.Project;
using Prorent24.DAL.Storages.Project.Movement;
using Prorent24.DAL.Storages.Subhire;
using Prorent24.DAL.Storages.Tasks;
using Prorent24.DAL.Storages.TimeRegistration;
using Prorent24.DAL.Storages.Vehicle;
using Prorent24.DAL.Storages.WorkPanelStorage;

namespace Prorent24.DAL
{
    public static class DependencyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection services)
        {
            #region INVOICE 

            services.AddTransient<IInvoiceStorage, InvoiceStorage>();
            services.AddTransient<IInvoiceLineStorage, InvoiceLineStorage>();
            services.AddTransient<IInvoiceExcludedStorage, InvoiceExcludedStorage>();

            #endregion

            #region EQUIPMENT

            services.AddTransient<IEquipmentStorage, EquipmentStorage>();
            services.AddTransient<IEquipmentContentStorage, EquipmentContentStorage>();
            services.AddTransient<IEquipmentAccessoryStorage, EquipmentAccessoryStorage>();
            services.AddTransient<IEquipmentAlternativeStorage, EquipmentAlternativeStorage>();
            services.AddTransient<IEquipmentSupplierStorage, EquipmentSupplierStorage>();
            services.AddTransient<IEquipmentSerialNumberStorage, EquipmentSerialNumberStorage>();
            services.AddTransient<IEquipmentQRCodeStorage, EquipmentQRCodeStorage>();
            services.AddTransient<IEquipmentPeriodicInspectionStorage, EquipmentPeriodicInspectionStorage>();
            services.AddTransient<IEquipmentWebShopStorage, EquipmentWebShopStorage>();
            services.AddTransient<IEquipmentStockStorage, EquipmentStockStorage>(); 
            services.AddTransient<IEquipmentReservedStorage, EquipmentReservedStorage>();

            #endregion

            #region CONTACT

            services.AddTransient<IContactStorage, ContactStorage>();
            services.AddTransient<IContactPersonStorage, ContactPersonStorage>();
            services.AddTransient<IContactPaymentStorage, ContactPaymentStorage>();
            services.AddTransient<IContactElectronicInvoiceStorage, ContactElectronicInvoiceStorage>();
            services.AddTransient<IContactCrewMemberStorage, ContactCrewMemberStorage>();

            #endregion

            #region TASKS

            services.AddTransient<ITaskStorage, TaskStorage>();
            services.AddTransient<ITaskCrewMemberStorage, TaskCrewMemberStorage>();
            services.AddTransient<ITaskExecutorStorage, TaskExecutorStorage>();

            #endregion

            #region TIME REGISTRATION

            services.AddTransient<ITimeRegistrationStorage, TimeRegistrationStorage>();

            #endregion

            #region USER
            services.AddTransient<IUserStorage, UserStorage>();
            #endregion
            
            #region CREW MEMBER

            services.AddTransient<ICrewMemberStorage, CrewMemberStorage>();
            services.AddTransient<ICrewMemberRateStorage, CrewMemberRateStorage>(); 

            #endregion

            #region VEHICLE

            services.AddTransient<IVehicleStorage, VehicleStorage>();
            services.AddTransient<IVehicleCrewMemberStorage, VehicleCrewMemberStorage>();

            #endregion

            #region MAINTENANCE

            services.AddTransient<IInspectionStorage, InspectionStorage>();
            services.AddTransient<IInspectionSerialNumberStorage, InspectionSerialNumberStorage>();
            services.AddTransient<IRepairStorage, RepairStorage>();

            #endregion

            #region CONFIGURATION

            #region Directory

            services.AddTransient<IDirectoryStorage, DirectoryStorage>();

            #endregion

            #region CustomerCommunication

            services.AddTransient<IDocumentTemplateStorage, DocumentTemplateStorage>();
            services.AddTransient<IDocumentTemplateBlockStorage, DocumentTemplateBlockStorage>();
            services.AddTransient<ISalutationStorage, SalutationStorage>();
            services.AddTransient<ILetterheadStorage, LetterheadStorage>();

            #endregion

            services.AddTransient<ISystemSettingStorage, SystemSettingStorage>();

            #region Financial

            services.AddTransient<IAdditionalConditionStorage, AdditionalConditionStorage>();
            services.AddTransient<IPaymentMethodStorage, PaymentMethodStorage>();
            services.AddTransient<IPaymentConditionStorage, PaymentConditionStorage>();
            services.AddTransient<IVatSchemeStorage, VatSchemeStorage>();
            services.AddTransient<IVatClassStorage, VatClassStorage>();
            services.AddTransient<ILedgerStorage, LedgerStorage>();
            services.AddTransient<IDiscountGroupStorage, DiscountGroupStorage>();
            services.AddTransient<IFactorGroupStorage, FactorGroupStorage>();
            services.AddTransient<IInvoiceMomentStorage, InvoiceMomentStorage>();
            services.AddTransient<IVatClassSchemeRateStorage, VatClassSchemeRateStorage>();
            services.AddTransient<IFactorGroupOptionStorage, FactorGroupOptionStorage>();

            #endregion

            #region Settings

            services.AddTransient<IPeriodicInspectionStorage, PeriodicInspectionStorage>();
            services.AddTransient<IProjectTypeStorage, ProjectTypeStorage>();

            #endregion

            #endregion

            #region GENERAL

            services.AddTransient<INotificationStorage, NotificationStorage>();
            services.AddTransient<IModuleStorage, ModuleStorage>();
            services.AddTransient<ISavedFilterStorage, SavedFilterStorage>();
            services.AddTransient<IFolderStorage, FolderStorage>();
            services.AddTransient<ITagStorage, TagStorage>();
            services.AddTransient<ITagDirectoryStorage, TagDirectoryStorage>();
            services.AddTransient<IPresetStorage, PresetStorage>();
            services.AddTransient<INoteStorage, NoteStorage>();
            services.AddTransient<IFileStorage, FileStorage>();
            services.AddTransient<IUserRoleStorage, UserRoleStorage>();
            services.AddTransient<IAddressStorage, AddressStorage>(); 
            services.AddTransient<IColumnStorage, ColumnStorage>();
            services.AddTransient<IDocumentStorage, DocumentStorage>();

            #endregion

            #region SUBHIRE
            services.AddTransient<ISubhireStorage, SubhireStorage>();
            services.AddTransient<ISubhireEquipmentStorage, SubhireEquipmentStorage>();
            services.AddTransient<ISubhireEquipmentGroupStorage, SubhireEquipmentGroupStorage>();
            #endregion

            #region CREWPLANNER
            services.AddTransient<ICrewPlannerStorage, CrewPlannerStorage>();
            #endregion

            #region PROJECT
            services.AddTransient<IProjectStorage, ProjectStorage>();
            services.AddTransient<IProjectTimeStorage, ProjectTimeStorage>();

            services.AddTransient<IProjectEquipmentStorage, ProjectEquipmentStorage>();
            services.AddTransient<IProjectEquipmentGroupStorage, ProjectEquipmentGroupStorage>();

            services.AddTransient<IProjectFunctionStorage, ProjectFunctionStorage>();
            services.AddTransient<IProjectFunctionGroupStorage, ProjectFunctionGroupStorage>();

            services.AddTransient<IProjectAdditionalCostStorage, ProjectAdditionalCostStorage>();

            services.AddTransient<IProjectPlanningStorage, ProjectPlanningStorage>();

            services.AddTransient<IProjectFinancialStorage, ProjectFinancialStorage>();
            services.AddTransient<IProjectFinancialCategoryStorage, ProjectFinancialCategoryStorage>();

            services.AddTransient<IProjectEquipmentMovementStorage, ProjectEquipmentMovementStorage>();

            services.AddTransient<IProjectCrewMemberStorage, ProjectCrewMemberStorage>();

            #endregion

            #region WORKPANEL

            services.AddTransient<IWorkPanelStorage, WorkPanelStorage>();

            #endregion
        }
    }
}
