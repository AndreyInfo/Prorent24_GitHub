import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { LogoutComponent } from "./logout/logout.component";
import { RegisterComponent } from "./register/register.component";
import { SystemEmptyGuard } from "@core/system.empty.guard";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "login",
        component: LoginComponent,
        canActivate: [SystemEmptyGuard]
      },
      {
        path: "logout",
        component: LogoutComponent
      },
      {
        path: "register",
        component: RegisterComponent,
        canActivate: [SystemEmptyGuard]
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
