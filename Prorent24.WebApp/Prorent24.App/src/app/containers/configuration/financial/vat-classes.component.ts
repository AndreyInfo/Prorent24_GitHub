import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { ConfigFinancialVatClassesEndpoints } from "@endpoints";
import { VatClassModel } from "@models/configuration/financial/vat-class.model";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-vat-classes",
  templateUrl: "./vat-classes.component.html"
})
export class VatClassesComponent extends GridAbstract<VatClassModel>
  implements OnInit {
  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(gridService, VatClassModel, ConfigFinancialVatClassesEndpoints);
  }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
