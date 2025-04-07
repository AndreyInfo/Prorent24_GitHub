import { OverlayModule } from "@angular/cdk/overlay";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { pgTimePickerInnerComponent } from "./timepicker-inner.component";
import { pgTimePickerComponent } from "./timepicker.component";
import { UtilModule } from "@shared/utils/util.module";
@NgModule({
  imports: [CommonModule, OverlayModule, UtilModule],
  declarations: [pgTimePickerComponent, pgTimePickerInnerComponent],
  exports: [pgTimePickerComponent, pgTimePickerInnerComponent]
})
export class pgTimePickerModule {}
