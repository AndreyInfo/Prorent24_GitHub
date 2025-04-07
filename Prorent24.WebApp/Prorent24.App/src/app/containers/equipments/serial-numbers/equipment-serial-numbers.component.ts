import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { EquipmentsSerialNumbersEndpoints } from "@endpoints";
import { SerialNumberModel } from "@models/equipments/serial-number.model";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-equipment-serial-numbers",
  templateUrl: "./equipment-serial-numbers.component.html"
})
export class EquipmentSerialNumbersComponent
  extends GridAbstract<SerialNumberModel>
  implements OnInit {
  parentId: any;

  constructor(public gridService: GridService, private route: ActivatedRoute) {
    super(gridService, SerialNumberModel, EquipmentsSerialNumbersEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.loadSubData(this.parentId);
  }
}
