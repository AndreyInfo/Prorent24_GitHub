import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { GridService } from "@services/grid.service";
import { Entity } from "@shared/enums/entity.enum";
import { SubleaseModel } from "@models/sublease/sublease.model";
import { SubleaseEndpoints } from "@endpoints";
import { TableEntity } from '@shared/enums/table-entity.enum';

@Component({
  selector: "app-sublease",
  templateUrl: "./sublease.component.html"
})
export class SubleaseComponent extends GridAbstract<SubleaseModel>
  implements OnInit {
  entityType: Entity = Entity.subhire;
  tableEntityType: TableEntity = TableEntity.subhire;
  users: Array<any> = new Array<any>();

  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(gridService, SubleaseModel, SubleaseEndpoints);
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

    clearFilterByPeriod() {
        this.filterPeriod.fromDate = null;
        this.filterPeriod.toDate = null;

        this.onChangedFilterByPeriod();
    }

}
