import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ROUTES } from "./projects.routing";
import { FormsModule } from "@angular/forms";
import { SharedModule } from "@shared/shared.module";
import { MatTabsModule } from "@angular/material/tabs";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";
import { FilterPanelModule } from "@components/filter/filter-panel.module";
import { WidgetInfoModule } from "@components/widget-info/widget-info.module";
import { TreeFoldersModule } from "@components/tree-folders/tree-folders.module";
import { jqxTreeModule } from "jqwidgets-ng/jqxtree";
import { EquipmentsCatalogModule } from "@components/equipment-catalog/equipment-catalog.module";
import { CrewAndVehiclesModule } from "@components/crew-and-vehicles/crew-and-vehicles.module";
import { SchedulerModule } from "@components/sheduler/sheduler.module";
import { NgxMaskModule } from "ngx-mask";
import { ColorPickerModule } from "ngx-color-picker";
import { GridService } from "@services/grid.service";
import { DocumentConfigurationModule } from "@components/document-configuration/document-configuration.module";
import { DocViewerModule } from "@components/doc-viewer/doc-viewer.module";

import { ProjectsComponent } from "./projects.component";
import { ProjectTabsComponent } from "./project-tabs.component";
import { ProjectFormDataComponent } from "./data/project-form-data.component";
import { ProjectEquipmentsComponent } from "./equipments/project-equipments.component";
import { ProjectAdditionalCostComponent } from "./additional-cost/project-additional-cost.component";
import { ProjectFinanceComponent } from "./finance/project-finance.component";
import { ProjectFinanceDocumentComponent } from "./finance/project-finance-document.component";
import { ProjectPlanningComponent } from "./planning/project-planning.component";
import { ProjectStaffAndVehiclesComponent } from "./staff-and-vehicles/project-staff-and-vehicles.component";
import { ProjectSubleaseComponent } from "./sublease/project-sublease.component";
import { ProjectFunctionsComponent } from "./staff-and-vehicles/project-functions/project-functions.component";

import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { PERFECT_SCROLLBAR_CONFIG } from "ngx-perfect-scrollbar";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";
import { ProjectShortageComponent } from "./shortage/project-shortage.component";

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
	suppressScrollX: true
};

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		ROUTES,
		SharedModule,
		MatTabsModule,
		pgDatePickerModule,
		pgTimePickerModule,
		FilterPanelModule,
		EquipmentsCatalogModule,
		WidgetInfoModule,
		TreeFoldersModule,
		jqxTreeModule,
		ColorPickerModule,
		PerfectScrollbarModule,
		SchedulerModule,
		NgxMaskModule.forRoot(),
		CrewAndVehiclesModule,
		DocumentConfigurationModule,
		DocViewerModule
	],
	declarations: [
		ProjectsComponent,
		ProjectTabsComponent,
		ProjectFormDataComponent,
		ProjectEquipmentsComponent,
		ProjectStaffAndVehiclesComponent,
		ProjectAdditionalCostComponent,
		ProjectSubleaseComponent,
		ProjectFinanceComponent,
		ProjectFinanceDocumentComponent,
		ProjectPlanningComponent,
		ProjectFunctionsComponent,
		ProjectShortageComponent
	],
	providers: [
		GridService,
		{
			provide: PERFECT_SCROLLBAR_CONFIG,
			useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
		}
	]
})
export class ProjectsModule {}
