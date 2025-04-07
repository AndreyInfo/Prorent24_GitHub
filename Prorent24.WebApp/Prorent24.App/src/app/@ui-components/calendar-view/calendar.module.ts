/*
Author : NG-ZORRO
Profile : https://github.com/NG-ZORRO
Repository : https://github.com/NG-ZORRO/ng-zorro-antd
version : 0.6
Modifed : Yes
*/
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { PgSelectModule } from "../select/select.module";
import { pgCalendarViewComponent } from "./calendar.component";

@NgModule({
  imports: [CommonModule, PgSelectModule, FormsModule],
  declarations: [pgCalendarViewComponent],
  exports: [pgCalendarViewComponent]
})
export class pgCalendarViewModule {}
