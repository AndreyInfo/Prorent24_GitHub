import { Routes, RouterModule } from "@angular/router";

import { TasksComponent } from "./tasks.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: TasksComponent
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
