import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { ConfigFinancialInvoiceMomentsEndpoints } from "@endpoints";
import { InvoiceMomentModel } from "@models/configuration/financial/invoice-moment.model";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-invoice-moments",
  templateUrl: "./invoice-moments.component.html"
})
export class InvoiceMomentsComponent extends GridAbstract<InvoiceMomentModel>
  implements OnInit {
  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(
      gridService,
      InvoiceMomentModel,
      ConfigFinancialInvoiceMomentsEndpoints
    );
  }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
