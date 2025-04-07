import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";
import { ModalModule } from "ngx-bootstrap";
import { jqxTreeModule } from "jqwidgets-ng/jqxtree";

import { AdditionalGridColumnComponent } from "./additional-grid-column.component";

@NgModule({
  declarations: [AdditionalGridColumnComponent],
  imports: [
    CommonModule,
    FormsModule,
    TranslateModule,
    ModalModule,
    jqxTreeModule
  ],
  exports: [AdditionalGridColumnComponent],
  providers: []
})
export class AdditionalGridColumnModule {}
