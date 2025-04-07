import { Routes, RouterModule } from "@angular/router";

import { EquipmentsComponent } from "./equipments.component";
import { EquipmentTabsComponent } from "./equipment-tabs.component";
import { EquipmentDataComponent } from "./data/equipment-form-data.component";
import { EquipmentAccessoriesComponent } from "./accessories/equipment-accessories.component";
import { EquipmentAlternativesComponent } from "./alternatives/equipment-alternatives.component";
import { EquipmentQrCodesComponent } from "./qr-codes/equipment-qr-codes.component";
import { EquipmentInspectionsComponent } from "./inspections/equipment-inspections.component";
import { EquipmentSuppliersComponent } from "./suppliers/equipment-suppliers.component";
import { EquipmentSerialNumbersComponent } from "./serial-numbers/equipment-serial-numbers.component";
import { EquipmentSerialNumbersDataComponent } from "./serial-numbers/equipment-serial-numbers-data.component";
import { EquipmentSerialNumbersTabsComponent } from "./serial-numbers/equipment-serial-numbers-tabs.component";
import { EquipmentSerialNumbersQRcodeComponent } from "./serial-numbers/equipment-serial-numbers-qrcode.component";
import { EquipmentContentsComponent } from "./contents/equipment-contents.component";
import { EquipmentStockComponent } from './stock/equipment-stock.component';
import { EquipmentFilesComponent } from './files/equipment-files.component';
import { EquipmentImagesComponent } from './images/equipment-images.component';

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: EquipmentsComponent
      },
      {
        path: "create",
        component: EquipmentTabsComponent,
        children: [
          { path: "", redirectTo: "data" },
          {
            path: "data",
            component: EquipmentDataComponent,
            data: {
              title: "Новое оборудование"
            }
          }
        ]
      },
      {
        path: "edit/:id",
        component: EquipmentTabsComponent,
        children: [
          { path: "", redirectTo: "data" },
          {
            path: "data",
            component: EquipmentDataComponent,
            data: {
              title: "Редактирование оборудования"
            }
          },
          {
            path: "serialNumbers",
            component: EquipmentSerialNumbersComponent
          },
          {
            path: "serialNumbers/create",
            component: EquipmentSerialNumbersTabsComponent,
            children: [
              { path: "", redirectTo: "data" },
              {
                path: "data",
                component: EquipmentSerialNumbersDataComponent
              }
            ]
          },
          {
            path: "serialNumbers/edit/:id",
            component: EquipmentSerialNumbersTabsComponent,
            children: [
              { path: "", redirectTo: "data" },
              {
                path: "data",
                component: EquipmentSerialNumbersDataComponent
              },
              {
                path: "qrcode",
                component: EquipmentSerialNumbersQRcodeComponent
              }
            ]
          },
          {
            path: "contents",
            component: EquipmentContentsComponent
          },
          {
            path: "accessories",
            component: EquipmentAccessoriesComponent
          },
          {
            path: "alternatives",
            component: EquipmentAlternativesComponent
          },
          {
            path: "suppliers",
            component: EquipmentSuppliersComponent
          },
          {
            path: "inspections",
            component: EquipmentInspectionsComponent
          },
          {
            path: "qrcode",
            component: EquipmentQrCodesComponent
          },
          {
            path: "stock",
            component: EquipmentStockComponent
          },
          {
            path: "files",
            component: EquipmentFilesComponent
          },
          {
            path: "images",
            component: EquipmentImagesComponent
          }
        ]
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
