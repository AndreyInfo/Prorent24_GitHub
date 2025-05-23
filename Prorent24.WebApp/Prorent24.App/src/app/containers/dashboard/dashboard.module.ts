import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ROUTES } from "./dashboard.routing";

import { SharedModule } from "@shared/shared.module";
import { PgTabsModule } from "@ui-components/tabs/tabs.module";

import { DashboardComponent } from "./dashboard.component";

import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { PERFECT_SCROLLBAR_CONFIG } from "ngx-perfect-scrollbar";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  imports: [
    CommonModule,
    ROUTES,
    SharedModule,
    PgTabsModule,
    PerfectScrollbarModule
  ],
  declarations: [DashboardComponent],
  providers: [
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ]
})
export class DashboardModule {}
