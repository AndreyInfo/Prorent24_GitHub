import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { GridService } from "@services/grid.service";
import { Entity } from "@shared/enums/entity.enum";
import { ProjectModel } from "@models/project/project.model";
import { ProjectsEndpoints } from "@endpoints";
import { TableEntity } from "@shared/enums/table-entity.enum";

@Component({
  selector: "app-projects",
  templateUrl: "./projects.component.html"
})
export class ProjectsComponent extends GridAbstract<ProjectModel>
  implements OnInit {
  entityType: Entity = Entity.projects;
  tableEntityType: TableEntity = TableEntity.projects;

  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(gridService, ProjectModel, ProjectsEndpoints);
  }

  ngOnInit(): void {
    this.loadData(this.filter);

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
