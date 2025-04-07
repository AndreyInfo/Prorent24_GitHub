import { Component, OnInit, AfterViewInit, Injector } from "@angular/core";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { ProjectFinancialModel } from "@models/project/project-financial.model";
import { ProjectFinancialCategoryModel } from "@models/project/project-financial-category.model";
import {
  ProjectsEndpoints,
  ProjectFinancialEndpoints,
  ProjectFinancialCategoryEndpoints,
  DocumentsEndpoints
} from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { FormAbstract } from "@abstractions/form.abstraction";
import { Entity } from "@shared/enums/entity.enum";
import { ProjectFinancialCategoryType } from "@shared/enums/project-financial-category-type.enum";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { FinancialModel } from "@models/configuration/financial/financial.model";
import { ActivatedRoute } from "@angular/router";
import { DocumentTemplateTypeEnum } from "@shared/enums/document-template-type.enum";
import { HttpParams } from "@angular/common/http";

@Component({
  selector: "app-project-finance",
  templateUrl: "./project-finance.component.html"
})
export class ProjectFinanceComponent extends FormAbstract<ProjectFinancialModel>
  implements OnInit, AfterViewInit {
  entityType = Entity.maintenance;

  financialModel: FinancialModel = new FinancialModel();

  invoiceMoments: Array<any> = new Array<any>();
  additionalCondition: Array<any> = new Array<any>();

  projectFinancialCategories: ProjectFinancialCategoryModel[];

  // rental
  rentalCategory: ProjectFinancialCategoryModel;
  rentalEquipmentGroups: ProjectFinancialCategoryModel[];

  // sale
  saleCategory: ProjectFinancialCategoryModel;
  saleEquipmentGroups: ProjectFinancialCategoryModel[];

  // different
  crewCategory: ProjectFinancialCategoryModel;
  transportCategory: ProjectFinancialCategoryModel;
  additionalCosts: ProjectFinancialCategoryModel;
  insuranse: ProjectFinancialCategoryModel = new ProjectFinancialCategoryModel();
  totalExcludeVat: ProjectFinancialCategoryModel;

  swowRentEquipmentGroups: boolean = false;
  swowSaleEquipmentGroups: boolean = false;
  vatSchemes: Array<any>;

  docType: any = DocumentTemplateTypeEnum;
  documents: Array<any> = new Array<any>();

  // splitter
  panelOptions: any = {
    mainSplitter: [] = [
      { size: '75%', collapsible: false },
      { size: '25%', collapsible: true }
    ]
  };

  constructor(
    private toggler: PagesToggleService,
    private route: ActivatedRoute,
    private injector: Injector,
    private customDirectory: CustomDirectoryService
  ) {
    super(
      injector,
      Entity.maintenance,
      ProjectFinancialModel,
      ProjectFinancialEndpoints,
      true
    );

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    super.ngOnInit();

    this.customDirectory
      .getInvoiceMoments()
      .subscribe(data => (this.invoiceMoments = data));

    this.customDirectory
      .getAdditionalConditions()
      .subscribe(data => (this.additionalCondition = data));

    this.customDirectory
      .getVatSchemes()
      .subscribe(data => (this.vatSchemes = data));

    // get financial categories for calculate
    this.http
      .getT<ProjectFinancialCategoryModel[]>(
        StringExt.Format(ProjectFinancialCategoryEndpoints.root, this.id)
      )
      .subscribe(result => {
        this.projectFinancialCategories = result;

        // rental
        this.rentalCategory = this.projectFinancialCategories.find(
          x => x.category === ProjectFinancialCategoryType.rental && !x.parentId
        );
        this.rentalEquipmentGroups = this.projectFinancialCategories.filter(
          x =>
            x.category === ProjectFinancialCategoryType.rental && x.parentId > 0
        );
        // this.rentalCategory.total = this.calculateTotal(this.rentalCategory.revenue, this.rentalCategory.discount);

        // sale
        this.saleCategory = this.projectFinancialCategories.find(
          x => x.category === ProjectFinancialCategoryType.sale && !x.parentId
        );
        this.saleEquipmentGroups = this.projectFinancialCategories.filter(
          x =>
            x.category === ProjectFinancialCategoryType.sale && x.parentId > 0
        );

        // different
        this.crewCategory = this.projectFinancialCategories.find(
          x => x.category === ProjectFinancialCategoryType.crew
        );
        this.transportCategory = this.projectFinancialCategories.find(
          x => x.category === ProjectFinancialCategoryType.transport
        );
        this.additionalCosts = this.projectFinancialCategories.find(
          x => x.category === ProjectFinancialCategoryType.additionalCosts
        );
        this.insuranse = this.projectFinancialCategories.find(
          x => x.category === ProjectFinancialCategoryType.insuranse
        );

        if (this.rentalCategory.total > 0) {
          this.insuranse.percentTotal = parseFloat(
            ((this.insuranse.total / this.rentalCategory.total) * 100).toFixed(
              2
            )
          );
        }
        this.totalExcludeVat = this.projectFinancialCategories.find(
          x => x.category === ProjectFinancialCategoryType.totalExcludeVat
        );
      });

    this.customDirectory
      .getFinancialSetting()
      .subscribe(data => (this.financialModel = data));

    this.http
      .get(StringExt.Format(ProjectsEndpoints.single, this.id) + "/documents")
      .subscribe(data => (this.documents = data));

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });
  }

  ngAfterViewInit(): void {
    if (this.id) {
      this.onDataLoadComplete.subscribe((data: any) => {
        setTimeout(() => {
          let addittionalCondition = this.additionalCondition.find(
            (f: any) => f.id == data.additionalConditionId
          );

          if (addittionalCondition) {
            this.entity.addittionalCondition = addittionalCondition.text;
          }
        });
      });
    }
  }

  calculateFinancialByType(type: ProjectFinancialCategoryType): void {
    switch (type) {
      case ProjectFinancialCategoryType.rental: {
        this.sendForRecount(this.rentalCategory);
        break;
      }
      case ProjectFinancialCategoryType.sale: {
        break;
      }
      case ProjectFinancialCategoryType.crew: {
        break;
      }
      case ProjectFinancialCategoryType.transport: {
        break;
      }
      case ProjectFinancialCategoryType.additionalCosts: {
        break;
      }
      case ProjectFinancialCategoryType.insuranse: {
        break;
      }
      case ProjectFinancialCategoryType.totalExcludeVat: {
        break;
      }
    }
  }

  sendForRecount(model: ProjectFinancialCategoryModel): void {
    model.discount = model.discount > 0 ? model.discount : 0;
    this.http
      .postT<ProjectFinancialCategoryModel>(
        StringExt.Format(
          ProjectFinancialCategoryEndpoints.single,
          this.id,
          model.id
        ),
        model
      )
      .subscribe(result => {
        if (model.category !== ProjectFinancialCategoryType.insuranse) {
          model = result;
        }
      });
  }

  calculateTotal(revenue: number, discount: number): number {
    let sumDiscount: number = (revenue * discount) / 100;
    let total: number = revenue - sumDiscount;
    return parseFloat(total.toFixed(2));
  }

  calculateByDiscount(model: ProjectFinancialCategoryModel): void {
    let sumDiscount: number = (model.revenue * model.discount) / 100;
    let total: number = parseFloat((model.revenue - sumDiscount).toFixed(2));
    model.total = total;
    model.profit = total - model.estimatedCosts;
    this.sendForRecount(model);
  }

  calculateByTotal(model: ProjectFinancialCategoryModel): void {
    model.total = model.total ?  model.total : 0;
    let sumByProcent: number = model.revenue / 100;
    let procentFromTotal: number = model.total / sumByProcent;
    model.discount = parseFloat((100 - procentFromTotal).toFixed(2));
    model.profit = model.total - model.estimatedCosts;
    this.sendForRecount(model);
  }

  calculateInsuranse(model: ProjectFinancialCategoryModel): void {
    let sum: number = (this.rentalCategory.total * model.percentTotal) / 100;
    model.revenue = parseFloat(sum.toFixed(2));
    model.total = model.revenue;
    this.sendForRecount(model);
  }

  onChangeAdditionalCondition(id: number): void {
    let _additionalConditions: any = this.additionalCondition.find(
      f => f.id === id
    );

    this.entity.addittionalCondition = _additionalConditions
      ? _additionalConditions.text
      : "";
  }

  onChangeFinancialVat($event: any): void {
    if ($event) {
      this.http
        .postT<ProjectFinancialCategoryModel>(
          StringExt.Format(ProjectFinancialEndpoints.root, this.id),
          this.entity
        )
        .subscribe((result: any) => {
          this.entity.totalIncVat = result.totalIncVat;
          this.onChangeModel.emit(result);
        });
    }
  }

  createDocument(type: DocumentTemplateTypeEnum): void {
    let typeStr: string = DocumentTemplateTypeEnum[type];

    let docId: number;
    this.http
      .post(
        StringExt.Format(DocumentsEndpoints.create, typeStr),
        null,
        new HttpParams().set("projectId", this.parentId)
      )
      .subscribe(
        (data: any) => {
          if (type != DocumentTemplateTypeEnum.invoice) {
            this.router.navigate(
              [`finance/documents/${data.documentType}/${data.id}`],
              {
                relativeTo: this.activateRoute.parent
              }
            );
          } else if (type === DocumentTemplateTypeEnum.invoice) {
            docId = data.invoiceId;
          }
        },
        null,
        () => {
          if (type === DocumentTemplateTypeEnum.invoice) {
            this.router.navigate([`../invoices/edit/${docId}`]);
          }
        }
      );
  }
}
