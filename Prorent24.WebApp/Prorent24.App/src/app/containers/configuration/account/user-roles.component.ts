import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { ConfigAccountUserRolesEndpoints } from "@endpoints";
import { GridService } from "@services/grid.service";
import { UserRolesModel } from "@models/configuration/account/user-roles.model";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-user-roles",
  templateUrl: "./user-roles.component.html"
})
export class UserRolesComponent extends GridAbstract<UserRolesModel>
  implements OnInit {
  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(gridService, UserRolesModel, ConfigAccountUserRolesEndpoints);
  }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
