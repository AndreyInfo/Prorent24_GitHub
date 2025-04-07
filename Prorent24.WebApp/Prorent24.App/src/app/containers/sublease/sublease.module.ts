import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./sublease.routing";

import { MatTabsModule } from "@angular/material/tabs";

import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { pgTimePickerModule } from "@ui-components/time-picker/timepicker.module";

import { SharedModule } from "@shared/shared.module";
import { WidgetInfoModule } from "@components/widget-info/widget-info.module";
import { FilterPanelModule } from "@components/filter/filter-panel.module";
import { EquipmentsCatalogModule } from "@components/equipment-catalog/equipment-catalog.module";

import { GridService } from "@services/grid.service";
import { SearchService } from "@services/search.service";

import { SubleaseComponent } from "./sublease.component";
import { SubleaseTabsComponent } from "./sublease-tabs.component";
import { SubleaseEquipmentsComponent } from "./sublease-equipments/sublease-equipments.component";
import { SubleaseFormDataComponent } from "./data/sublease-form-data.component";
import { ShortageComponent } from "./shortage.component";

@NgModule({
  declarations: [
    SubleaseComponent,
    SubleaseTabsComponent,
    SubleaseEquipmentsComponent,
    SubleaseFormDataComponent,
    ShortageComponent
  ],
  imports: [
    CommonModule,
    ROUTES,
    FormsModule,
    SharedModule,
    FilterPanelModule,
    WidgetInfoModule,
    MatTabsModule,
    pgDatePickerModule,
    pgTimePickerModule,
    EquipmentsCatalogModule
  ],
  exports: [],
  providers: [GridService, SearchService]
})
export class SubleaseModule {}
