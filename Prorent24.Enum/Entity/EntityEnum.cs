namespace Prorent24.Enum.Entity
{
    public enum EntityEnum
    {
        //General
        UserEntity = 0,
        DirectoryCurrencyEntity = 1,
        DirectoryCountryEntity = 2,
        DirectoryTimeZoneEntity = 3,
        DirectoryLanguageEntity = 4,
        FolderEntity =5,
        DetailEntity = 6,
        TagEntity = 7,
        NoteEntity = 9,
        FileEntity = 10,
        Role = 11,

        //Financial Configuration
        AdditionalConditionEntity = 100,
        LedgerEntity = 101,
        PaymentMethodEntity = 102,
        VatClassEntity = 103,
        VatSchemeEntity = 104,
        VatClassSchemeRateEntity = 105,
        PaymentConditionEntity = 106,
        InvoiceMomentEntity = 107,
        DiscountGroupEntity = 108,
        FactorGroupEntity = 109,
        FactorGroupOptionEntity = 110,
        SystemSettingEntity = 111,

        //Customer Communication
        SalutationEntity = 200,
        LetterheadEntity = 201,
        DocumentTemplateEntity = 202,

        //Settings Configuration
        ExtraInputFieldEntity = 300,
        ProjectTemplateEntity = 301,
        ProjectTypeEntity = 302,

        //Account
        UserRoleEntity = 400,

        //Settings
        PeriodicInspectionEntity = 500,

        //Module
        DashboardEntity = 10000,
        ScheduleEntity = 10001,
        WarehouseEntity = 10002,
        ProjectEntity = 10003,
        CrewPlannerEntity = 10004,
        SubhireEntity = 10005,
        InvoiceEntity = 10006,
        EquipmentEntity = 10007,
        ContactEntity = 10008,
        CrewMemberEntity = 10009,
        VehicleEntity = 10010,
        TaskEntity = 10011,
        TimeRegistrationEntity = 10012,
        MaintenanceEntity = 10013,
        StatisticEntity = 10014,
        ConfigurationEntity = 10015,
        
        // Module CrewMember - нумерація?
        CrewMemberRateEntity = 100091,

        // Module Equipment
        EquipmentAlternativeEntity = 100071,
        EquipmentContentEntity = 100072,
        EquipmentAccessoryEntity = 100073,
        EquipmentSupplierEntity = 100074,
        EquipmentSerialNumberEntity = 100075,
        EquipmentQRCodeEntity = 100076,
        EquipmentSerialNumberQRCodeEntity = 100077,
        EquipmentPeriodicInspectionEntity = 100078,
        EquipmentStockEntity = 100079,

        //Contact 
        ContactPersonEntity = 100092,

        //Maintenance
        InspectionEntity = 100131,
        InspectionSerialNumberEntity = 100132,
        RepairEntity = 100133,

        //Invoice

        InvoiceLineEntity = 100061,

        //Subhire
        SubhireEquipmentEntity = 100051,
        SubhireShortageEntity = 100052,

        //Project
        ProjectEquipmentEntity = 100031,
        ProjectFunctionEntity = 100032,
        ProjectAdditionalCostEntity = 100033,
        ProjectPlanningEntity = 100034,
        ProjectPlanningCrewmember = 100035,
        ProjectPlanningVehicle = 100036,

        NotificationEntity = 12
    }
}
