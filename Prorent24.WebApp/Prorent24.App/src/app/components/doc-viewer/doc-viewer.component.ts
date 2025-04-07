import { Component, OnInit, Input, ViewChild } from "@angular/core";
import { DomSanitizer } from "@angular/platform-browser";
import { HttpService } from "@core/http.service";
import { DocumentsEndpoints } from "@endpoints";
import { DocumentTemplateTypeEnum } from "@shared/enums/document-template-type.enum";
import { Entity } from "@shared/enums/entity.enum";
import { StringExt } from "@shared/utils/string.extension";
import { DocFormWidgetComponent } from './doc-form-widget.component';

@Component({
  selector: "rent-doc-viewer",
  templateUrl: "./doc-viewer.component.html"
})
export class DocViewerComponent implements OnInit {
  @ViewChild("docInfoWidget", { static: true }) docInfoWidget: DocFormWidgetComponent;

  @Input() entity: any = {};
  @Input() entityType: Entity;
  @Input() documentId: any;
  @Input() documentTemplateType: DocumentTemplateTypeEnum;

  docUrl: any;

  // splitter
  panelOptions: any = {
    mainSplitter: [] = [
      { size: "100%", collapsible: false },
      { size: 300, min: 300, max: 500, collapsible: true }
    ]
  };

  constructor(private http: HttpService, public sanitizer: DomSanitizer) {}

  ngOnInit(): void {
    this.docUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.http.getPath(
      StringExt.Format(DocumentsEndpoints.getFile, this.documentId)
    ));  
  }
}
