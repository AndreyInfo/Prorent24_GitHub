import { Component, OnInit, Input } from "@angular/core";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { OpenKitsAndCasesTypeEnum } from "@shared/enums/open-kit-cases-type.enum";
import { Entity } from "@shared/enums/entity.enum";
import { DocumentTemplateTypeEnum } from "@shared/enums/document-template-type.enum";
import { DocumentsEndpoints } from "@endpoints";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";
import { MessageService } from "@ui-components/notification/notification.service";
import { Subscription } from "rxjs";

@Component({
  selector: "rent-doc-form-widget",
  templateUrl: "./doc-form-widget.component.html"
})
export class DocFormWidgetComponent implements OnInit {
  @Input() entity: any = {};
  @Input() entityType: Entity;
  @Input() documentId: any;
  @Input() documentTemplateType: DocumentTemplateTypeEnum;

  openKitsAndCasesTypes: any = OpenKitsAndCasesTypeEnum;
  templates: [] = [];
  letterheads: [] = [];
  paymentConditions: [] = [];

  constructor(
    private http: HttpService,
    private customDirectory: CustomDirectoryService,
    private notification: MessageService
  ) {}

  ngOnInit(): void {
    if (!this.entity.id) {
      this.http
        .get(StringExt.Format(DocumentsEndpoints.info, this.documentId))
        .subscribe((data: any) => (this.entity = data));
    }

    this.customDirectory
      .getDocumentTemplatesByType(this.documentTemplateType)
      .subscribe(data => {
        this.templates = data;
      });

    this.customDirectory.getLetterheads().subscribe(data => {
      this.letterheads = data;
    });

    this.customDirectory.getPaymentConditions().subscribe(data => {
      this.paymentConditions = data;
    });
  }

  submitDocumentForm(): void {
    this.http
      .post(
        StringExt.Format(DocumentsEndpoints.info, this.documentId),
        this.entity
      )
      .subscribe((data: any) => (this.entity = data), null, () => {
        this.notification.create("success", "Документ обновлен", {
          Position: "top-right",
          Style: "flip",
          Duration: 3000
        });
      });
  }
}
