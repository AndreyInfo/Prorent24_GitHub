
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Enum.Configuration.ConfigurationRoles
{
    public enum PermissionFieldEnum
    {
        #region General

        [FieldValueType(selectPermission: PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 1, Description = "Is allowed to edit his own user data")]
        OwnDataGeneral = 1,

        [FieldValueType(PermissionValueTypeEnum.DownloadFilePermission,
            defaultRole: nameof(DownloadFilePermissionEnum.No),
            freelancerRole: nameof(DownloadFilePermissionEnum.YesButNoConfidentialFiles),
            powerUserRole: nameof(DownloadFilePermissionEnum.AllFiles),
            officeRole: nameof(DownloadFilePermissionEnum.YesButNoConfidentialFiles),
            Order = 2, Description = "")]
        DownloadFilesGeneral = 2,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 3, Description = "")]
        UploadFilesGeneral = 3,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 4, Description = "")]
        PrintPackSlipGeneral = 4,

        [FieldValueType(PermissionValueTypeEnum.TemplatesEmailsPersonalTextPermission,
            defaultRole: nameof(TemplatesEmailsPersonalTextPermissionEnum.OnlyEditAndDeleteYourOwn),
            freelancerRole: nameof(TemplatesEmailsPersonalTextPermissionEnum.OnlyEditAndDeleteYourOwn),
            powerUserRole: nameof(TemplatesEmailsPersonalTextPermissionEnum.AlsoEditAndDeletePublicOnes),
            officeRole: nameof(TemplatesEmailsPersonalTextPermissionEnum.OnlyEditAndDeleteYourOwn),
            Order = 5, Description = "This right applies to the default texts for emails and slips")]
        TemplateEmailGeneral = 5,
        #endregion

        #region Schedule
        [FieldValueType(PermissionValueTypeEnum.EntityShortModificationPermission,
            defaultRole: nameof(EntityShortModificationPermissionEnum.OnlyView),
            freelancerRole: nameof(EntityShortModificationPermissionEnum.OnlyView),
            powerUserRole: nameof(EntityShortModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityShortModificationPermissionEnum.ViewEditDelete),
            Order = 6, Description = "")]
        OwnAppointmentSchedule = 6,

        [FieldValueType(PermissionValueTypeEnum.EntityShortModificationPermission,
            defaultRole: nameof(EntityShortModificationPermissionEnum.OnlyView),
            freelancerRole: nameof(EntityShortModificationPermissionEnum.OnlyView),
            powerUserRole: nameof(EntityShortModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityShortModificationPermissionEnum.ViewEditDelete),
            Order = 7, Description = "")]
        OwnAvailabilitySchedule = 7,

        [FieldValueType(PermissionValueTypeEnum.EntityNoShortModificationPermission,
            defaultRole: nameof(EntityNoShortModificationPermissionEnum.NoPermissions),
            freelancerRole: nameof(EntityNoShortModificationPermissionEnum.NoPermissions),
            powerUserRole: nameof(EntityNoShortModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityNoShortModificationPermissionEnum.ViewEditDelete),
            Order = 8, Description = "")]
        AppointmentOtherSchedule = 8,

        [FieldValueType(PermissionValueTypeEnum.EntityNoShortModificationPermission,
            defaultRole: nameof(EntityNoShortModificationPermissionEnum.NoPermissions),
            freelancerRole: nameof(EntityNoShortModificationPermissionEnum.NoPermissions),
            powerUserRole: nameof(EntityNoShortModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityNoShortModificationPermissionEnum.ViewEditDelete),
            Order = 9, Description = "This right applies to availability and reservations")]
        AvailabilityOtherSchedule = 9,

        [FieldValueType(PermissionValueTypeEnum.ProjectPlanningPermission,
            defaultRole: nameof(ProjectPlanningPermissionEnum.NoAccessToProjectPlanning),
            freelancerRole: nameof(ProjectPlanningPermissionEnum.FunctionsInSubProjectsForWhichThisCrewMembersIsScheduled),
            powerUserRole: nameof(ProjectPlanningPermissionEnum.AllFunctionsInAllProjects),
            officeRole: nameof(ProjectPlanningPermissionEnum.AllFunctionsInAllProjects),
            Order = 10, Description = "This right refers to the functions that this employee is allowed to see")]
        ProjPlanningCrewMembSchedule = 10,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.Yes),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 11, Description = "This permission only applies to the app. Can the crew member see which equipment is planned on projects?")]
        PlannedEquipmentProjSchedule = 11,
        #endregion

        #region Warehouse
        [FieldValueType(PermissionValueTypeEnum.WhichProjectWarehousePermission,
            defaultRole: nameof(WhichProjectWarehousePermissionEnum.AllProjects),
            freelancerRole: nameof(WhichProjectWarehousePermissionEnum.AllProjects),
            powerUserRole: nameof(WhichProjectWarehousePermissionEnum.AllProjects),
            officeRole: nameof(WhichProjectWarehousePermissionEnum.AllProjects),
            Order = 12, Description = "")]
        WhichProjWarehouse = 12,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 13, Description = "Change project status into 'prepped', 'on location' or 'return'")]
        ChangeProjectStatusWarehouse = 13,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 14, Description = "With this authorization, crew members can pack different or more equipment than initially planned for this project")]
        AddItemWarehouse = 14,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 15, Description = "")]
        CreateRepairsWarehouse = 15,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 16, Description = "")]
        ChangeStockWarehouse = 16,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 17, Description = "")]
        InvoiceCostWarehouse = 17,
        #endregion

        #region Project
        [FieldValueType(PermissionValueTypeEnum.WhichProjectPermission,
            defaultRole: nameof(WhichProjectWarehousePermissionEnum.AllProjects),
            freelancerRole: nameof(WhichProjectWarehousePermissionEnum.AllProjects),
            powerUserRole: nameof(WhichProjectWarehousePermissionEnum.AllProjects),
            officeRole: nameof(WhichProjectWarehousePermissionEnum.AllProjects),
            Order = 18, Description = "Own projects are projects for which this crew member is the account manager")]
        WhichProjProject = 18,

        [FieldValueType(PermissionValueTypeEnum.ChangeProjectPermission,
            defaultRole: nameof(ChangeProjectPermissionEnum.No),
            freelancerRole: nameof(ChangeProjectPermissionEnum.No),
            powerUserRole: nameof(ChangeProjectPermissionEnum.AllProjects),
            officeRole: nameof(ChangeProjectPermissionEnum.No),
            Order = 19, Description = "")]
        ChangeProjProject = 19,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.No),
            Order = 20, Description = "Concerns only projects that this crew member is allowed to edit")]
        DeleteProjProject = 20,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.Yes),
            freelancerRole: nameof(BooleanSelectPermissionEnum.Yes),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 21, Description = "")]
        EditDeleteFunctionProject = 21,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.No),
            Order = 22, Description = "")]
        CreateInvoiceProject = 22,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.No),
            Order = 23, Description = "")]
        CreateQuotContractProject = 23,

        [FieldValueType(PermissionValueTypeEnum.AvailabilityOfEquipmentPermission,
            defaultRole: nameof(AvailabilityOfEquipmentPermissionEnum.NoPermissions),
            freelancerRole: nameof(AvailabilityOfEquipmentPermissionEnum.NoPermissions),
            powerUserRole: nameof(AvailabilityOfEquipmentPermissionEnum.ProjectsAndSubhire),
            officeRole: nameof(AvailabilityOfEquipmentPermissionEnum.NoPermissions),
            Order = 24, Description = "Concerns the equipment availability timeline in projects and the equipment module.This also determines if shortages are shown in red in the project")]
        AvailabilityEquipmentProject = 24,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.No),
            Order = 25, Description = "Concerns all projects visible to this crew member")]
        ChangeScheduleProject = 25,
        #endregion

        #region CrewPlann
        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.No),
            Order = 26, Description = "Concerns all projects visible to this crew member")]
        ChangeScheduleCrewPlann = 26,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.No),
            Order = 27, Description = "")]
        SendPlanEmailCrewPlann = 27,
        #endregion

        #region Subhire
        [FieldValueType(PermissionValueTypeEnum.EditSubhirePermission,
            defaultRole: nameof(EditSubhirePermissionEnum.No),
            freelancerRole: nameof(EditSubhirePermissionEnum.No),
            powerUserRole: nameof(EditSubhirePermissionEnum.AllSubhireJobs),
            officeRole: nameof(EditSubhirePermissionEnum.AllSubhireJobs),
            Order = 28, Description = "")]
        EditSubhire = 28,
        #endregion

        #region Invoice 
        [FieldValueType(PermissionValueTypeEnum.EntityModificationPermission,
            defaultRole: nameof(EntityModificationPermissionEnum.OnlyView),
            freelancerRole: nameof(EntityModificationPermissionEnum.OnlyView),
            powerUserRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityModificationPermissionEnum.OnlyView),
            Order = 29, Description = "Own invoices are invoices belonging to projects for which this crew member is the account manager")]
        OwnInvoice = 29,

        [FieldValueType(PermissionValueTypeEnum.EntityFullModificationPermission,
            defaultRole: nameof(EntityFullModificationPermissionEnum.NoPermissions),
            freelancerRole: nameof(EntityFullModificationPermissionEnum.NoPermissions),
            powerUserRole: nameof(EntityFullModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityFullModificationPermissionEnum.NoPermissions),
            Order = 30, Description = "")]
        OtherAccountInvoice = 30,
        #endregion

        #region Equipment
        [FieldValueType(PermissionValueTypeEnum.EntityModificationPermission,
            defaultRole: nameof(EntityModificationPermissionEnum.ViewEdit),
            freelancerRole: nameof(EntityModificationPermissionEnum.ViewEdit),
            powerUserRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            Order = 31, Description = "")]
        DatabaseEquipment = 31,

        [FieldValueType(PermissionValueTypeEnum.AvailabilityOfEquipmentPermission,
            defaultRole: nameof(AvailabilityOfEquipmentPermissionEnum.NoPermissions),
            freelancerRole: nameof(AvailabilityOfEquipmentPermissionEnum.NoPermissions),
            powerUserRole: nameof(AvailabilityOfEquipmentPermissionEnum.ProjectsAndSubhire),
            officeRole: nameof(AvailabilityOfEquipmentPermissionEnum.NoPermissions),
            Order = 32, Description = "Concerns the equipment availability timeline in projects and the equipment module.This also determines if shortages are shown in red in the project")]
        AvailabilityEquipment = 32,
        #endregion

        #region Contract
        [FieldValueType(PermissionValueTypeEnum.EntityModificationPermission,
            defaultRole: nameof(EntityModificationPermissionEnum.OnlyView),
            freelancerRole: nameof(EntityModificationPermissionEnum.ViewEdit),
            powerUserRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            Order = 33, Description = "")]
        DatabaseContract = 33,
        #endregion

        #region CrewMember
        [FieldValueType(PermissionValueTypeEnum.EntityModificationPermission,
            defaultRole: nameof(EntityModificationPermissionEnum.ViewEdit),
            freelancerRole: nameof(EntityModificationPermissionEnum.ViewEdit),
            powerUserRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            Order = 34, Description = "")]
        DatabaseCrewMember = 34,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.No),
            Order = 35, Description = "Is allowed to change passwords, user roles and email addresses of crew members")]
        LogiInfoCrewMember = 35,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 36, Description = "Display projects and appointments for which a crew member is scheduled")]
        TimelineCrewMember = 36,
        #endregion

        #region Vehicle
        [FieldValueType(PermissionValueTypeEnum.EntityModificationPermission,
            defaultRole: nameof(EntityModificationPermissionEnum.ViewEdit),
            freelancerRole: nameof(EntityModificationPermissionEnum.ViewEdit),
            powerUserRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            Order = 37, Description = "")]
        DatabaseVehicle = 37,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 38, Description = "Is allowed to see the timeline of vehicles in the vehicles module.The timeline shows the projects a vehicle is planned on")]
        TimelineVehicle = 38,
        #endregion

        #region Task
        [FieldValueType(PermissionValueTypeEnum.EntityFullModificationPermission,
            defaultRole: nameof(EntityFullModificationPermissionEnum.NoPermissions),
            freelancerRole: nameof(EntityFullModificationPermissionEnum.NoPermissions),
            powerUserRole: nameof(EntityFullModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityFullModificationPermissionEnum.ViewEdit),
            Order = 39, Description = "")]
        OtherTask = 39,
        #endregion

        #region TimeReg
        [FieldValueType(PermissionValueTypeEnum.EntityModificationPermission,
            defaultRole: nameof(EntityModificationPermissionEnum.ViewEdit),
            freelancerRole: nameof(EntityModificationPermissionEnum.ViewEdit),
            powerUserRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            Order = 40, Description = "")]
        OwnHourTimeReg = 40,

        [FieldValueType(PermissionValueTypeEnum.EntityFullModificationPermission,
            defaultRole: nameof(EntityFullModificationPermissionEnum.NoPermissions),
            freelancerRole: nameof(EntityFullModificationPermissionEnum.NoPermissions),
            powerUserRole: nameof(EntityFullModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityFullModificationPermissionEnum.OnlyView),
            Order = 41, Description = "")]
        CrewMemberHourTimeReg = 41,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.No),
            Order = 42, Description = "")]
        PreRegHourTimeReg = 42,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.Yes),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.Yes),
            Order = 43, Description = "")]
        ClockAppTimeReg = 43,
        #endregion

        #region Maintenance
        [FieldValueType(PermissionValueTypeEnum.EntityModificationPermission,
            defaultRole: nameof(EntityModificationPermissionEnum.OnlyView),
            freelancerRole: nameof(EntityModificationPermissionEnum.OnlyView),
            powerUserRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            officeRole: nameof(EntityModificationPermissionEnum.ViewEditDelete),
            Order = 44, Description = "")]
        RepairInspectionMaintenance = 44,
        #endregion

        #region Configuration
        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.No),
            Order = 45, Description = "")]
        CompanyInfo = 45,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
            defaultRole: nameof(BooleanSelectPermissionEnum.No),
            freelancerRole: nameof(BooleanSelectPermissionEnum.No),
            powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
            officeRole: nameof(BooleanSelectPermissionEnum.No),
            Order = 46, Description = "")]
        UserRoles = 46,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
                   defaultRole: nameof(BooleanSelectPermissionEnum.No),
                   freelancerRole: nameof(BooleanSelectPermissionEnum.No),
                   powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
                   officeRole: nameof(BooleanSelectPermissionEnum.No),
                   Order = 47, Description = "")]
        TimeAndLocation = 47,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
                   defaultRole: nameof(BooleanSelectPermissionEnum.No),
                   freelancerRole: nameof(BooleanSelectPermissionEnum.No),
                   powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
                   officeRole: nameof(BooleanSelectPermissionEnum.No),
                   Order = 48, Description = "")]
        ProjectTypes = 48,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
                   defaultRole: nameof(BooleanSelectPermissionEnum.No),
                   freelancerRole: nameof(BooleanSelectPermissionEnum.No),
                   powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
                   officeRole: nameof(BooleanSelectPermissionEnum.No),
                   Order = 49, Description = "")]
        Communication = 49,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
                   defaultRole: nameof(BooleanSelectPermissionEnum.No),
                   freelancerRole: nameof(BooleanSelectPermissionEnum.No),
                   powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
                   officeRole: nameof(BooleanSelectPermissionEnum.No),
                   Order = 50, Description = "")]
        Financial = 50,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
                   defaultRole: nameof(BooleanSelectPermissionEnum.No),
                   freelancerRole: nameof(BooleanSelectPermissionEnum.No),
                   powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
                   officeRole: nameof(BooleanSelectPermissionEnum.No),
                   Order = 51, Description = "")]
        InvoiceMoments = 51,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
                   defaultRole: nameof(BooleanSelectPermissionEnum.No),
                   freelancerRole: nameof(BooleanSelectPermissionEnum.No),
                   powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
                   officeRole: nameof(BooleanSelectPermissionEnum.No),
                   Order = 52, Description = "")]
        DiscountGroups = 52,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
                   defaultRole: nameof(BooleanSelectPermissionEnum.No),
                   freelancerRole: nameof(BooleanSelectPermissionEnum.No),
                   powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
                   officeRole: nameof(BooleanSelectPermissionEnum.No),
                   Order = 53, Description = "")]
        PaymentConditions = 53,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
                   defaultRole: nameof(BooleanSelectPermissionEnum.No),
                   freelancerRole: nameof(BooleanSelectPermissionEnum.No),
                   powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
                   officeRole: nameof(BooleanSelectPermissionEnum.No),
                   Order = 54, Description = "")]
        VatSchemes = 54,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
                   defaultRole: nameof(BooleanSelectPermissionEnum.No),
                   freelancerRole: nameof(BooleanSelectPermissionEnum.No),
                   powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
                   officeRole: nameof(BooleanSelectPermissionEnum.No),
                   Order = 55, Description = "")]
        PaymentMethods = 55,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
                  defaultRole: nameof(BooleanSelectPermissionEnum.No),
                  freelancerRole: nameof(BooleanSelectPermissionEnum.No),
                  powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
                  officeRole: nameof(BooleanSelectPermissionEnum.No),
                  Order = 56, Description = "")]
        Ledger = 56,

        [FieldValueType(PermissionValueTypeEnum.BooleanSelectPermission,
           defaultRole: nameof(BooleanSelectPermissionEnum.No),
           freelancerRole: nameof(BooleanSelectPermissionEnum.No),
           powerUserRole: nameof(BooleanSelectPermissionEnum.Yes),
           officeRole: nameof(BooleanSelectPermissionEnum.No),
           Order = 57, Description = "")]
        DocumentTemplate = 57,


        #endregion

    }
}
