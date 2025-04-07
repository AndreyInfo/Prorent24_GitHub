import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { ProjectTypeModel } from "@models/configuration/settings/project-type.model";
import { GridService } from "@services/grid.service";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { ConfigSettingsProjectTypesEndpoints } from "@endpoints";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-project-types",
  templateUrl: "./project-types.component.html"
})
export class ProjectTypesComponent extends GridAbstract<ProjectTypeModel>
  implements OnInit {
  invoiceMoments: Array<any>;
  documentTemplates: any = Array<any>();
  letterheads: Array<any>;
  additionalConditions: Array<any>;

  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService,
    private customService: CustomDirectoryService
  ) {
    super(gridService, ProjectTypeModel, ConfigSettingsProjectTypesEndpoints);
  }

  ngOnInit(): void {
    super.ngOnInit();

    this.customService
      .getInvoiceMoments()
      .subscribe(data => (this.invoiceMoments = data));

    this.customService.getDocumentTemplates().subscribe(data => {
      this.documentTemplates = this.groupBy(data, x => x["typeName"]);
    });

    this.customService.getLetterheads().subscribe(data => {
      this.letterheads = data;
    });

    this.customService.getAdditionalConditions().subscribe(data => {
      this.additionalConditions = data;
    });

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }

  groupBy(x, f): any {
    return x.reduce(
      (r, v, i, a, k = f(v)) => ((r[k] || (r[k] = [])).push(v), r),
      {}
    );
  }
}
