import { Component, OnInit } from "@angular/core";
import { GridService } from "@services/grid.service";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { ActivatedRoute } from "@angular/router";
import { ProjectShortageEndpoints } from "@endpoints/api.endpoints";
import { ProjectAdditionaCost } from "@models/project/project-additional-cost.model";
import { Entity } from "@shared/enums/entity.enum";

@Component({
	selector: "app-shortage",
	templateUrl: "./project-shortage.component.html"
})
export class ProjectShortageComponent extends GridAbstract<ProjectAdditionaCost>
	implements OnInit {
	entityType: Entity = Entity.projects;
	vatTypes: Array<any>;

	constructor(public gridService: GridService, private route: ActivatedRoute) {
		super(gridService, ProjectAdditionaCost, ProjectShortageEndpoints);

		this.parentId = this.route.parent.snapshot.params.id;
	}
	ngOnInit(): void {
		this.loadSubData(this.parentId);
	}
}
