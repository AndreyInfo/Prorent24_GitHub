import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { CustomDirectoryService } from "@services/custom-directory.service";

import { FinancialModel } from "@models/configuration/financial/financial.model";
import { HttpService } from "@core/http.service";
import { ConfigFinancialSettingsEndpoints } from "@endpoints";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { DirectoryService } from "@services/directory.service";

@Component({
  selector: "app-financial",
  templateUrl: "./financial.component.html"
})
export class FinancialComponent implements OnInit {
  entity: FinancialModel = new FinancialModel();

  vatClasses: Array<any>;
  vatSchemes: Array<any>;
  companyTypes: Array<any>;

  constructor(
    private toggler: PagesToggleService,
    private http: HttpService,
    private customDirectory: CustomDirectoryService,
    private directory: DirectoryService
  ) {}

  ngOnInit(): void {
    this.customDirectory
      .getVatClasses()
      .subscribe(data => (this.vatClasses = data));

    this.customDirectory
      .getVatSchemes()
      .subscribe(data => (this.vatSchemes = data));

    this.directory
      .getCompanyTypes()
      .subscribe(data => (this.companyTypes = data));

    this.http
      .get(ConfigFinancialSettingsEndpoints.root)
      .subscribe(data => (this.entity = data));

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.http
        .post(ConfigFinancialSettingsEndpoints.root, form.value)
        .subscribe();
    }
  }
}
