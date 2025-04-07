import { Component, OnInit } from "@angular/core";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { Entity } from "@shared/enums/entity.enum";
import { GridService } from "@services/grid.service";
import { RepairModel } from "@models/maintenance/repair.model";
import { MaintenanceRepairEndpoints } from "@endpoints";
import { TableEntity } from '@shared/enums/table-entity.enum';

@Component({
  selector: "app-maintenance-repair",
  templateUrl: "./maintenance-repair.component.html"
})
export class MaintenanceRepairComponent extends GridAbstract<RepairModel>
  implements OnInit {
  entityType: Entity = Entity.repairs;
  tableEntityType: TableEntity = TableEntity.repairs;
  users: Array<any> = new Array<any>();

  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(gridService, RepairModel, MaintenanceRepairEndpoints);
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
