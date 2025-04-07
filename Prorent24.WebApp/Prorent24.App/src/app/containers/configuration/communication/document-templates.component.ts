import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { ConfigCommunicationDocTemplatesEndpoints } from "@endpoints";
import { DocTemplateModel } from "@models/configuration/communication/doc-template.model";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-document-templates",
  templateUrl: "./document-templates.component.html",
  styles: []
})
export class DocumentTemplatesComponent extends GridAbstract<DocTemplateModel>
  implements OnInit {
  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(
      gridService,
      DocTemplateModel,
      ConfigCommunicationDocTemplatesEndpoints
    );
  }

  ngOnInit(): void {
    super.ngOnInit();

    this.dataGrid.onBindingcomplete.subscribe(() => {
      this.dataGrid.addgroup("typeName");
      this.dataGrid.expandallgroups();
    });

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
