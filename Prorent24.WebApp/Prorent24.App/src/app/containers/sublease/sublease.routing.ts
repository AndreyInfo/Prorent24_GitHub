import { Routes, RouterModule } from "@angular/router";

import { SubleaseComponent } from "./sublease.component";
import { SubleaseTabsComponent } from "./sublease-tabs.component";
import { SubleaseFormDataComponent } from "./data/sublease-form-data.component";
import { SubleaseEquipmentsComponent } from "./sublease-equipments/sublease-equipments.component";
import { ShortageComponent } from "./shortage.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: SubleaseComponent,
        data: {
          title: "Субаренда"
        }
      },
      {
        path: "create",
        component: SubleaseTabsComponent,
        children: [
          { path: "", redirectTo: "data" },
          {
            path: "data",
            component: SubleaseFormDataComponent,
            data: {
              title: "Новая субаренда"
            }
          }
        ]
      },
      {
        path: "edit/:id",
        component: SubleaseTabsComponent,
        children: [
          { path: "", redirectTo: "data" },
          {
            path: "data",
            component: SubleaseFormDataComponent,
            data: {
              title: "Редактирование субаренды"
            }
          },
          {
            path: "equipments",
            component: SubleaseEquipmentsComponent
          }
        ]
      },
      {
        path: "shortage",
        component: ShortageComponent,
        data: {
          title: "Дефицит"
        }
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
