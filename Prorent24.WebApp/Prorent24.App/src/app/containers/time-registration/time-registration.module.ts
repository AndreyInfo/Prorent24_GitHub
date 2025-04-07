import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./time-registration.routing";

import { SharedModule } from "@shared/shared.module";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";
import { FilterPanelModule } from "@components/filter/filter-panel.module";
import { WidgetInfoModule } from "@components/widget-info/widget-info.module";

import { GridService } from "@services/grid.service";
import { SearchService } from "@services/search.service";

import { TimeRegistrationComponent } from "./time-registration.component";
import { TimeRegistrationFormComponent } from "./time-registration-form/time-registration-form.component";

@NgModule({
  imports: [
    CommonModule,
    ROUTES,
    FormsModule,
    SharedModule,
    pgDatePickerModule,
    pgTimePickerModule,
    FilterPanelModule,
    WidgetInfoModule
  ],
  declarations: [TimeRegistrationComponent, TimeRegistrationFormComponent],
  providers: [GridService, SearchService]
})
export class TimeRegistrationModule {}
