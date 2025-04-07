import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./schedule.routing";
import { TranslateModule } from "@ngx-translate/core";
import { SecondarySidebarModule } from "@ui-components/secondary-sidebar/secondary-sidebar.module";
import { PgCardModule } from "@ui-components/card/card.module";
import { jqxSchedulerModule } from "jqwidgets-ng/jqxscheduler";
import { ModalModule } from "ngx-bootstrap";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";
import { PgSelectModule } from "@ui-components/select/select.module";
import { EnumPipeArrayModule } from "@shared/pipes/enum-array.module";
import { ScheduleComponent } from "./schedule.component";
import { ProgressModule } from "@ui-components/progress/progress.module";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ROUTES,
    TranslateModule,
    jqxSchedulerModule,
    PgCardModule,
    ModalModule,
    pgDatePickerModule,
    pgTimePickerModule,
    PgSelectModule,
    EnumPipeArrayModule,
    SecondarySidebarModule,
    ProgressModule
  ],
  declarations: [ScheduleComponent]
})
export class ScheduleModule {}
