import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./tasks.routing";

import { SharedModule } from "@shared/shared.module";
import { WidgetInfoModule } from "@components/widget-info/widget-info.module";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";
import { FilterPanelModule } from "@components/filter/filter-panel.module";

import { GridService } from "@services/grid.service";
import { SearchService } from "@services/search.service";

import { TasksComponent } from "./tasks.component";

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
  declarations: [TasksComponent],
  providers: [GridService, SearchService]
})
export class TasksModule {}
