import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { EnumToArrayPipe } from "./enum-array.pipe";

@NgModule({
  declarations: [EnumToArrayPipe],
  imports: [CommonModule],
  exports: [EnumToArrayPipe],
  providers: []
})
export class EnumPipeArrayModule {}
