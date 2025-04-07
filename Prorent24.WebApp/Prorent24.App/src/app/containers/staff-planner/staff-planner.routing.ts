import { Routes, RouterModule } from "@angular/router";

import { StaffPlannerComponent } from "./staff-planner.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: StaffPlannerComponent
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
