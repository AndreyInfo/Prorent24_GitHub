import { Component, OnInit, Input } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { EquipmentsQrCodesEndpoints } from "@endpoints/api.endpoints";
import { ActivatedRoute } from "@angular/router";
import { QrCodeModel } from "@models/equipments/qr-code.model";

@Component({
  selector: "app-equipment-qr-codes",
  templateUrl: "./equipment-qr-codes.component.html"
})
export class EquipmentQrCodesComponent extends GridAbstract<QrCodeModel>
  implements OnInit {
  parentId: any;

  constructor(public gridService: GridService, private route: ActivatedRoute) {
    super(gridService, QrCodeModel, EquipmentsQrCodesEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.loadSubData(this.parentId);
  }
}
