import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./warehouse.routing";
import { TranslateModule } from "@ngx-translate/core";
import { PgCardModule } from "@ui-components/card/card.module";
import { DragDropModule } from "@angular/cdk/drag-drop";
import { MatTabsModule } from "@angular/material/tabs";
import { ModalModule } from "ngx-bootstrap";
import { QRCodeModule } from "angularx-qrcode";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { CollapseModule } from "ngx-bootstrap";
import { EnumPipeArrayModule } from "@shared/pipes/enum-array.module";
import { PgSelectModule } from "@ui-components/select/select.module";
import { WarehouseComponent } from "./warehouse.component";
import { SchedulerModule } from "@components/sheduler/sheduler.module";
import { CustomFormsModule } from "ng2-validation"; 
import { WarehouseBookingComponent } from "./booking/booking.component";

import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { PERFECT_SCROLLBAR_CONFIG } from "ngx-perfect-scrollbar";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";
import { MovementStateComponent } from "./booking/movement-state.component";
import { UiSwitchModule } from 'ngx-ui-switch';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [
    WarehouseComponent,
    WarehouseBookingComponent,
    MovementStateComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ROUTES,
    TranslateModule,
    PgCardModule,
    PerfectScrollbarModule,
    DragDropModule,
    ModalModule,
    MatTabsModule,
    pgDatePickerModule,
    PgSelectModule,
    EnumPipeArrayModule,
    QRCodeModule,
    CollapseModule,
    SchedulerModule,
    CustomFormsModule,
    UiSwitchModule.forRoot({
      size: 'small',
      color: '#FFFFFF',
      switchColor: '#E7A953',
      defaultBgColor: '#FFFFFF',
      defaultBoColor : '#FFFFFF',
      checkedLabel: 'ВКЛ.',
      uncheckedLabel: 'ВЫКЛ.'
    })
  ],
  exports: [],
  providers: [
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ]
})
export class WarehouseModule {}
