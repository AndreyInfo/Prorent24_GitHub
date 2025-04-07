import { Component, OnInit, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { FormAbstract } from "@abstractions/form.abstraction";
import { Entity } from "@shared/enums/entity.enum";
import { ContactPersonModel } from "@models/contacts/contact-person.model";
import { ContactPersonsEndpoints } from "@endpoints";
import { DirectoryService } from "@services/directory.service";
import { CustomDirectoryService } from "@services/custom-directory.service";

@Component({
  selector: "app-contact-form-personal-contact",
  templateUrl: "./contact-form-personal-contact.component.html"
})
export class ContactFormPersonalContactComponent
  extends FormAbstract<ContactPersonModel>
  implements OnInit {

  constructor(
    private injector: Injector,
    private directory: DirectoryService,
    private customDirectory: CustomDirectoryService,
    private route: ActivatedRoute
  ) {
    super(
      injector,
      Entity.contacts,
      ContactPersonModel,
      ContactPersonsEndpoints
    );

    this.parentId = this.route.parent.snapshot.params.id;
  }

  countries: Array<any>;
  salutations: Array<any>;

  ngOnInit(): void {
    super.ngOnInit();

    this.customDirectory
      .getSalutations()
      .subscribe(data => (this.salutations = data));

    this.directory.getCountries().subscribe(data => (this.countries = data));
  }
}
