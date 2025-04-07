import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { AlternativeModel } from "@models/equipments/alternative.model";
import { GridService } from "@services/grid.service";
import { EquipmentAlternativesEndpoints } from "@endpoints";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-equipment-alternatives",
  templateUrl: "./equipment-alternatives.component.html"
})
export class EquipmentAlternativesComponent
  extends GridAbstract<AlternativeModel>
  implements OnInit {
  parentId: any;

  constructor(
    private toggler: PagesToggleService,
    private gridService: GridService,
    private route: ActivatedRoute
  ) {
    super(gridService, AlternativeModel, EquipmentAlternativesEndpoints);

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
    let alternative: any = {
      alternativeId: data.id
    };

    let res: any[] = this.getRowsByCellValue(
      "alternativeId",
      alternative.alternativeId
    );

    if (res.length === 0) {
      this.grid.upsertRow(alternative, this.parentId).then(data => {
        this.tableSource.source.localdata.push(data);
        this.source = new jqx.dataAdapter(this.tableSource.source);
      });
    }
  }
}
