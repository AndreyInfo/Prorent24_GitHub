import { Component, OnInit, Input, EventEmitter, Output } from "@angular/core";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { DocumentTemplateTypeEnum } from "@shared/enums/document-template-type.enum";
import { OpenKitsAndCasesTypeEnum } from "@shared/enums/open-kit-cases-type.enum";
import { Entity } from "@shared/enums/entity.enum";

@Component({
  selector: "rent-document-configuration",
  templateUrl: "./document-configuration.component.html"
})
export class DocumentConfigurationComponent implements OnInit {
  _entity: any = {};

  @Input() entityType: Entity;
  @Input() documentTemplateType: DocumentTemplateTypeEnum;
  @Input()
  set entity(value: any) {
		this._entity = value;
		this.CheckedChange.emit(this._entity);
	}
  get entity(): any {
    return this._entity;
  }

  @Output() CheckedChange = new EventEmitter();
  @Output() onTemplateUpdateEvent = new EventEmitter<any>();
  templateUpdate(value: any): void {
    this.onTemplateUpdateEvent.emit(value);
  }

  openKitsAndCasesTypes: any = OpenKitsAndCasesTypeEnum;
  templates: [] = [];
  letterheads: [] = [];
  paymentConditions: [] = [];

  constructor(private customDirectory: CustomDirectoryService) {}

  ngOnInit(): void {
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
}
