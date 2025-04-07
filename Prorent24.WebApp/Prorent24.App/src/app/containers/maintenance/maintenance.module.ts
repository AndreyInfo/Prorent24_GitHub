import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./maintenance.routing";

import { SharedModule } from "@shared/shared.module";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";

import { FilterPanelModule } from "@components/filter/filter-panel.module";
import { WidgetInfoModule } from "@components/widget-info/widget-info.module";

import { GridService } from "@services/grid.service";

import { MaintenanceComponent } from "./maintenance.component";
import { MaintenanceRepairComponent } from "./repair/maintenance-repair.component";
import { MaintenanceInspectionsComponent } from "./inspections/maintenance-inspections.component";
import { MaintenanceRepairFormComponent } from "./repair/repair-form/maintenance-repair-form.component";
import { MaintenanceInspectionFormComponent } from "./inspections/inspection-form/maintenance-inspection-form.component";

@NgModule({
  declarations: [
    MaintenanceComponent,
    MaintenanceRepairComponent,
    MaintenanceInspectionsComponent,
    MaintenanceRepairFormComponent,
    MaintenanceInspectionFormComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ROUTES,
    SharedModule,
    pgDatePickerModule,
    pgTimePickerModule,
    FilterPanelModule,
    WidgetInfoModule
  ],
  exports: [],
  providers: [GridService]
})
export class MaintenanceModule {}
