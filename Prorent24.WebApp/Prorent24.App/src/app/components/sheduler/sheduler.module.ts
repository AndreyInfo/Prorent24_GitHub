import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { DayPilotModule } from "daypilot-pro-angular";
import { SchedulerComponent } from "./sheduler.component";
import { ShedulerDataService } from "@services/sheduler-data.service";
import { ButtonsModule } from "ngx-bootstrap/buttons";

@NgModule({
  imports: [CommonModule, FormsModule, DayPilotModule, ButtonsModule],
  declarations: [SchedulerComponent],
  exports: [SchedulerComponent],
  providers: [ShedulerDataService]
})
export class SchedulerModule {}
