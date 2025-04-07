import { Component, OnInit, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { FormAbstract } from "@abstractions/form.abstraction";
import { ContactDigitalInvoiceModel } from "@models/contacts/contact-digital-invoice.model";
import { ContactDigitalInvoiceEndpoints } from "@endpoints";
import { Entity } from "@shared/enums/entity.enum";

@Component({
  selector: "app-contact-form-digital-invoicing",
  templateUrl: "./contact-form-digital-invoicing.component.html"
})
export class ContactFormDigitalInvoicingComponent
  extends FormAbstract<ContactDigitalInvoiceModel>
  implements OnInit {
  constructor(private injector: Injector, private route: ActivatedRoute) {
    super(
      injector,
      Entity.contacts,
      ContactDigitalInvoiceModel,
      ContactDigitalInvoiceEndpoints,
      true
    );

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
