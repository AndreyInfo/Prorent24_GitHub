import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { HttpService } from "@core/http.service";
import { EquipmentsEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { SetType } from "@shared/enums/set-type.enum";
import { EquipmentModel } from "@models/equipments/equipment.model";
import { SupplyType } from "@shared/enums/supply-type.enum";
import { EqTabEventService } from './eq-tab-event.service';

enum EquipmentTabsEnum {
  data,
  serialNumbers,
  contents,
  accessories,
  alternatives,
  suppliers,
  inspections,
  qrcode,
  stock,
  files,
  images
}

@Component({
  selector: "app-equipment-tabs",
  templateUrl: "./equipment-tabs.component.html"
})
export class EquipmentTabsComponent implements OnInit {
  id: any;
  data: EquipmentModel;
  tabs: Array<string>;

  constructor(private http: HttpService, private route: ActivatedRoute, public eqTabEventService: EqTabEventService) {
    this.id = this.route.snapshot.params.id;
    this.tabs = [EquipmentTabsEnum[EquipmentTabsEnum.data]];
  }

  ngOnInit(): void {
    if (this.id) {
     

      this.http
        .get(StringExt.Format(EquipmentsEndpoints.single, this.id))
        .subscribe(data => (this.data = data), null, () => {
          let eq_type: SetType = this.data.setType;
          this.buildTabs(eq_type);
        });
    }

    this.eqTabEventService.changeEmitted$.subscribe(type => {
      if(this.id){
        this.buildTabs(type);
      }
    })
  }

  private buildTabs(type: any){
    this.tabs = [EquipmentTabsEnum[EquipmentTabsEnum.data]];
    let additional_tabs: Array<string> = new Array<string>();
    switch (type) {
      case SetType.item:
        additional_tabs = [
          EquipmentTabsEnum[EquipmentTabsEnum.serialNumbers],
          EquipmentTabsEnum[EquipmentTabsEnum.accessories],
          EquipmentTabsEnum[EquipmentTabsEnum.alternatives],
          EquipmentTabsEnum[EquipmentTabsEnum.files],
          EquipmentTabsEnum[EquipmentTabsEnum.images]
          /* EquipmentTabsEnum[EquipmentTabsEnum.suppliers], */
          /* EquipmentTabsEnum[EquipmentTabsEnum.inspections], */
          /* EquipmentTabsEnum[EquipmentTabsEnum.qrcode] */
        ];
        break;
      case SetType.case:
        additional_tabs = [
          EquipmentTabsEnum[EquipmentTabsEnum.serialNumbers],
          EquipmentTabsEnum[EquipmentTabsEnum.contents],
          EquipmentTabsEnum[EquipmentTabsEnum.accessories],
          EquipmentTabsEnum[EquipmentTabsEnum.alternatives],
          EquipmentTabsEnum[EquipmentTabsEnum.files],
          EquipmentTabsEnum[EquipmentTabsEnum.images]
          /*  EquipmentTabsEnum[EquipmentTabsEnum.suppliers], */
          /*  EquipmentTabsEnum[EquipmentTabsEnum.inspections], */
          /* EquipmentTabsEnum[EquipmentTabsEnum.qrcode] */
        ];
        break;
      case SetType.kit:
        additional_tabs = [
          EquipmentTabsEnum[EquipmentTabsEnum.contents],
          EquipmentTabsEnum[EquipmentTabsEnum.accessories],
          EquipmentTabsEnum[EquipmentTabsEnum.alternatives],
          EquipmentTabsEnum[EquipmentTabsEnum.files],
          EquipmentTabsEnum[EquipmentTabsEnum.images]
          /* EquipmentTabsEnum[EquipmentTabsEnum.suppliers], */
          /* EquipmentTabsEnum[EquipmentTabsEnum.inspections] */
        ];
        break;
    }

    if (this.data.supplyType.toString() === SupplyType[SupplyType.sale]) {
      additional_tabs.push(EquipmentTabsEnum[EquipmentTabsEnum.stock]);
    }

    this.tabs = [...this.tabs, ...additional_tabs];
  }
}
