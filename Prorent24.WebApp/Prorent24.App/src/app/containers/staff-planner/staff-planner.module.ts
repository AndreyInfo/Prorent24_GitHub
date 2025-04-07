import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./staff-planner.routing";
import { SharedModule } from "@shared/shared.module";
import { FilterPanelModule } from "@components/filter/filter-panel.module";
import { CrewAndVehiclesModule } from "@components/crew-and-vehicles/crew-and-vehicles.module";
import { SchedulerModule } from "@components/sheduler/sheduler.module";
import { StaffPlannerComponent } from "./staff-planner.component";
import { pgCollapseModule } from "@ui-components/collapse";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";
import { TagModule } from "@ui-components/tag/tag.module";
import { ButtonsModule } from "ngx-bootstrap/buttons";

@NgModule({
  declarations: [StaffPlannerComponent],
  imports: [
    CommonModule,
    ROUTES,
    FormsModule,
    SharedModule,
    FilterPanelModule,
    CrewAndVehiclesModule,
    SchedulerModule,
    pgCollapseModule,
    pgDatePickerModule,
    pgTimePickerModule,
    TagModule,
    ButtonsModule
  ],
  exports: [],
  providers: []
})
export class StaffPlannerModule {}
