import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { ConfigFinancialPaymentConditionsEndpoints } from "@endpoints";
import { PaymentConditionModel } from "@models/configuration/financial/payment-condition.model";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-payment-conditions",
  templateUrl: "./payment-conditions.component.html"
})
export class PaymentConditionsComponent
  extends GridAbstract<PaymentConditionModel>
  implements OnInit {
  paymentMethods: Array<any>;

  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService,
    private customDirectory: CustomDirectoryService
  ) {
    super(
      gridService,
      PaymentConditionModel,
      ConfigFinancialPaymentConditionsEndpoints
    );
  }

  ngOnInit(): void {
    super.ngOnInit();

    this.customDirectory
      .getPaymentMethods()
      .subscribe(data => (this.paymentMethods = data));

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
