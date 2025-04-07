import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./vehicles.routing";

import { SharedModule } from "@shared/shared.module";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";
import { SchedulerModule } from "@components/sheduler/sheduler.module";
import { ExcelModule } from "@components/excel/excel.module";

import { FilterPanelModule } from "@components/filter/filter-panel.module";
import { WidgetInfoModule } from "@components/widget-info/widget-info.module";

import { GridService } from "@services/grid.service";

import { jqxTreeModule } from "jqwidgets-ng/jqxtree";

import { VehiclesComponent } from "./vehicles.component";
import { VehicleFormComponent } from "./vehicle-form/vehicle-form.component";
import { TreeFoldersModule } from "@components/tree-folders/tree-folders.module";

@NgModule({
  imports: [
    CommonModule,
    ROUTES,
    FormsModule,
    SharedModule,
    pgDatePickerModule,
    pgTimePickerModule,
    SchedulerModule,
    ExcelModule,
    FilterPanelModule,
    WidgetInfoModule,
    TreeFoldersModule,
    jqxTreeModule
  ],
  declarations: [VehiclesComponent, VehicleFormComponent],
  providers: [GridService]
})
export class VehiclesModule {}
