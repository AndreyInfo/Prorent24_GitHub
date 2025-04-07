import { Routes, RouterModule } from "@angular/router";

import { ScheduleComponent } from "./schedule.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: ScheduleComponent
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
