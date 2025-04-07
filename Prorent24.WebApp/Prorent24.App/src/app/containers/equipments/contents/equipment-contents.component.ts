import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { EquipmentContentsEndpoints } from "@endpoints";
import { EquipmentContentModel } from "@models/equipments/equipment-content.model";

@Component({
  selector: "app-equipment-contents",
  templateUrl: "./equipment-contents.component.html"
})
export class EquipmentContentsComponent extends GridAbstract<EquipmentContentModel>
  implements OnInit {
  parentId: any;

  constructor(
    private toggler: PagesToggleService,
    private gridService: GridService,
    private route: ActivatedRoute
  ) {
    super(gridService, EquipmentContentModel, EquipmentContentsEndpoints);

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
    let content: any = {
      contentId: data.id
    };

    this.grid.upsertRow(content, this.parentId).then(data => {
      let res: any[] = this.getRowsByCellValue(
        "contentId",
        content.contentId
      );

      if (res.length === 0) {
        this.dataGrid.addrow(null, data);
      } else {
        this.dataGrid.updaterow(res[0].boundindex, data);
      }
    });
  }
}
