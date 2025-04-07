import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";
import { jqxSplitterModule } from "jqwidgets-ng/jqxsplitter";
import { PgSelectModule } from "@ui-components/select/select.module";

import { EnumPipeArrayModule } from "@shared/pipes/enum-array.module";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";

import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { PERFECT_SCROLLBAR_CONFIG } from "ngx-perfect-scrollbar";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";
import { CustomDirectoryService } from "@services/custom-directory.service";

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

import { DocViewerComponent } from "./doc-viewer.component";
import { DocFormWidgetComponent } from "./doc-form-widget.component";

@NgModule({
  declarations: [DocViewerComponent, DocFormWidgetComponent],
  imports: [
    TranslateModule,
    CommonModule,
    FormsModule,
    TranslateModule,
    jqxSplitterModule,
    PgSelectModule,
    EnumPipeArrayModule,
    pgDatePickerModule,
    PerfectScrollbarModule
  ],
  exports: [DocViewerComponent],
  providers: [
    CustomDirectoryService,
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ]
})
export class DocViewerModule {}
