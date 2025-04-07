import { Routes, RouterModule } from "@angular/router";

import { StaffComponent } from "./staff.component";
import { StaffTabsComponent } from "./staff-tabs.component";
import { StaffFormComponent } from "./staff-form/staff-form.component";
import { StaffRatesComponent } from "./staff-rates/staff-rates.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: StaffComponent
      },
      {
        path: "create",
        component: StaffTabsComponent,
        children: [
          { path: "", redirectTo: "data" },
          {
            path: "data",
            component: StaffFormComponent
          }
        ]
      },
      {
        path: "edit/:id",
        component: StaffTabsComponent,
        children: [
          { path: "", redirectTo: "data" },
          {
            path: "data",
            component: StaffFormComponent
          },
          {
            path: "rates",
            component: StaffRatesComponent
          }
        ]
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
