import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";
import { ModalModule } from "ngx-bootstrap";
import { UploadModule } from "@ui-components/upload/upload.module";
import { ExcelComponent } from "./excel.component";
import { TreeFoldersModule } from "@components/tree-folders/tree-folders.module";

@NgModule({
  declarations: [ExcelComponent],
  imports: [
    CommonModule,
    FormsModule,
    TranslateModule,
    ModalModule,
    UploadModule,
    TreeFoldersModule
  ],
  exports: [ExcelComponent],
  providers: []
})
export class ExcelModule {}
