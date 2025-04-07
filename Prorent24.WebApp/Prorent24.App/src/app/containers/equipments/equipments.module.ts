import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./equipments.routing";

import { MatTabsModule } from "@angular/material/tabs";
import { jqxDragDropModule } from "jqwidgets-ng/jqxdragdrop";
import { SharedModule } from "@shared/shared.module";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";
import { FilterPanelModule } from "@components/filter/filter-panel.module";
import { WidgetInfoModule } from "@components/widget-info/widget-info.module";
import { TreeFoldersModule } from "@components/tree-folders/tree-folders.module";
import { EquipmentsCatalogModule } from "@components/equipment-catalog/equipment-catalog.module";
import { QRCodeModule } from "angularx-qrcode";

import { GridService } from "@services/grid.service";

import { EquipmentsComponent } from "./equipments.component";
import { EquipmentTabsComponent } from "./equipment-tabs.component";
import { EquipmentDataComponent } from "./data/equipment-form-data.component";
import { EquipmentSerialNumbersComponent } from "./serial-numbers/equipment-serial-numbers.component";
import { EquipmentSuppliersComponent } from "./suppliers/equipment-suppliers.component";
import { EquipmentAccessoriesComponent } from "./accessories/equipment-accessories.component";
import { EquipmentAlternativesComponent } from "./alternatives/equipment-alternatives.component";
import { EquipmentSerialNumbersTabsComponent } from "./serial-numbers/equipment-serial-numbers-tabs.component";
import { EquipmentSerialNumbersDataComponent } from "./serial-numbers/equipment-serial-numbers-data.component";
import { EquipmentSerialNumbersHistoryComponent } from "./serial-numbers/equipment-serial-numbers-history.component";
import { EquipmentSerialNumbersQRcodeComponent } from "./serial-numbers/equipment-serial-numbers-qrcode.component";
import { EquipmentQrCodesComponent } from "./qr-codes/equipment-qr-codes.component";
import { EquipmentInspectionsComponent } from "./inspections/equipment-inspections.component";
import { EquipmentContentsComponent } from "./contents/equipment-contents.component";
import { EquipmentStockComponent } from "./stock/equipment-stock.component";
import { EquipmentFilesComponent } from './files/equipment-files.component';
import { EquipmentImagesComponent } from './images/equipment-images.component';
import { NgxMaskModule } from "ngx-mask";
import { ExcelModule } from "@components/excel/excel.module";
import { EqTabEventService } from './eq-tab-event.service';

@NgModule({
  declarations: [
    EquipmentsComponent,
    EquipmentTabsComponent,
    EquipmentDataComponent,
    EquipmentSerialNumbersComponent,
    EquipmentSuppliersComponent,
    EquipmentAccessoriesComponent,
    EquipmentAlternativesComponent,
    EquipmentSerialNumbersTabsComponent,
    EquipmentSerialNumbersDataComponent,
    EquipmentSerialNumbersHistoryComponent,
    EquipmentSerialNumbersQRcodeComponent,
    EquipmentInspectionsComponent,
    EquipmentContentsComponent,
    EquipmentQrCodesComponent,
    EquipmentStockComponent,
    EquipmentFilesComponent,
    EquipmentImagesComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ROUTES,
    SharedModule,
    pgDatePickerModule,
    pgTimePickerModule,
    FilterPanelModule,
    WidgetInfoModule,
    TreeFoldersModule,
    QRCodeModule,
    jqxDragDropModule,
    MatTabsModule,
    EquipmentsCatalogModule,
    ExcelModule,
    NgxMaskModule.forRoot()
  ],
  providers: [GridService, EqTabEventService]
})
export class EquipmentsModule {}
