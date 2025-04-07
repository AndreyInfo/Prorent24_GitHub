import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { StringExt } from "@shared/utils/string.extension";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { HttpService } from "@core/http.service";
import { ConfigFinancialVatSchemesEndpoints } from "@endpoints";
import { ConfigFinancialVatClassesEndpoints } from "@endpoints";
import { VatClassModel } from "@models/configuration/financial/vat-class.model";
import { VatSchemeModel } from "@models/configuration/financial/vat-scheme.model";
import { VatSchemeType } from "@shared/enums/vat-scheme-type.enum";
import { VatClassSchemeRate } from "@models/configuration/financial/vat-class-scheme-rate";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-vat-schemes",
  templateUrl: "./vat-schemes.component.html"
})
export class VatSchemesComponent extends GridAbstract<VatSchemeModel>
  implements OnInit {
  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService,
    private http: HttpService
  ) {
    super(gridService, VatSchemeModel, ConfigFinancialVatSchemesEndpoints);
  }

  vatClasses: VatClassModel[] = [];
  rates: VatClassSchemeRate[] = [];
  fixedRate: VatClassSchemeRate = new VatClassSchemeRate();
  vatReverseCharge: VatClassSchemeRate = new VatClassSchemeRate();

  ngOnInit(): void {
    super.ngOnInit();
    this.aggregateVatClassSchemeRates();

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }

  overrideOnEditRowModal(id: any): void {
    if (id) {
      this.http
        .getT<VatSchemeModel>(
          StringExt.Format(ConfigFinancialVatSchemesEndpoints.single, id)
        )
        .subscribe(data => {
          this.entity = data;
          this.clearSchemeRates();
          this.aggregateVatClassSchemeRates();
          this.entity.type = this.entity.type
            ? this.entity.type
            : VatSchemeType.rates;
          this.rowModal.show();
        });
    } else {
      this.clearSchemeRates();
      this.aggregateVatClassSchemeRates();
      this.rowModal.show();
    }
  }

  clearSchemeRates(): void {
    this.rates = [];
    this.fixedRate = new VatClassSchemeRate();
    this.vatReverseCharge = new VatClassSchemeRate();
  }

  aggregateVatClassSchemeRates(): void {
    this.http
      .get(ConfigFinancialVatClassesEndpoints.root + "/list")
      .subscribe(result => {
        this.vatClasses = result;
        this.entity.vatClassSchemeRates = this.entity.vatClassSchemeRates
          ? this.entity.vatClassSchemeRates
          : [];

        // rate
        for (let i: number = 0; i < this.vatClasses.length; i++) {
          let vatClass: any = this.vatClasses[i];
          let index: number = this.entity.vatClassSchemeRates.findIndex(
            x => x.vatClassId === vatClass.id
          );
          if (index < 0) {
            let classSchemeRate: VatClassSchemeRate = new VatClassSchemeRate();
            classSchemeRate.vatSchemeId = this.entity.id;
            classSchemeRate.type = VatSchemeType.rates;
            classSchemeRate.vatClassId = vatClass.id;
            classSchemeRate.vatClass = vatClass;
            classSchemeRate.rate = 0;
            classSchemeRate.accountingCode = "";
            classSchemeRate.edifactCode = "";
            this.rates.push(classSchemeRate);
          } else {
            this.entity.vatClassSchemeRates[index].vatClass = vatClass;
            this.rates.push(this.entity.vatClassSchemeRates[index]);
          }
        }

        // fixed rate
        let vatClassSchemeFixedRate: any = this.entity.vatClassSchemeRates.find(
          x => x.type === VatSchemeType.fixedRate
        );
        if (vatClassSchemeFixedRate) {
          this.fixedRate = vatClassSchemeFixedRate;
          this.fixedRate.vatSchemeId = this.entity.id;
        } else {
          this.fixedRate = new VatClassSchemeRate();
          this.fixedRate.vatSchemeId = this.entity.id;
          this.fixedRate.type = VatSchemeType.fixedRate;
          this.fixedRate.rate = 0;
          this.fixedRate.accountingCode = "0";
        }

        // vat reverse charge
        let vatClassSchemeReverseChargeRate: any = this.entity.vatClassSchemeRates.find(
          x => x.type === VatSchemeType.vatReverseCharge
        );
        if (vatClassSchemeReverseChargeRate) {
          this.vatReverseCharge = vatClassSchemeReverseChargeRate;
          this.vatReverseCharge.vatSchemeId = this.entity.id;
        } else {
          this.vatReverseCharge = new VatClassSchemeRate();
          this.vatReverseCharge.vatSchemeId = this.entity.id;
          this.vatReverseCharge.type = VatSchemeType.vatReverseCharge;
          this.vatReverseCharge.rate = 0;
          this.vatReverseCharge.accountingCode = "0";
        }
      });
  }

  overrideOnSubmit(form: NgForm): void {
    this.rowModal.hide();
    form.value.vatClassSchemeRates = [];
    form.value.vatClassSchemeRates = this.rates;
    form.value.vatClassSchemeRates.push(this.fixedRate);
    form.value.vatClassSchemeRates.push(this.vatReverseCharge);
    this.onSubmit(form);
  }
}
