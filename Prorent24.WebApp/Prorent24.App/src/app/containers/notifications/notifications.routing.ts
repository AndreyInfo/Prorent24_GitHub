import { Routes, RouterModule } from "@angular/router";

import { NotificationsComponent } from "./notifications.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: NotificationsComponent
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
