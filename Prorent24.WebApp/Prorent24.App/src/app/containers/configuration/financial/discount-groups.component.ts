import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { ConfigFinancialDiscountGroupsEndpoints } from "@endpoints";
import { DiscountGroupModel } from "@models/configuration/financial/discount-group.model";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-discount-groups",
  templateUrl: "./discount-groups.component.html"
})
export class DiscountGroupsComponent extends GridAbstract<DiscountGroupModel>
  implements OnInit {
  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(
      gridService,
      DiscountGroupModel,
      ConfigFinancialDiscountGroupsEndpoints
    );
  }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
