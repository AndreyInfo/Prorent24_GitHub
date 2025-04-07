import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { PERFECT_SCROLLBAR_CONFIG } from "ngx-perfect-scrollbar";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

import { SecondarySidebarComponent } from "./secondary-sidebar.component";

@NgModule({
  imports: [CommonModule, PerfectScrollbarModule],
  declarations: [SecondarySidebarComponent],
  providers: [
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ],
  exports: [SecondarySidebarComponent]
})
export class SecondarySidebarModule {}
