import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ROUTES } from "./notifications.routing";

import { SharedModule } from "@shared/shared.module";
import { GridService } from "@services/grid.service";

import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { PERFECT_SCROLLBAR_CONFIG } from "ngx-perfect-scrollbar";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

import { NotificationsComponent } from "./notifications.component";

@NgModule({
  declarations: [NotificationsComponent],
  imports: [CommonModule, ROUTES, SharedModule, PerfectScrollbarModule],
  exports: [],
  providers: [
    GridService,
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ]
})
export class NotificationsModule {}
