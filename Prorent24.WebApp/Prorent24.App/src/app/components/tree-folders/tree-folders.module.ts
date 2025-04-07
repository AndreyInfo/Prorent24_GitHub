import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";
import { jqxTreeModule } from "jqwidgets-ng/jqxtree";
import { jqxCheckBoxModule } from "jqwidgets-ng/jqxcheckbox";
import { jqxMenuModule } from "jqwidgets-ng/jqxmenu";
import { ModalModule } from "ngx-bootstrap";
import { TreeFoldersComponent } from "./tree-folders.component";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { FoldersService } from "@services/folders.service";

@NgModule({
  declarations: [TreeFoldersComponent],
  imports: [
    CommonModule,
    FormsModule,
    TranslateModule,
    ModalModule,
    jqxTreeModule,
    jqxCheckBoxModule,
    jqxMenuModule
  ],
  exports: [TreeFoldersComponent],
  providers: [CustomDirectoryService, FoldersService]
})
export class TreeFoldersModule {}
