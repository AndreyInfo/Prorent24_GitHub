import { Routes, RouterModule } from "@angular/router";

import { VehiclesComponent } from "./vehicles.component";
import { VehicleFormComponent } from "./vehicle-form/vehicle-form.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: VehiclesComponent
      },
      {
        path: "create",
        component: VehicleFormComponent,
        data: {
          title: "Добавить транспорт"
        }
      },
      {
        path: "edit/:id",
        component: VehicleFormComponent,
        data: {
          title: "Редактирование транспорта"
        }
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
