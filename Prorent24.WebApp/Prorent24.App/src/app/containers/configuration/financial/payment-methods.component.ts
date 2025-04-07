import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { PaymentMethodModel } from "@models/configuration/financial/payment-method.model";
import { ConfigFinancialPaymentMethodsEndpoints } from "@endpoints";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-payment-methods",
  templateUrl: "./payment-methods.component.html"
})
export class PaymentMethodsComponent extends GridAbstract<PaymentMethodModel>
  implements OnInit {
  paymentMethods: Array<any>;

  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(
      gridService,
      PaymentMethodModel,
      ConfigFinancialPaymentMethodsEndpoints
    );
  }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
