import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { ProjectAdditionaCost } from "@models/project/project-additional-cost.model";
import { Entity } from "@shared/enums/entity.enum";
import { ProjectAdditionalCostsEndpoints } from "@endpoints";
import { CustomDirectoryService } from "@services/custom-directory.service";

@Component({
  selector: "app-project-additional-cost",
  templateUrl: "./project-additional-cost.component.html"
})
export class ProjectAdditionalCostComponent
  extends GridAbstract<ProjectAdditionaCost>
  implements OnInit {
  entityType: Entity = Entity.projects;
  vatTypes: Array<any>;

  constructor(
    public gridService: GridService,
    private customDirectory: CustomDirectoryService,
    private route: ActivatedRoute
  ) {
    super(gridService, ProjectAdditionaCost, ProjectAdditionalCostsEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.loadSubData(this.parentId);

    this.customDirectory
      .getVatClasses()
      .subscribe(data => (this.vatTypes = data));
  }
}
