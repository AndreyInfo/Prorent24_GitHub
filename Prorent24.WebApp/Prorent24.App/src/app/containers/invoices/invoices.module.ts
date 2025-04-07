import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./invoices.routing";

import { SharedModule } from "@shared/shared.module";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";

import { FilterPanelModule } from "@components/filter/filter-panel.module";
import { WidgetInfoModule } from "@components/widget-info/widget-info.module";
import { DocumentConfigurationModule } from "@components/document-configuration/document-configuration.module";

import { GridService } from "@services/grid.service";

import { InvoicesComponent } from "./invoices.component";
import { InvoiceFormComponent } from "./invoice-form/invoice-form.component";

import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { PERFECT_SCROLLBAR_CONFIG } from "ngx-perfect-scrollbar";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [InvoicesComponent, InvoiceFormComponent],
  imports: [
    CommonModule,
    FormsModule,
    ROUTES,
    SharedModule,
    pgDatePickerModule,
    pgTimePickerModule,
    FilterPanelModule,
    WidgetInfoModule,
    PerfectScrollbarModule,
    DocumentConfigurationModule
  ],
  exports: [],
  providers: [
    GridService,
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ]
})
export class InvoicesModule {}
