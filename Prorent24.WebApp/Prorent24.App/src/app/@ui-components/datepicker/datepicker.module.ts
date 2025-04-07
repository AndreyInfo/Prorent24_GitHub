import { OverlayModule } from "@angular/cdk/overlay";
import { ObserversModule } from "@angular/cdk/observers";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { pgDatePickerComponent } from "./datepicker.component";
import { pgRangePickerComponent } from "./rangepicker.component";
import { pgCalendarViewModule } from "../calendar-view/calendar.module";

import { pgTimePickerModule } from "../time-picker/timepicker.module";
import { UtilModule } from "@shared/utils/util.module";
import { pgDateScroller } from "./datepicker-scroller.component";

@NgModule({
  imports: [
    CommonModule,
    pgTimePickerModule,
    pgCalendarViewModule,
    FormsModule,
    OverlayModule,
    UtilModule,
    ObserversModule
  ],
  declarations: [pgDatePickerComponent, pgRangePickerComponent, pgDateScroller],
  exports: [pgDatePickerComponent, pgRangePickerComponent]
})
export class pgDatePickerModule {}
