import { Routes, RouterModule } from "@angular/router";
import { WarehouseComponent } from "./warehouse.component";
import { WarehouseBookingComponent } from "./booking/booking.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: WarehouseComponent
      },
      {
        path: "booking/:id",
        component: WarehouseBookingComponent,
        data: {
          title: "Перемещение оборудования"
        }
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
