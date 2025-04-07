import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { EquipmentsAccessoriesEndpoints } from "@endpoints";
import { AccsessoryModel } from "@models/equipments/accessory.model";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-equipment-accessories",
  templateUrl: "./equipment-accessories.component.html"
})
export class EquipmentAccessoriesComponent extends GridAbstract<AccsessoryModel>
  implements OnInit {
  parentId: any;

  constructor(
    private toggler: PagesToggleService,
    private gridService: GridService,
    private route: ActivatedRoute
  ) {
    super(gridService, AccsessoryModel, EquipmentsAccessoriesEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.loadSubData(this.parentId);

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });
  }

  addRow(data: any): void {
    let accsessory: any = {
      accessoryId: data.id
    };

    this.grid.upsertRow(accsessory, this.parentId).then(data => {
      let res: any[] = this.getRowsByCellValue(
        "accessoryId",
        accsessory.accessoryId
      );

      if (res.length === 0) {
        this.dataGrid.addrow(null, data);
      } else {
        this.dataGrid.updaterow(res[0].boundindex, data);
      }
    });
  }
}
