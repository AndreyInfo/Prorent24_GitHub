import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

enum SubleaseTabsEnum {
  data,
  equipments
}

@Component({
  selector: "app-sublease-tabs",
  templateUrl: "./sublease-tabs.component.html"
})
export class SubleaseTabsComponent implements OnInit {
  id: any;
  tabs: Array<string>;

  constructor(private route: ActivatedRoute) {
    this.id = this.route.snapshot.params.id;
    this.tabs = [SubleaseTabsEnum[SubleaseTabsEnum.data]];
  }

  ngOnInit(): void {
    if (this.id) {
      let additional_tabs: Array<string> = [
        SubleaseTabsEnum[SubleaseTabsEnum.equipments]
      ];

      this.tabs = [...this.tabs, ...additional_tabs];
    }
  }
}
