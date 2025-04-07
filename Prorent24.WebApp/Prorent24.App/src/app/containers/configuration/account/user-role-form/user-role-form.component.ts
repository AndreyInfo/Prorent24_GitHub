import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";
import { ConfigAccountUserRolesEndpoints } from "@endpoints";
import { UserRolePermissionsModel } from "@models/configuration/account/user-role-permission.model";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-user-role-form",
  templateUrl: "./user-role-form.component.html"
})
export class UserRoleFormComponent implements OnInit {
  private id: string;
  entity: UserRolePermissionsModel = new UserRolePermissionsModel();
  constructor(
    private toggler: PagesToggleService,
    private http: HttpService,
    private activateRoute: ActivatedRoute,
    private router: Router
  ) {
    this.id = this.activateRoute.snapshot.params.id;
  }

  ngOnInit(): void {
    if (this.id) {
      this.http
        .get(StringExt.Format(ConfigAccountUserRolesEndpoints.single, this.id))
        .subscribe(data =>{
          this.entity = data;
        });
    } else {
      this.http
        .get(StringExt.Format(ConfigAccountUserRolesEndpoints.schema))
        .subscribe(data => (this.entity.modulePermissions = data));
    }

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }

  entityWithoutFunctionPermissions():any{
    if(this.entity.modulePermissions){
      return this.entity.modulePermissions.filter(x=>x.functionPermissions.length < 1);
    }
    else{
      return null;
    }
  }

  entityWithFunctionPermissions(){
    if(this.entity.modulePermissions){
      return this.entity.modulePermissions.filter(x=>x.functionPermissions.length > 0);
    }
    else{
      return null;
    }
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      if (this.id) {
        this.http
          .post(
            StringExt.Format(ConfigAccountUserRolesEndpoints.single, this.id),
            this.entity
          )
          .subscribe(null, null, () => {
            this.router.navigate(["/configuration/account/user-roles"]);
          });
      } else {
        this.http
          .post(ConfigAccountUserRolesEndpoints.root, this.entity)
          .subscribe(null, null, () => {
            this.router.navigate(["/configuration/account/user-roles"]);
          });
      }
    }
  }
}
