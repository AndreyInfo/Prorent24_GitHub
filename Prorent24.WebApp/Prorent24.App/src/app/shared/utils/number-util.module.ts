import { NgModule } from "@angular/core";
import { NumberDirective } from "./numbers-only.directive";

@NgModule({
  declarations: [NumberDirective],
  imports: [],
  exports: [NumberDirective],
  providers: []
})
export class NumberUtilModule {}
