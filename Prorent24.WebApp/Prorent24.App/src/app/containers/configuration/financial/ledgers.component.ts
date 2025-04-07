import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { ConfigFinancialLedgersEndpoints } from "@endpoints";
import { LedgerModel } from "@models/configuration/financial/ledger.model";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-ledgers",
  templateUrl: "./ledgers.component.html"
})
export class LedgersComponent extends GridAbstract<LedgerModel>
  implements OnInit {
  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(gridService, LedgerModel, ConfigFinancialLedgersEndpoints);
  }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
