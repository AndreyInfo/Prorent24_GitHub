import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { ActivatedRoute } from "@angular/router";
import { QrCodeModel } from "@models/equipments/qr-code.model";
import { EquipmentsSerialNumbersQrCodeEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { JqxGridModel } from "@shared/models/jqx-grid.model";
import { NgForm } from "@angular/forms";

@Component({
  selector: "app-equipment-serial-numbers-qrcode",
  templateUrl: "./equipment-serial-numbers-qrcode.component.html"
})
export class EquipmentSerialNumbersQRcodeComponent
  extends GridAbstract<QrCodeModel>
  implements OnInit {
  parentId: any;
  sub_parentId: any;
  constructor(public gridService: GridService, private route: ActivatedRoute) {
    super(gridService, QrCodeModel, EquipmentsSerialNumbersQrCodeEndpoints);

    this.sub_parentId = this.route.parent.parent.snapshot.params.id;
    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.grid
      .getDataGridByUrl(
        StringExt.Format(
          this.grid.endpoint.root,
          this.sub_parentId,
          this.parentId
        )
      )
      .then((data: JqxGridModel) => {
        this.tableSource = data;
        this.source = new jqx.dataAdapter(this.tableSource.source);
      });
  }

  submitQrCode(form: NgForm): void {
    if (form.valid) {
      let url: string = form.value.id
        ? StringExt.Format(this.grid.endpoint.single, this.sub_parentId, this.parentId, form.value.id)
        : StringExt.Format(this.grid.endpoint.root, this.sub_parentId, this.parentId);

      this.grid.upsertRowByUrl(form.value, url).then(data => {
        if (form.value.id) {
          this.dataGrid.updaterow(this.selected_entity.rowIndex, data);
        } else {
          this.dataGrid.addrow(null, data);
        }
        this.entity = new QrCodeModel();
        this.rowModal.hide();
      });
    }
  }
}
