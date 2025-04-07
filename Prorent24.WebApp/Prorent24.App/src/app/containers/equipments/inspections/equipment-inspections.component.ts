import { Component, OnInit, Input } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { InspectionModel } from "@models/configuration/settings/inspection.model";
import { EquipmentsInspectionsEndpoints } from "@endpoints";

@Component({
  selector: "app-equipment-inspections",
  templateUrl: "./equipment-inspections.component.html"
})
export class EquipmentInspectionsComponent extends GridAbstract<InspectionModel>
  implements OnInit {
  parentId: any;

  constructor(public gridService: GridService, private route: ActivatedRoute) {
    super(gridService, InspectionModel, EquipmentsInspectionsEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.loadSubData(this.parentId);

    this.dataGrid.onBindingcomplete.subscribe(() => {
      this.dataGrid.setcolumnproperty("active", "editable", true);
    });
  }

  saveInspectiosChanges(): void {
    let rows: any = this.dataGrid.getrows();
    rows.forEach(element => {
      let inspectionState: any = {
        equipmentId: element.equipmentId,
        periodicInspectionId: element.periodicInspectionId,
        active: element.active
      };

      this.onSubmitCustomData(inspectionState);
    });
  }

  onChangeActive(event: any): void {
    let args = event.args;
    let columnDataField = args.datafield;
    let rowIndex = args.rowindex;
    let cellValue = args.value;
    let oldValue = args.oldvalue;
  }
}
