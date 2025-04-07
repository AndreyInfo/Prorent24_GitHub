import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { HttpService } from "@core/http.service";
import { DirectoryService } from "@services/directory.service";
import { ElectronicInvoicingModel } from "@models/configuration/financial/electronic-invoicing.model";
import { ConfigFinancialElectronicInvoicingEndpoints } from "@endpoints";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-electronic-invoicing",
  templateUrl: "./electronic-invoicing.component.html"
})
export class ElectronicInvoicingComponent implements OnInit {
  entity: ElectronicInvoicingModel = new ElectronicInvoicingModel();
  currency: Array<any>;

  constructor(
    private toggler: PagesToggleService,
    private http: HttpService,
    private directory: DirectoryService
  ) {}

  ngOnInit(): void {
    this.directory.getCurrency().subscribe(data => (this.currency = data));

    this.http
      .get(ConfigFinancialElectronicInvoicingEndpoints.root)
      .subscribe(data => (this.entity = data));

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.http
        .post(ConfigFinancialElectronicInvoicingEndpoints.root, form.value)
        .subscribe();
    }
  }
}
