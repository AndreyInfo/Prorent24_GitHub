import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./account.routing";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";

import { SharedModule } from "@shared/shared.module";

import { ProfileFormComponent } from "./profile-form/profile-form.component";

@NgModule({
  declarations: [ProfileFormComponent],
  imports: [
    CommonModule,
    FormsModule,
    ROUTES,
    pgDatePickerModule,
    pgTimePickerModule,
    SharedModule
  ],
  exports: [],
  providers: []
})
export class AccountModule {}
