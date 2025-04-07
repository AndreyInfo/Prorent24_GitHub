import { Routes, RouterModule } from "@angular/router";

import { ProfileFormComponent } from "./profile-form/profile-form.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "profile",
        component: ProfileFormComponent
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
