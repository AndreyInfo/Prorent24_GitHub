import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { GridService } from "@services/grid.service";
import { Entity } from "@shared/enums/entity.enum";
import { MaintenanceInspectionModel } from "@models/maintenance/maintenance-inspection.model";
import { MaintenanceInspectionsEndpoints } from "@endpoints";
import { TableEntity } from '@shared/enums/table-entity.enum';

@Component({
  selector: "app-maintenance-inspections",
  templateUrl: "./maintenance-inspections.component.html"
})
export class MaintenanceInspectionsComponent
  extends GridAbstract<MaintenanceInspectionModel>
  implements OnInit {
  entityType: Entity = Entity.inspections;
  tableEntityType: TableEntity = TableEntity.inspections;
  users: Array<any> = new Array<any>();

  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(
      gridService,
      MaintenanceInspectionModel,
      MaintenanceInspectionsEndpoints
    );
  }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });
    }

    setDefaultColumns(): void {
        let data: any = {};
        data.moduleEntity = this.tableEntityType;
        data.columns = null;

        this.gridService.setGridColumn(data).then(() => {
            this.loadData();
        });
    }

}
