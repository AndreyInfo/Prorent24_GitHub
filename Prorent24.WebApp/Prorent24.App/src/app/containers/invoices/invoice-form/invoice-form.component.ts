import { Component, OnInit, Injector } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Observable } from "rxjs";
import { FormAbstract } from "@abstractions/form.abstraction";
import { SearchService } from "@services/search.service";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { Entity } from "@shared/enums/entity.enum";
import { InvoiceModel } from "@models/invoice/invoice.model";
import { InvoicesEndpoints, ContactsEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { ContactModel } from "@models/contacts/contact.model";
import { DocumentTemplateTypeEnum } from "@shared/enums/document-template-type.enum";
import { InvoiceDocumentsEndpoints, InvoiceLinesEndpoints } from "@endpoints";
import { InvoiceLine } from "@models/invoice/invoice-line.model";
import { FinancialModel } from "@models/configuration/financial/financial.model";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { DomSanitizer } from "@angular/platform-browser";
import { InvoiceState } from "@shared/enums/invoice-state.enum";
import { take } from 'rxjs/operators';
import { X } from '@angular/cdk/keycodes';

@Component({
  selector: "app-invoice-form",
  templateUrl: "./invoice-form.component.html"
})
export class InvoiceFormComponent extends FormAbstract<InvoiceModel>
  implements OnInit {
  financialModel: FinancialModel = new FinancialModel();
  newInvoiceLine: InvoiceLine = new InvoiceLine();
  documentTemplateType: any = DocumentTemplateTypeEnum.invoice;
  invoiceState: any = InvoiceState;

  contacts: Array<any> = new Array<any>();
  vatClasses: Array<any> = new Array<any>();
  vatSchemas: Array<any> = new Array<any>();
  ledgers: Array<any> = new Array<any>();

  docViewer: boolean;
  docUrl: string;

  statuses = {

  }

  // splitter
  // panelOptions: any = {
  //   mainSplitter: [] = [
  //     { size: "100%", collapsible: false },
  //     { size: 300, min: 300, max: 500, collapsible: true }
  //   ]
  // };

  panelOptions: any = {
		headerSplitter:[] = [{size: "100%", collapsible: false}],
		mainSplitter: [] = [{ size: "70%", collapsible: false }, { size: "30%" }],
		nestedSplitter: [] = [
			{ size: "100%", collapsible: false },
			{ size: 260, min: 260, max: 350 }
		]
	};

  constructor(
    private toggler: PagesToggleService,
    private injector: Injector,
    private search: SearchService,
    private customDirectory: CustomDirectoryService,
    public sanitizer: DomSanitizer
  ) {
    super(injector, Entity.invoices, InvoiceModel, InvoicesEndpoints);
  }

  ngOnInit(): void {
    super.ngOnInit();

    this.docViewer = this.id ? true : false;

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });

    this.customDirectory
      .getVatClasses()
      .subscribe(data => (this.vatClasses = data));

    this.customDirectory
      .getVatSchemes()
      .subscribe(data => (this.vatSchemas = data));

    this.customDirectory
      .getFinancialSetting()
      .subscribe(data => (this.financialModel = data), null, () => {
        this.initDefaultValue();
      });

    this.customDirectory.getLedgers().subscribe(data => (this.ledgers = data));

    this.entity.url = this.sanitizer.bypassSecurityTrustResourceUrl(
      this.http.getPath(
        StringExt.Format(InvoiceDocumentsEndpoints.invoice, this.id)
      )
    );

    this.onDataLoadComplete.pipe(take(1)).subscribe((data: InvoiceModel) => {
      this.contacts.push(data.client);
    })
  }

  //#region on change client
  onChangeContactSearch(event: any): void {
    let search_val: string = event.target.value;
    if (search_val.length > 2 && search_val.length < 6) {
      this.search.contacts(search_val).subscribe((data: Array<any>) => {
        if (data.length > 0) {
          data.forEach((e: any, index: number) => {
            let u: any = this.contacts.find(f => f.id === e.id);
            if (u) {
              data.splice(index, 1);
            }
          });
          this.contacts = [...this.contacts, ...data];
        }
      });
    }
  }

  onSelectedContact(): void {
   // let personId =  this.contacts.find(x=>x.id == this.entity.clientId).contactId;
    // let personId: any = this.contacts.find(
    //   (f: any) => f.id == this.entity.clientId
    // ).contactId;

    this.http
      .getT<ContactModel>(StringExt.Format(ContactsEndpoints.single, this.entity.clientId))
      .subscribe(res => {
        this.entity.client = res;
      });
  }
  //#endregion

  //#region invoice line
  onAddInvoiceLine(form: NgForm): void {
    if (form.valid) {
      let _invoiceLinesTemp: Array<InvoiceLine> = new Array<InvoiceLine>();
      _invoiceLinesTemp = [..._invoiceLinesTemp, ...this.entity.invoiceLines];
      _invoiceLinesTemp.push(form.value);

      this.http
        .post(InvoiceLinesEndpoints.calculate_total, {
          invoiceLines: _invoiceLinesTemp,
          vatSchemeId: this.entity.vatSchemeId
        })
        .subscribe(
          (data: InvoiceModel) => {
            this.entity.vat = data.vat;
            this.entity.priceExcludeVAT = data.priceExcludeVAT;
            this.entity.priceIncludeVAT = data.priceIncludeVAT;
            this.entity.totalNewPrice = data.totalNewPrice;
          },
          null,
          () => {
            if (this.id) {
              this.http
                .post(
                  StringExt.Format(InvoiceLinesEndpoints.root, this.id),
                  form.value
                )
                .subscribe((data: InvoiceLine) =>
                  this.entity.invoiceLines.push(data)
                );
            } else {
              this.entity.invoiceLines.push(form.value);
            }

            this.newInvoiceLine = new InvoiceLine();
          }
        );
    }
  }

  removeInvoiceLine(index: number, id?: number): void {
    this.entity.invoiceLines.splice(index, 1);
    this.calculateTotal();

    if (id) {
      this.http
        .post(StringExt.Format(InvoiceLinesEndpoints.delete, this.id, id))
        .subscribe();
    }
  }

  changeInvoiceLine(
    invoiceLine: InvoiceLine,
    withoutCalculate?: boolean
  ): void {
    if (invoiceLine.id) {
      this.http
        .post(
          StringExt.Format(
            InvoiceLinesEndpoints.single,
            this.id,
            invoiceLine.id
          ),
          invoiceLine
        )
        .subscribe();
    }

    if (!withoutCalculate) {
      this.calculateTotal();
    }
  }
  //#endregion

  calculateTotal(): void {
    this.http
      .post(InvoiceLinesEndpoints.calculate_total, {
        invoiceLines: this.entity.invoiceLines,
        vatSchemeId: this.entity.vatSchemeId
      })
      .subscribe((data: InvoiceModel) => {
        this.entity.vat = data.vat;
        this.entity.priceExcludeVAT = data.priceExcludeVAT;
        this.entity.priceIncludeVAT = data.priceIncludeVAT;
        this.entity.totalNewPrice = data.totalNewPrice;
      });
  }

  //#region submit invoice
  onSubmitInvoiceForm(form: NgForm): void {
    debugger
    if (form.valid) {
      let isNew: boolean;

      form.value.invoiceLines = this.entity.invoiceLines;
      let submitReq: Observable<any>;
      if (this.id) {
        submitReq = this.http.post(
          StringExt.Format(InvoicesEndpoints.single, this.id),
          form.value
        );
      } else {
        isNew = true;
        submitReq = this.http.post(InvoicesEndpoints.root, form.value);
      }

      submitReq.subscribe(
        (data: InvoiceModel) => (this.entity.id = data.id),
        null,
        () => {
          if (isNew) {
            this.router.navigate([`./edit/${this.entity.id}`], {
              relativeTo: this.activateRoute.parent
            });
          } else {
            /* this.router.navigate(["."], {
              relativeTo: this.activateRoute.parent
            }); */
          }
        }
      );
    }
  }
  //#endregion

  downloadFile(): void {
    this.http
      .uploadFile(StringExt.Format(InvoiceDocumentsEndpoints.invoice, this.id))
      .toPromise()
      .then(res => {
        var blob = new Blob([res], { type: "application/pdf" });
        var link = document.createElement("a");
        link.href = window.URL.createObjectURL(blob);
        link.download = "invoice.pdf";
        link.click();
      });
  }

  private initDefaultValue(): void {
    this.entity.vatClassId =
      this.entity.vatClassId && this.entity.vatClassId > 0
        ? this.entity.vatClassId
        : this.financialModel.defaultVatClassId;
    this.entity.vatSchemeId =
      this.entity.vatSchemeId && this.entity.vatSchemeId > 0
        ? this.entity.vatSchemeId
        : this.financialModel.defaultVatSchemeId;
  }
}
