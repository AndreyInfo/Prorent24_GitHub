using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Prorent24.BLL.Consumers.EmailConsumers;
using Prorent24.BLL.Handlers.ProjectHandlers;
using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Services.Configuration.AccountConfiguration.CompanyDetails;
using Prorent24.BLL.Services.Configuration.AccountConfiguration.UserRole;
using Prorent24.BLL.Services.Configuration.CustomerCommunication.Communication;
using Prorent24.BLL.Services.Configuration.CustomerCommunication.DocumentTemplate;
using Prorent24.BLL.Services.Configuration.CustomerCommunication.Letterhead;
using Prorent24.BLL.Services.Configuration.CustomerCommunication.Salutation;
using Prorent24.BLL.Services.Configuration.Directory;
using Prorent24.BLL.Services.Configuration.Financial.AdditionalCondition;
using Prorent24.BLL.Services.Configuration.Financial.DiscountGroup;
using Prorent24.BLL.Services.Configuration.Financial.ElectronicInvoicing;
using Prorent24.BLL.Services.Configuration.Financial.FactorGroup;
using Prorent24.BLL.Services.Configuration.Financial.FinancialSetting;
using Prorent24.BLL.Services.Configuration.Financial.InvoiceMoment;
using Prorent24.BLL.Services.Configuration.Financial.Ledger;
using Prorent24.BLL.Services.Configuration.Financial.Payment;
using Prorent24.BLL.Services.Configuration.Financial.Vat;
using Prorent24.BLL.Services.Configuration.Settings;
using Prorent24.BLL.Services.Configuration.Settings.PeriodicInspection;
using Prorent24.BLL.Services.Configuration.Settings.ProjectType;
using Prorent24.BLL.Services.Configuration.Settings.TimeAndLocation;
using Prorent24.BLL.Services.Configuration.Settings.TimeRegistrationSettings;
using Prorent24.BLL.Services.Contact;
using Prorent24.BLL.Services.CrewMember;
using Prorent24.BLL.Services.CrewPlanner;
using Prorent24.BLL.Services.Equipment;
using Prorent24.BLL.Services.Equipment.Accessory;
using Prorent24.BLL.Services.Equipment.Alternative;
using Prorent24.BLL.Services.Equipment.Content;
using Prorent24.BLL.Services.Equipment.PeriodicInspection;
using Prorent24.BLL.Services.Equipment.SerialNumber;
using Prorent24.BLL.Services.Equipment.SerialNumber.QRCode;
using Prorent24.BLL.Services.Equipment.Stock;
using Prorent24.BLL.Services.Equipment.Supplier;
using Prorent24.BLL.Services.Equipment.Webshop;
using Prorent24.BLL.Services.General.Excel;
using Prorent24.BLL.Services.General.File;
using Prorent24.BLL.Services.General.Filter;
using Prorent24.BLL.Services.General.Folder;
using Prorent24.BLL.Services.General.Grid;
using Prorent24.BLL.Services.General.Mail;
using Prorent24.BLL.Services.General.Module;
using Prorent24.BLL.Services.General.Note;
using Prorent24.BLL.Services.General.Tag;
using Prorent24.BLL.Services.General.Tree;
using Prorent24.BLL.Services.Invoice;
using Prorent24.BLL.Services.Invoice.Excluded;
using Prorent24.BLL.Services.Invoice.InvoiceLine;
using Prorent24.BLL.Services.Maintenance.Inspection;
using Prorent24.BLL.Services.Maintenance.Inspection.SerialNumber;
using Prorent24.BLL.Services.Maintenance.InspectionSerialNumber.SerialNumber;
using Prorent24.BLL.Services.Maintenance.Repair;
using Prorent24.BLL.Services.Project;
using Prorent24.BLL.Services.Project.AdditionalCost;
using Prorent24.BLL.Services.Project.Equipment;
using Prorent24.BLL.Services.Project.Financial;
using Prorent24.BLL.Services.Project.Function;
using Prorent24.BLL.Services.Project.Movement;
using Prorent24.BLL.Services.Project.Planning;
using Prorent24.BLL.Services.Schedule;
using Prorent24.BLL.Services.Subhire;
using Prorent24.BLL.Services.Tasks;
using Prorent24.BLL.Services.TimeRegistration;
using Prorent24.BLL.Services.Vehicle;
using Prorent24.BLL.Services.WorkPanel;
using Prorent24.DAL;
using Prorent24.BLL.Services.General.Document;
using Prorent24.BLL.Handlers.EquipmentHandlers;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.AspUser;
using Prorent24.BLL.Services.Notification;
using Prorent24.BLL.Handlers.NotificationHandlers;

namespace Prorent24.BLL
{
    public static class DependencyInjection
    {
        public static void AddBusinessLogicLayer(this IServiceCollection services)
        {
            #region Handlers

            services.AddTransient<INotificationHandler<SendPasswordHandlerModel>, SendPasswordHandler>();
            services.AddTransient<INotificationHandler<ChangeStatusProjectModel>, ProjectChangeStatusHandler>();
            services.AddTransient<INotificationHandler<EquipmentReservedHandlerModel>, ReserveEquipmentHandler>();
            services.AddTransient<INotificationHandler<NotificationHandlerModel>, NotificationHandler>();

            #endregion

            #region Invoice

            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IInvoiceLineService, InvoiceLineService>();
            services.AddTransient<IInvoiceExcludedService, InvoiceExcludedService>();

            #endregion

            #region Equipment

            services.AddTransient<IEquipmentService, EquipmentService>();
            services.AddTransient<IEquipmentAlternativeService, EquipmentAlternativeService>();
            services.AddTransient<IEquipmentContentService, EquipmentContentService>();
            services.AddTransient<IEquipmentAccessoryService, EquipmentAccessoryService>();
            services.AddTransient<IEquipmentSupplierService, EquipmentSupplierService>();
            services.AddTransient<IEquipmentSerialNumberService, EquipmentSerialNumberService>();
            services.AddTransient<IEquipmentQRCodeService, EquipmentQRCodeService>();
            services.AddTransient<IEquipmentPeriodicInspectionService, EquipmentPeriodicInspectionService>();
            services.AddTransient<IEquipmentWebShopService, EquipmentWebShopService>();
            services.AddTransient<IEquipmentStockService, EquipmentStockService>();

            #endregion

            #region Contact

            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IContactPersonService, ContactPersonService>();
            services.AddTransient<IContactPaymentService, ContactPaymentService>();
            services.AddTransient<IContactElectronicInvoiceService, ContactElectronicInvoiceService>();
            #endregion

            #region CrewMember

            services.AddTransient<ICrewMemberService, CrewMemberService>();
            services.AddTransient<ICrewMemberRateService, CrewMemberRateService>();

            #endregion

            #region Vehicle

            services.AddTransient<IVehicleService, VehicleService>();

            #endregion

            #region Task

            services.AddTransient<ITaskService, TaskService>();

            #endregion

            #region TimeRegistration

            services.AddTransient<ITimeRegistrationService, TimeRegistrationService>();

            #endregion

            #region Maintenance

            services.AddTransient<IInspectionService, InspectionService>();
            services.AddTransient<IInspectionSerialNumberService, InspectionSerialNumberService>();
            services.AddTransient<IRepairService, RepairService>();

            #endregion

            #region Configuration

            #region Directory

            services.AddTransient<IDirectoryService, DirectoryService>();

            #endregion


            #region Account

            services.AddTransient<ICompanyDetailsService, CompanyDetailsService>();
            services.AddTransient<IUserRoleService, UserRoleService>();

            #endregion

            #region Settings

            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ITimeAndLocationService, TimeAndLocationService>();
            services.AddTransient<IPeriodicInspectionService, PeriodicInspectionService>();
            services.AddTransient<ITimeRegistrationSettingsService, TimeRegistrationSettingsService>();
            services.AddTransient<IProjectTypeService, ProjectTypeService>();

            #endregion

            #region CustomerCommunication

            services.AddTransient<ICustomerCommmunicationService, CustomerCommmunicationService>();
            services.AddTransient<ISalutationService, SalutationService>();
            services.AddTransient<IDocumentTemplateService, DocumentTemplateService>();
            services.AddTransient<ILetterheadService, LetterheadService>();

            #endregion

            #region Financial

            services.AddTransient<IElectronicInvoicingService, ElectronicInvoicingService>();
            services.AddTransient<IPaymentConditionService, PaymentConditionService>();
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();
            services.AddTransient<IVatSchemeService, VatSchemeService>();
            services.AddTransient<IVatClassService, VatClassService>();
            services.AddTransient<IAdditionalConditionService, AdditionalConditionService>();
            services.AddTransient<ILedgerService, LedgerService>();
            services.AddTransient<IDiscountGroupService, DiscountGroupService>();
            services.AddTransient<IFactorGroupService, FactorGroupService>();
            services.AddTransient<IInvoiceMomentService, InvoiceMomentService>();
            services.AddTransient<IFinancialSettingService, FinancialSettingService>();
            services.AddTransient<IFactorGroupOptionService, FactorGroupOptionService>();

            #endregion

            #endregion

            #region General

            services.AddTransient<IMailService, MailService>();

            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IGridService, GridService>();
            services.AddTransient<ITreeService, TreeService>();
            services.AddTransient<IPresetService, PresetService>();
            services.AddTransient<IFolderService, FolderService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IFilterService, FilterService>();
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IExcelService, ExcelService>();
            services.AddTransient<IDocumentService, DocumentService>();

            #endregion

            #region Subhire
            services.AddTransient<ISubhireService, SubhireService>();
            services.AddTransient<ISubhireEquipmentGroupService, SubhireEquipmentGroupService>();
            #endregion

            #region CrewPlanner
            services.AddTransient<ICrewPlannerService, CrewPlannerService>();
            #endregion

            #region Project

            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IProjectEquipmentService, ProjectEquipmentService>();
            services.AddTransient<IProjectFunctionService, ProjectFunctionService>();
            services.AddTransient<IProjectAdditionalCostService, ProjectAdditionalCostService>();
            services.AddTransient<IProjectPlanningService, ProjectPlanningService>();
            services.AddTransient<IProjectFinancialService, ProjectFinancialService>();
            services.AddTransient<IProjectEquipmentMovementService, ProjectEquipmentMovementService>();

            #endregion

            #region WorkPanel

            services.AddTransient<IWorkPanelService, WorkPanelService>();

            #endregion

            #region Schedule

            services.AddTransient<IScheduleService, ScheduleService>();

            #endregion

            #region User

            services.AddTransient<IUserService, UserService>();

            #endregion

            #region Notification

            services.AddTransient<INotificationService, NotificationService>();

            #endregion

            
            services.AddTransient<IPermissionService, BaseService>();
            services.AddDataAccessLayer();
        }
    }
}
