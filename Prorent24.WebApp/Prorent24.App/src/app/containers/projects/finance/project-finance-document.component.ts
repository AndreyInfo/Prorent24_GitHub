import { Component, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { DocViewerComponent } from "@components/doc-viewer/doc-viewer.component";
import { StringExt } from '@shared/utils/string.extension';
import { DocumentsEndpoints } from '@endpoints';
import { DomSanitizer } from '@angular/platform-browser';
import { HttpService } from '@core/http.service';

@Component({
  selector: "app-project-finance-document",
  templateUrl: "./project-finance-document.component.html"
})
export class ProjectFinanceDocumentComponent implements OnInit {
  @ViewChild("docViewer", { static: true }) docViewer: DocViewerComponent;

  entity: any = {};
  documentTemplateType: string;
  documentId: number;

  // splitter
  panelOptions: any = {
    mainSplitter: [] = [
      { size: "100%", collapsible: false },
      { size: 300, min: 300, max: 500, collapsible: true }
    ]
  };

  constructor(
    private toggler: PagesToggleService,
    private activateRoute: ActivatedRoute,
    public sanitizer: DomSanitizer,
    private http: HttpService,
  ) {
    this.documentTemplateType = this.activateRoute.snapshot.params.type;
    this.documentId = this.activateRoute.snapshot.params.id;
  }

  ngOnInit(): void {
    this.entity.url = this.sanitizer.bypassSecurityTrustResourceUrl(
      this.http.getPath(
        StringExt.Format(DocumentsEndpoints.getFile, this.documentId)
      )
    );

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });
  }

  updateDocument(): void {
    this.docViewer.docInfoWidget.submitDocumentForm();
  }
}
