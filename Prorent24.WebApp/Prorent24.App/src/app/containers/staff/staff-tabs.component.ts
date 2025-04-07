import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

enum StaffTabsEnum {
  data,
  rates
}

@Component({
  selector: "app-staff-tabs",
  templateUrl: "./staff-tabs.component.html"
})
export class StaffTabsComponent implements OnInit {
  id: any;
  tabs: Array<string>;

  constructor(private route: ActivatedRoute) {
    this.id = this.route.snapshot.params.id;
    this.tabs = [StaffTabsEnum[StaffTabsEnum.data]];
  }

  ngOnInit(): void {
    if (this.id) {
      let additional_tabs: Array<string> = [StaffTabsEnum[StaffTabsEnum.rates]];

      this.tabs = [...this.tabs, ...additional_tabs];
    }
  }
}
