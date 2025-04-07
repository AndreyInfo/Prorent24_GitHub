import { Component, OnInit } from "@angular/core";
import { ConditionModel } from "@models/configuration/financial/condition.model";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { ConfigFinancialConditionsEndpoints } from "@endpoints";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-additional-conditions",
  templateUrl: "./additional-conditions.component.html"
})
export class AdditionalConditionsComponent extends GridAbstract<ConditionModel>
  implements OnInit {
  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(gridService, ConditionModel, ConfigFinancialConditionsEndpoints);
  }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
