import { Routes, RouterModule } from "@angular/router";

import { TimeRegistrationComponent } from "./time-registration.component";
import { TimeRegistrationFormComponent } from "./time-registration-form/time-registration-form.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: TimeRegistrationComponent
      },
      {
        path: "create",
        component: TimeRegistrationFormComponent
      },
      {
        path: "edit/:id",
        component: TimeRegistrationFormComponent
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
