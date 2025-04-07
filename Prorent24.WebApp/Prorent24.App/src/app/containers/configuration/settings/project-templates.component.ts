import { Component, OnInit } from "@angular/core";
import { ProjectTemplateModel } from "@models/configuration/settings/project-template.model";
import { GridService } from "@services/grid.service";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { ConfigSettingsProjectTemplatesEndpoints } from "@endpoints";

@Component({
  selector: "app-project-templates",
  templateUrl: "./project-templates.component.html",
  styles: []
})
export class ProjectTemplatesComponent
  extends GridAbstract<ProjectTemplateModel>
  implements OnInit {
  constructor(public gridService: GridService) {
    super(
      gridService,
      ProjectTemplateModel,
      ConfigSettingsProjectTemplatesEndpoints
    );
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
