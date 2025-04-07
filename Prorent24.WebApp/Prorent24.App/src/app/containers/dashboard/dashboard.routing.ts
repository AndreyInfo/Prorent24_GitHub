import { Routes, RouterModule } from "@angular/router";

import { DashboardComponent } from "./dashboard.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: DashboardComponent
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
