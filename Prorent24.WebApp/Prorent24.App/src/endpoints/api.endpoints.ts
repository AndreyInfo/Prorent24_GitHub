import { IBaseEndPoints } from "./base-endpoints.interface";

/* --- GENERAL --- */
const controller_general: string = "generals";

//#region filter
export const GeneralFiltersEndpoints: any = {
	single: `${controller_general}/filters/{0}`,
	delete: `${controller_general}/filters/{0}/delete`
};
//#endregion

//#region tags
export const GeneralTagsEndpoints: any = {
	root: `${controller_general}/tags/{0}/{1}`,
	single: `${controller_general}/tags/{0}`,
	delete: `${controller_general}/tags/{0}/delete`,
	search: `${controller_general}/tags/{0}`
};
//#endregion

//#region files
export const GeneralExcelEndpoints: any = {
	root: `${controller_general}/excel/{0}`
};
//#endregion

//#region files
export const GeneralFilesEndpoints: any = {
	root: `${controller_general}/files/{0}/{1}`,
	list: `${controller_general}/files/{0}/{1}/{2}/{3}`,
	upload: `${controller_general}/files/{0}/{1}/{2}`,
	single: `${controller_general}/files/{0}`,
	delete: `${controller_general}/files/{0}/delete`
};
//#endregion

//#region notes
export const GeneralNotesEndpoints: any = {
	root: `${controller_general}/notes/{0}/{1}`,
	single: `${controller_general}/notes/{0}`,
	delete: `${controller_general}/notes/{0}/delete`
};
//#endregion

//#region presets
export const GeneralPresetsEndpoints: any = {
	root: `${controller_general}/presets`,
	single: `${controller_general}/presets/{0}`,
	delete: `${controller_general}/presets/{0}/delete`
};
//#endregion

//#region grid columns
export const GeneralGridColumnsEndpoints: any = {
	column_groups: `${controller_general}/grids/trees/column_groups/{0}`,
	add_columns: `${controller_general}/grids/column_groups/add`,
	init_current_columns: `${controller_general}/grids/column_groups/{0}/update`,
	set_columns: `${controller_general}/grids/column_groups/set`
};
//#endregion

//#region filter
export const SearchEndpoints: any = {
	users: `crew_members/search/{0}`,
	contacts: `contacts/search/{0}`,
	contact_persons: `contacts/contact_persons/search/{0}`,
	tags: `generals/tags/{0}?search={1}`,
	equipments: `equipments/search/{0}`
};
//#endregion

//#region folders
export const FoldersEndpoints: any = {
	root: `${controller_general}/folders`,
	single: `${controller_general}/folders/{0}`,
	delete: `${controller_general}/folders/{0}/delete`,
	part: `${controller_general}/folders/type/{0}/{1}`
};
//#endregion

/* --- GRID CONFIGURATION --- */
const controller_grid: string = "generals/grids";

//#region
export const GridConfigEndpoints: any = {
	resize_column: `${controller_grid}/column_groups/{0}/columns/{1}/size/{2}`,
	reorder_column: `${controller_grid}/column_groups/{0}/columns/{1}/order/{2}`
};
//#endregion

/* --- ACCOUNT --- */
const controller_account: string = "account";
//#region session
export const AccountEndpoints: any = {
	login: `${controller_account}/login`,
	register: `${controller_account}/register`,
	logout: `${controller_account}/logout`,
	check: `${controller_account}/check`,
	checkSystemIsEmpty: `${controller_account}/check-isempty`,
	changePassword: `${controller_account}/change_password`,
	forgotPassword: `${controller_account}/forgot_password/{0}`,
	profileImg: `${controller_account}/upload/profile-image`
};
//#endregion

/* --- WORKPLANNER --- */
const controller_work_panel: string = "workPanel";
//#region tasks
export const WorkPanelEndpoints: IBaseEndPoints = {
	root: `${controller_work_panel}`,
	single: `${controller_work_panel}/{0}`,
	delete: `${controller_work_panel}/{0}/delete`,
	details: `${controller_work_panel}/{0}/details`,
	sub_root: `${controller_work_panel}/{0}/{1}`
};
//#endregion

/* --- MY SHEDULE --- */
const controller_myshedule: string = "schedule";
//#region project
export const MySheduleEndpoints: any = {
	calendar: `${controller_myshedule}/{0}`,
	new_accessibility: `crew_planners`,
	update_accessibility: `crew_planners/{0}`
};
//#endregion

/* --- WAREHOUSE --- */
const controller_warehouse: string = "projects";
//#region project
export const WarehouseEndpoints: any = {
	tasks: `${controller_warehouse}/warehouse`,
	shedule_crew: `${controller_warehouse}/plannings/warehouse/crew`,
	shedule_vehicles: `${controller_warehouse}/plannings/warehouse/transport`,
	booking: `${controller_warehouse}/{0}/warehouse`,
	booking_movement: `${controller_warehouse}/{0}/warehouse/movement`,
	booking_movements: `${controller_warehouse}/{0}/warehouse/movements`
};
//#endregion

/* --- CREW PLANNER --- */
const controller_crew_planner: string = "crew_planners";
//#region session
export const CrewPlannerEndpoints: any = {
	root: `${controller_crew_planner}`,
	scheduler: `${controller_crew_planner}/{0}/byType`,
	schedulerCrewMembers: `${controller_crew_planner}/scheduler/crew_members`,
	plannings: `${controller_crew_planner}/{0}/plannings`
};
//#endregion

/* --- PROJECTS --- */
const controller_projects: string = "projects";
//#region project
export const ProjectsEndpoints: IBaseEndPoints = {
	root: `${controller_projects}`,
	single: `${controller_projects}/{0}`,
	delete: `${controller_projects}/{0}/delete`,
	details: `${controller_projects}/{0}/details`
};
//#endregion

//#region project equipments groups
export const ProjectEquipmentGroupsEndpoints: IBaseEndPoints = {
	root: `${controller_projects}/{0}/groups`,
	single: `${controller_projects}/{0}/groups/{1}`,
	delete: `${controller_projects}/{0}/groups/{1}/delete`,
	details: `${controller_projects}/{0}/groups/{1}/details`
};
//#endregion

//#region project equipments
export const ProjectEquipmentEndpoints = {
	// : IBaseEndPoints
	root: `${controller_projects}/{0}/equipments`,
	single: `${controller_projects}/{0}/equipments/{1}`,
	delete: `${controller_projects}/{0}/equipments/{1}/delete`,
	details: `${controller_projects}/{0}/equipments/{1}/details`,
	shortages: `${controller_projects}/{0}/shortages`
};
//#endregion

//#region project function groups
export const ProjectFunctionGroupsEndpoints: IBaseEndPoints = {
	root: `${controller_projects}/{0}/function_groups`,
	single: `${controller_projects}/{0}/function_groups/{1}`,
	delete: `${controller_projects}/{0}/function_groups/{1}/delete`
};
//#endregion

//#region project functions
export const ProjectFunctionsEndpoints: IBaseEndPoints = {
	root: `${controller_projects}/functions`,
	single: `${controller_projects}/functions/{0}`,
	delete: `${controller_projects}/functions/{0}/delete`
};
//#endregion

//#region current project functions
export const ProjectConcreteFunctionsEndpoints: IBaseEndPoints = {
	root: `${controller_projects}/{0}/functions`,
	single: `${controller_projects}/{0}/functions/{1}`,
	delete: `${controller_projects}/{0}/functions/{1}/delete`
};
//#endregion

//#region project additional costs
export const ProjectAdditionalCostsEndpoints: IBaseEndPoints = {
	root: `${controller_projects}/{0}/additional_costs`,
	single: `${controller_projects}/{0}/additional_costs/{1}`,
	delete: `${controller_projects}/{0}/additional_costs/{1}/delete`
};
//#endregion

//#region project shortange
export const ProjectShortageEndpoints: IBaseEndPoints = {
	root: `${controller_projects}/{0}/shortages`,
	single: `${controller_projects}/{0}/shortages/{1}`,
	delete: `${controller_projects}/{0}/shortages/{1}/delete`
};

//#endregion

//#region project financial
export const ProjectFinancialEndpoints: IBaseEndPoints = {
	root: `${controller_projects}/{0}/financial`,
	single: `${controller_projects}/{0}/financial`,
	delete: `${controller_projects}/{0}/financial/{1}/delete`
};

export const ProjectFinancialCategoryEndpoints: IBaseEndPoints = {
	root: `${controller_projects}/{0}/financial_categories`,
	single: `${controller_projects}/{0}/financial_categories/{1}`,
	delete: `${controller_projects}/{0}/financial_categories/{1}/delete`
};
//#endregion

//#region project planning functions
export const ProjectFunctionPlanningEndpoints: IBaseEndPoints = {
	root: `${controller_projects}/{0}/function/{1}/plannings`,
	single: `${controller_projects}/{0}/function/{1}/plannings/{2}`,
	delete: `${controller_projects}/{0}/function/{1}/plannings/{2}/delete`
};
//#endregion

//#region current project planning
export const ProjectPlanningEndpoints: IBaseEndPoints = {
	root: `${controller_projects}/{0}/plannings`,
	single: `${controller_projects}/{0}/plannings/{1}`,
	delete: `${controller_projects}/{0}/plannings/{1}/delete`
};
//#endregion

/* --- INVOICES --- */
const controller_invoices: string = "invoices";
//#region project
export const InvoicesEndpoints: IBaseEndPoints = {
	root: `${controller_invoices}`,
	single: `${controller_invoices}/{0}`,
	delete: `${controller_invoices}/{0}/delete`,
	details: `${controller_invoices}/{0}/details`
};

export const InvoiceLinesEndpoints: any = {
	root: `${controller_invoices}/{0}/lines`,
	single: `${controller_invoices}/{0}/lines/{1}`,
	delete: `${controller_invoices}/{0}/lines/{1}/delete`,
	calculate_total: `${controller_invoices}/calculate_total`
};

export const InvoiceDocumentsEndpoints: any = {
	invoice: `${controller_invoices}/{0}/document`
};
//#endregion

/* --- SUBLEASE --- */
const controller_sublease: string = "subhires";
//#region sublease
export const SubleaseEndpoints: IBaseEndPoints = {
	root: `${controller_sublease}`,
	single: `${controller_sublease}/{0}`,
	delete: `${controller_sublease}/{0}/delete`,
	details: `${controller_sublease}/{0}/details`
};

export const SubleaseShortagesEndpoints: IBaseEndPoints = {
	root: `${controller_sublease}/shortages`,
	single: `${controller_sublease}/shortages/{0}`,
	delete: `${controller_sublease}/shortages/delete`,
	details: `${controller_sublease}/shortage/details`
};
//#endregion

//#region sublease group
export const SubleaseEquipmentGroupsEndpoints: IBaseEndPoints = {
	root: `${controller_sublease}/{0}/groups`,
	single: `${controller_sublease}/{0}/groups/{1}`,
	delete: `${controller_sublease}/{0}/groups/{1}/delete`,
	details: `${controller_sublease}/{0}/groups/{1}/details`
};
//#endregion

//#region sublease equipments
export const SubleaseEquipmentEndpoints: IBaseEndPoints = {
	root: `${controller_sublease}/{0}/equipments`,
	single: `${controller_sublease}/{0}/equipments/{1}`,
	delete: `${controller_sublease}/{0}/equipments/{1}/delete`,
	details: `${controller_sublease}/{0}/equipments/{1}/details`
};
//#endregion

/* --- STAFF --- */
const controller_staff: string = "crew_members";
//#region staff
export const StaffEndpoints: IBaseEndPoints = {
	root: `${controller_staff}`,
	single: `${controller_staff}/{0}`,
	delete: `${controller_staff}/{0}/delete`,
	details: `${controller_staff}/{0}/details`
};
//#endregion

//#region staff
export const StaffShedulerEndpoints: any = {
	timeline: `${controller_staff}/timeLine`
};
//#endregion

//#region staff rates
export const StaffRatesEndpoints: IBaseEndPoints = {
	root: `${controller_staff}/{0}/rates`,
	single: `${controller_staff}/{0}/rates/{1}`,
	delete: `${controller_staff}/{0}/rates/{1}/delete`
};
//#endregion

//#region staff group
export const StaffGroupEndpoints: IBaseEndPoints = {
	root: `${controller_staff}/groups`,
	single: ``,
	delete: ``
};
//#endregion

/* --- VEHICLES --- */
const controller_vehicles: string = "vehicles";
//#region vehicles
export const VehiclesEndpoints: IBaseEndPoints = {
	root: `${controller_vehicles}`,
	single: `${controller_vehicles}/{0}`,
	delete: `${controller_vehicles}/{0}/delete`,
	details: `${controller_vehicles}/{0}/details`,
	export: `${controller_vehicles}/export`
};
//#endregion

//#region vehicles
export const VehiclesGroupEndpoints: IBaseEndPoints = {
	root: `${controller_vehicles}/groups`,
	single: ``,
	delete: ``
};
//#endregion

/* --- TASKS --- */
const controller_tasks: string = "tasks";
//#region tasks
export const TasksEndpoints: IBaseEndPoints = {
	root: `${controller_tasks}`,
	single: `${controller_tasks}/{0}`,
	delete: `${controller_tasks}/{0}/delete`,
	details: `${controller_tasks}/{0}/details`,
	sub_root: `${controller_tasks}/{0}/{1}`
};
//#endregion

/* --- TIME REGISTRATION --- */
const controller_time_registration: string = "time_registration";
//#region time registration
export const TimeRegistrationEndpoints: IBaseEndPoints = {
	root: `${controller_time_registration}`,
	single: `${controller_time_registration}/{0}`,
	delete: `${controller_time_registration}/{0}/delete`,
	details: `${controller_time_registration}/{0}/details`
};
//#endregion

/* --- EQUIPMENTS --- */
const controller_equipments: string = "equipments";
//#region equipments
export const EquipmentsEndpoints: IBaseEndPoints = {
	root: `${controller_equipments}`,
	single: `${controller_equipments}/{0}`,
	delete: `${controller_equipments}/{0}/delete`,
	details: `${controller_equipments}/{0}/details`,
	export: `${controller_equipments}/export`
};
//#endregion

//#region equipments serial numbers
export const EquipmentsSerialNumbersEndpoints: IBaseEndPoints = {
	root: `${controller_equipments}/{0}/serial_numbers`,
	single: `${controller_equipments}/{0}/serial_numbers/{1}`,
	delete: `${controller_equipments}/{0}/serial_numbers/{1}/delete`
};
//#endregion

//#region equipments serial numbers qr codes
export const EquipmentsSerialNumbersQrCodeEndpoints: IBaseEndPoints = {
	root: `${controller_equipments}/{0}/serial_numbers/{1}/qr_codes`,
	single: `${controller_equipments}/{0}/serial_numbers/{1}/qr_codes/{2}`,
	delete: `${controller_equipments}/{0}/serial_numbers/{1}/qr_codes/{2}/delete`
};
//#endregion

//#region equipments accessories
export const EquipmentsAccessoriesEndpoints: IBaseEndPoints = {
	root: `${controller_equipments}/{0}/accessories`,
	single: `${controller_equipments}/{0}/accessories/{1}`,
	delete: `${controller_equipments}/{0}/accessories/{1}/delete`
};
//#endregion

//#region equipments alternatives
export const EquipmentAlternativesEndpoints: IBaseEndPoints = {
	root: `${controller_equipments}/{0}/alternatives`,
	single: `${controller_equipments}/{0}/alternatives/{1}`,
	delete: `${controller_equipments}/{0}/alternatives/{1}/delete`
};
//#endregion

//#region equipments content
export const EquipmentContentsEndpoints: IBaseEndPoints = {
	root: `${controller_equipments}/{0}/contents`,
	single: `${controller_equipments}/{0}/contents/{1}`,
	delete: `${controller_equipments}/{0}/contents/{1}/delete`
};
//#endregion

//#region equipments catalog
export const EquipmentCatalogEndpoints: IBaseEndPoints = {
	root: `${controller_equipments}/groups`,
	single: `${controller_equipments}/groups`,
	delete: null
};
//#endregion

//#region equipments suppliers
export const EquipmentsSuppliersEndpoints: IBaseEndPoints = {
	root: `${controller_equipments}/{0}/suppliers`,
	single: `${controller_equipments}/{0}/suppliers/{1}`,
	delete: `${controller_equipments}/{0}/suppliers/{1}/delete`
};
//#endregion

//#region equipments inspections
export const EquipmentsInspectionsEndpoints: IBaseEndPoints = {
	root: `${controller_equipments}/{0}/periodic_inspections`,
	single: `${controller_equipments}/{0}/periodic_inspections`,
	delete: null
};
//#endregion

//#region equipments qr-codes
export const EquipmentsQrCodesEndpoints: IBaseEndPoints = {
	root: `${controller_equipments}/{0}/qr_codes`,
	single: `${controller_equipments}/{0}/qr_codes/{1}`,
	delete: `${controller_equipments}/{0}/qr_codes/{1}/delete`
};
//#endregion

//#region equipments stock
export const EquipmentsStockEndpoints: IBaseEndPoints = {
	root: `${controller_equipments}/{0}/stock`,
	single: `${controller_equipments}/{0}/stock/{1}`,
	delete: `${controller_equipments}/{0}/stock/{1}/delete`
};
//#endregion

/* --- CONTACTS --- */
const controller_contacts: string = "contacts";
//#region contacts
export const ContactsEndpoints: IBaseEndPoints = {
	root: `${controller_contacts}`,
	single: `${controller_contacts}/{0}`,
	delete: `${controller_contacts}/{0}/delete`,
	details: `${controller_contacts}/{0}/details`
};
//#endregion

//#region contact persons
export const ContactPersonsEndpoints: IBaseEndPoints = {
	root: `${controller_contacts}/{0}/contact_persons`,
	single: `${controller_contacts}/{0}/contact_persons/{1}`,
	delete: `${controller_contacts}/{0}/contact_persons/{1}/delete`,
	details: `${controller_contacts}/{0}/contact_persons/{1}details`
};
//#endregion

//#region contact persons
export const ContactPaymentEndpoints: IBaseEndPoints = {
	root: `${controller_contacts}/{0}/contact_payment`,
	single: `${controller_contacts}/{0}/contact_payment`,
	delete: null
};
//#endregion

//#region contact digital invoice
export const ContactDigitalInvoiceEndpoints: IBaseEndPoints = {
	root: `${controller_contacts}/{0}/contact_electronic_invoice`,
	single: `${controller_contacts}/{0}/contact_electronic_invoice`,
	delete: null
};
//#endregion

/* --- DIRECTORY --- */
const controller_directory: string = "directories";
//#region directory
export const DirectoryEndpoints: any = {
	language: `${controller_directory}/language`,
	currency: `${controller_directory}/currency`,
	timeZone: `${controller_directory}/timeZone`,
	country: `${controller_directory}/country`,
	companyType: `${controller_directory}/companyType`,
	measuringUnits: `${controller_directory}/measuringUnits`
};
//#endregion

/* --- MAINTENANCE --- */
const controller_maintenance_repair: string = "repairs";
//#region repair
export const MaintenanceRepairEndpoints: IBaseEndPoints = {
	root: `${controller_maintenance_repair}`,
	single: `${controller_maintenance_repair}/{0}`,
	delete: `${controller_maintenance_repair}/{0}/delete`,
	details: `${controller_maintenance_repair}/{0}/details`
};
//#endregion

const controller_maintenance_inspections: string = "inspections";
//#region inspections
export const MaintenanceInspectionsEndpoints: IBaseEndPoints = {
	root: `${controller_maintenance_inspections}`,
	single: `${controller_maintenance_inspections}/{0}`,
	delete: `${controller_maintenance_inspections}/{0}/delete`,
	details: `${controller_maintenance_inspections}/{0}/details`
};
//#endregion

/* --- CONGIGURATION --- */
const controller_configuration: string = "configuration";
//#region part account
const segment_account: string = "account";

// company details
export const ConfigAccountCompanyDetailsEndpoints: any = {
	root: `${controller_configuration}/${segment_account}/company_details`,
	logo: `${controller_configuration}/${segment_account}/company_details/upload/logo-image`,
	login_background: `${controller_configuration}/${segment_account}/company_details/upload/background-image`
};

// user roles
export interface ISchema extends IBaseEndPoints {
	schema: string;
}

export const ConfigAccountUserRolesEndpoints: ISchema = {
	root: `${controller_configuration}/${segment_account}/user_roles`,
	single: `${controller_configuration}/${segment_account}/user_roles/{0}`,
	delete: `${controller_configuration}/${segment_account}/user_roles/{0}/delete`,
	schema: `${controller_configuration}/${segment_account}/user_roles/modules_new_role`
};
//#endregion

//#region part settings
const segment_settings: string = "settings";

// time and location
export const ConfigSettingsTimeAndLocationEndpoints: any = {
	root: `${controller_configuration}/${segment_settings}/time_location`
};

// project types
export const ConfigSettingsProjectTypesEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_settings}/project_types`,
	single: `${controller_configuration}/${segment_settings}/project_types/{0}`,
	delete: `${controller_configuration}/${segment_settings}/project_types/{0}/delete`
};

// project templates
export const ConfigSettingsProjectTemplatesEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_settings}/project_templates`,
	single: `${controller_configuration}/${segment_settings}/project_templates/{0}`,
	delete: `${controller_configuration}/${segment_settings}/project_templates/{0}/delete`
};

// inspection
export const ConfigSettingsInspectionEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_settings}/project_inspections`,
	single: `${controller_configuration}/${segment_settings}/project_inspections/{0}`,
	delete: `${controller_configuration}/${segment_settings}/project_inspections/{0}/delete`
};

// time registration
export const ConfigSettingsTimeRegistrationEndpoints: any = {
	root: `${controller_configuration}/${segment_settings}/time_registration`
};
//#endregion

//#region part communication
const segment_communication: string = "customer_communication";

// communication
export const ConfigCommunicationEndpoints: any = {
	root: `${controller_configuration}/${segment_communication}/details`
};

export const ConfigCommunicationDocTemplatesEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_communication}/document_templates`,
	single: `${controller_configuration}/${segment_communication}/document_templates/{0}`,
	delete: `${controller_configuration}/${segment_communication}/document_templates/{0}/delete`,
	list: `${controller_configuration}/${segment_communication}/document_templates/type/{0}`
};

export const ConfigCommunicationBlanksEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_communication}/letterheads`,
	single: `${controller_configuration}/${segment_communication}/letterheads/{0}`,
	delete: `${controller_configuration}/${segment_communication}/letterheads/{0}/delete`
};

export const ConfigCommunicationSalutationsEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_communication}/salutations`,
	single: `${controller_configuration}/${segment_communication}/salutations/{0}`,
	delete: `${controller_configuration}/${segment_communication}/salutations/{0}/delete`
};
//#endregion

//#region part financial
const segment_financial: string = "financial";

// financial settings
export const ConfigFinancialSettingsEndpoints: any = {
	root: `${controller_configuration}/${segment_financial}/financial_settings`
};

// electronic invoicing
export const ConfigFinancialElectronicInvoicingEndpoints: any = {
	root: `${controller_configuration}/${segment_financial}/electronic_invoicings`
};

// factor groups
export const ConfigFinancialFactorGroupsEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_financial}/factor_groups`,
	single: `${controller_configuration}/${segment_financial}/factor_groups/{0}`,
	delete: `${controller_configuration}/${segment_financial}/factor_groups/{0}/delete`
};

// discount groups
export const ConfigFinancialDiscountGroupsEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_financial}/discount_groups`,
	single: `${controller_configuration}/${segment_financial}/discount_groups/{0}`,
	delete: `${controller_configuration}/${segment_financial}/discount_groups/{0}/delete`
};

// invoice moments
export const ConfigFinancialInvoiceMomentsEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_financial}/invoice_moments`,
	single: `${controller_configuration}/${segment_financial}/invoice_moments/{0}`,
	delete: `${controller_configuration}/${segment_financial}/invoice_moments/{0}/delete`
};

// payment conditions
export const ConfigFinancialPaymentConditionsEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_financial}/payment_conditions`,
	single: `${controller_configuration}/${segment_financial}/payment_conditions/{0}`,
	delete: `${controller_configuration}/${segment_financial}/payment_conditions/{0}/delete`
};

// vat schemes
export const ConfigFinancialVatSchemesEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_financial}/vat_schemes`,
	single: `${controller_configuration}/${segment_financial}/vat_schemes/{0}`,
	delete: `${controller_configuration}/${segment_financial}/vat_schemes/{0}/delete`
};

// vat classes
export const ConfigFinancialVatClassesEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_financial}/vat_classes`,
	single: `${controller_configuration}/${segment_financial}/vat_classes/{0}`,
	delete: `${controller_configuration}/${segment_financial}/vat_classes/{0}/delete`
};

// payment methods
export const ConfigFinancialPaymentMethodsEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_financial}/payment_methods`,
	single: `${controller_configuration}/${segment_financial}/payment_methods/{0}`,
	delete: `${controller_configuration}/${segment_financial}/payment_methods/{0}/delete`
};

// ledgers
export const ConfigFinancialLedgersEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_financial}/ledgers`,
	single: `${controller_configuration}/${segment_financial}/ledgers/{0}`,
	delete: `${controller_configuration}/${segment_financial}/ledgers/{0}/delete`
};

// additional conditions
export const ConfigFinancialConditionsEndpoints: IBaseEndPoints = {
	root: `${controller_configuration}/${segment_financial}/additional_conditions`,
	single: `${controller_configuration}/${segment_financial}/additional_conditions/{0}`,
	delete: `${controller_configuration}/${segment_financial}/additional_conditions/{0}/delete`
};
//#endregion

/* --- DOCUMENTS --- */
const controller_documents: string = "documents";
//#region documents
export const DocumentsEndpoints: any = {
	create: `${controller_documents}/{0}/create`,
	info: `${controller_documents}/{0}`,
	getFile: `${controller_documents}/{0}/file`
};
//#endregion

/* --- NOTIFICATIONS --- */
const controller_notifications: string = "notifications";
//#region notifications
export const NotificationsEndpoints: IBaseEndPoints = {
	root: `${controller_notifications}`,
	single: `${controller_notifications}/{0}`,
	delete: `${controller_notifications}/{0}/delete`,
	details: `${controller_notifications}/{0}`,
	list: `${controller_notifications}/short`
};
//#endregion
