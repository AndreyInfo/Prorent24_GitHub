import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";
import { EnumPipeArrayModule } from "@shared/pipes/enum-array.module";
import { PgSelectModule } from "@ui-components/select/select.module";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { DocumentConfigurationComponent } from "./document-configuration.component";

import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { PERFECT_SCROLLBAR_CONFIG } from "ngx-perfect-scrollbar";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";
import { CustomDirectoryService } from "@services/custom-directory.service";

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [DocumentConfigurationComponent],
  imports: [
    CommonModule,
    FormsModule,
    TranslateModule,
    EnumPipeArrayModule,
    PerfectScrollbarModule,
    PgSelectModule,
    pgDatePickerModule
  ],
  exports: [DocumentConfigurationComponent],
  providers: [
    CustomDirectoryService,
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ]
})
export class DocumentConfigurationModule {}
