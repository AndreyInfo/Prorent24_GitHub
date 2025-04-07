import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { jqxDragDropModule } from "jqwidgets-ng/jqxdragdrop";
import { jqxTreeModule } from "jqwidgets-ng/jqxtree";
import { SharedModule } from "@shared/shared.module";

import { GridService } from "@services/grid.service";

import { EquipmentsCatalogComponent } from "./equipment-catalog.component";

@NgModule({
  declarations: [EquipmentsCatalogComponent],
  imports: [CommonModule, SharedModule, jqxDragDropModule,jqxTreeModule],
  providers: [GridService],
  exports: [EquipmentsCatalogComponent]
})
export class EquipmentsCatalogModule {}
