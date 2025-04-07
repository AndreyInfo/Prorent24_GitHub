import { Component, OnInit, Input } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { ContactPersonsEndpoints } from "@endpoints";
import { GridService } from "@services/grid.service";
import { ContactPersonModel } from "@models/contacts/contact-person.model";

@Component({
  selector: "app-contact-personal-contacts",
  templateUrl: "./contact-personal-contacts.component.html"
})
export class ContactPersonalContactsComponent
  extends GridAbstract<ContactPersonModel>
  implements OnInit {

  constructor(public gridService: GridService, private route: ActivatedRoute) {
    super(gridService, ContactPersonModel, ContactPersonsEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.loadSubData(this.parentId);
  }
}
