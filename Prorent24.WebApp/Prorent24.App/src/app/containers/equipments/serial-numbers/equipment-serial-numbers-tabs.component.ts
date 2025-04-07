import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

enum SerialNumberTabsEnum {
  data,
  qrcode
}

@Component({
  selector: "app-equipment-serial-numbers-tabs",
  templateUrl: "./equipment-serial-numbers-tabs.component.html"
})
export class EquipmentSerialNumbersTabsComponent implements OnInit {
  id: any;
  parentId: any;
  tabs: Array<string>;

  constructor(private route: ActivatedRoute) {
    this.id = this.route.snapshot.params.id;
    this.parentId = this.route.parent.snapshot.params.id;

    this.tabs = [SerialNumberTabsEnum[SerialNumberTabsEnum.data]];
  }

  ngOnInit(): void {
    if (this.id) {
      this.tabs.push(SerialNumberTabsEnum[SerialNumberTabsEnum.qrcode]);
    }
  }
}
