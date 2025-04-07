import { Component, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

enum ContactTabsEnum {
  data,
  personalContacts,
  payments,
  digitalInvoicing
}

@Component({
  selector: "app-contact-tabs",
  templateUrl: "./contact-tabs.component.html"
})
export class ContactTabsComponent implements OnInit {
  id: any;
  tabs: Array<string>;

  constructor(private route: ActivatedRoute) {
    this.id = this.route.snapshot.params.id;
    this.tabs = [ContactTabsEnum[ContactTabsEnum.data]];
  }

  ngOnInit(): void {
    if (this.id) {
      let additional_tabs: Array<string> = [
        ContactTabsEnum[ContactTabsEnum.personalContacts],
        ContactTabsEnum[ContactTabsEnum.payments],
        ContactTabsEnum[ContactTabsEnum.digitalInvoicing]
      ];

      this.tabs = [...this.tabs, ...additional_tabs];
    }
  }
}
