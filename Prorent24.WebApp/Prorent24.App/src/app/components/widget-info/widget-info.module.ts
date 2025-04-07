import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { PgCardModule } from "@ui-components/card/card.module";
import { TagModule } from "@ui-components/tag/tag.module";
import { PgSelectModule } from "@ui-components/select/select.module";
import { ModalModule, TypeaheadModule } from "ngx-bootstrap";
import { TranslateModule } from "@ngx-translate/core";
import { MatTooltipModule } from "@angular/material/tooltip";

import { WidgetInfoComponent } from "./widget-info.component";

import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { PERFECT_SCROLLBAR_CONFIG } from "ngx-perfect-scrollbar";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";
import { UploadModule } from "@ui-components/upload/upload.module";
import { EnumPipeArrayModule } from "@shared/pipes/enum-array.module";

import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";

import { SearchService } from "@services/search.service";

import { WidgetTagsComponent } from "./tags/widget-tags.component";
import { WidgetNotesComponent } from "./notes/widget-notes.component";
import { WidgetFilesComponent } from "./files/widget-files.component";
import { WidgetTasksComponent } from "./tasks/widget-tasks.component";

import { DatepickerOverviewExample } from '@ui-components/datepicker/datepicker-overview-example';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [
    WidgetInfoComponent,
    WidgetTagsComponent,
    WidgetTasksComponent,
    WidgetNotesComponent,
        WidgetFilesComponent,
        DatepickerOverviewExample
  ],
  imports: [
    CommonModule,
    FormsModule,
    PerfectScrollbarModule,
    PgCardModule,
    TagModule,
    PgSelectModule,
    ModalModule,
    TypeaheadModule,
    TranslateModule,
    UploadModule,
    EnumPipeArrayModule,
    pgDatePickerModule,
    pgTimePickerModule,
    MatTooltipModule
  ],
  exports: [WidgetInfoComponent, EnumPipeArrayModule],
  providers: [
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    },
    SearchService
  ]
})
export class WidgetInfoModule {}
