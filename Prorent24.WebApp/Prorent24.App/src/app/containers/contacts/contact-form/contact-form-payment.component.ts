import { Component, OnInit, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { FormAbstract } from "@abstractions/form.abstraction";
import { ContactPaymentModel } from "@models/contacts/contact-payment.model";
import { Entity } from "@shared/enums/entity.enum";
import { ContactPaymentEndpoints } from "@endpoints/api.endpoints";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { BooleanEnum } from "@shared/enums/boolean.enum";

@Component({
  selector: "app-contact-form-payment",
  templateUrl: "./contact-form-payment.component.html"
})
export class ContactFormPaymentComponent
  extends FormAbstract<ContactPaymentModel>
  implements OnInit {
  constructor(
    private injector: Injector,
    private customDirectory: CustomDirectoryService,
    private route: ActivatedRoute
  ) {
    super(
      injector,
      Entity.contacts,
      ContactPaymentModel,
      ContactPaymentEndpoints,
      true
    );

    this.parentId = this.route.parent.snapshot.params.id;
  }

  booleanType = BooleanEnum;
  invoiceMoments: Array<any>;
  paymentConditions: Array<any>;
  vatSchemes: Array<any>;

  ngOnInit(): void {
    super.ngOnInit();

    this.customDirectory
      .getInvoiceMoments()
      .subscribe(data => (this.invoiceMoments = data));

    this.customDirectory
      .getPaymentConditions()
      .subscribe(data => (this.paymentConditions = data));

    this.customDirectory
      .getVatSchemes()
      .subscribe(data => (this.vatSchemes = data));
  }
}
