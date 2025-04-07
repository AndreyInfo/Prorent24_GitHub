import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { ConfigFinancialFactorGroupsEndpoints } from "@endpoints";
import { FactorGroupModel } from "@models/configuration/financial/factor-group.model";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-factor-groups",
  templateUrl: "./factor-groups.component.html"
})
export class FactorGroupsComponent extends GridAbstract<FactorGroupModel>
  implements OnInit {
  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(gridService, FactorGroupModel, ConfigFinancialFactorGroupsEndpoints);
  }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
