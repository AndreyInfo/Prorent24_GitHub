import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./staff.routing";

import { SharedModule } from "@shared/shared.module";
import { GridService } from "@services/grid.service";
import { PermissionService } from "@services/permission.service";

import { jqxTreeModule } from "jqwidgets-ng/jqxtree";
import { MatTabsModule } from "@angular/material/tabs";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";
import { NgxMaskModule } from "ngx-mask";
import { SchedulerModule } from "@components/sheduler/sheduler.module";

import { WidgetInfoModule } from "@components/widget-info/widget-info.module";
import { FilterPanelModule } from "@components/filter/filter-panel.module";
import { TreeFoldersModule } from "@components/tree-folders/tree-folders.module";

import { StaffComponent } from "./staff.component";
import { StaffFormComponent } from "./staff-form/staff-form.component";
import { StaffRatesComponent } from "./staff-rates/staff-rates.component";
import { StaffTabsComponent } from "./staff-tabs.component";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ROUTES,
    SharedModule,
    pgDatePickerModule,
    pgTimePickerModule,
    SchedulerModule,
    FilterPanelModule,
    WidgetInfoModule,
    jqxTreeModule,
    TreeFoldersModule,
    MatTabsModule,
    NgxMaskModule.forRoot()
  ],
  declarations: [
    StaffComponent,
    StaffTabsComponent,
    StaffFormComponent,
    StaffRatesComponent
  ],
  providers: [GridService, PermissionService],
  exports: []
})
export class StaffModule {}
