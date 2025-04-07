import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./session.routing";
import { TranslateModule } from "@ngx-translate/core";
import { ModalModule } from "ngx-bootstrap";

import { LoginComponent } from "@containers/session/login/login.component";
import { RegisterComponent } from "@containers/session/register/register.component";
import { LogoutComponent } from "./logout/logout.component";

@NgModule({
  imports: [CommonModule, ROUTES, FormsModule, TranslateModule, ModalModule],
  declarations: [LoginComponent, RegisterComponent, LogoutComponent]
})
export class SessionModule {}
