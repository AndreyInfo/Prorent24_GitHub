import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./configuration.routing";
import { NgxMaskModule } from "ngx-mask";

import { SharedModule } from "@shared/shared.module";
import { ColorPickerModule } from "ngx-color-picker";
import { pgCollapseModule } from "@ui-components/collapse";
import { CustomFormsModule } from "ng2-validation"; 

// service
import { GridService } from "@services/grid.service";
import { CustomDirectoryService } from "@services/custom-directory.service";

// components
import { ConfigurationComponent } from "./configuration.component";
import { ConfigurationMenuComponent } from "./configuration-menu.component";
import * as Configuration from "@containers/configuration";

@NgModule({
  imports: [
    CommonModule,
    ROUTES,
    FormsModule,
    SharedModule,
    ColorPickerModule,
    pgCollapseModule,
    NgxMaskModule.forRoot(),
    CustomFormsModule
  ],
  declarations: [
    ConfigurationMenuComponent,
    ConfigurationComponent,
    Configuration.CompanyDetailsComponent,
    Configuration.LicensesComponent,
    Configuration.PaymentsComponent,
    Configuration.UserRolesComponent,
    Configuration.UserRoleFormComponent,
    Configuration.ExtensionsComponent,
    Configuration.TimeAndLocationComponent,
    Configuration.RangeNumbersComponent,
    Configuration.ProjectTypesComponent,
    Configuration.ProjectTemplatesComponent,
    Configuration.InspectionsComponent,
    Configuration.TimeRegistrationComponent,
    Configuration.CustomFieldsComponent,
    Configuration.DatabaseComponent,
    Configuration.CommunicationComponent,
    Configuration.DocumentTemplatesComponent,
    Configuration.BlanksComponent,
    Configuration.BlankFormComponent,
    Configuration.SalutationComponent,
    Configuration.AdditionalConditionsComponent,
    Configuration.DiscountGroupsComponent,
    Configuration.ElectronicInvoicingComponent,
    Configuration.FactorGroupsComponent,
    Configuration.FinancialComponent,
    Configuration.InvoiceMomentsComponent,
    Configuration.LedgersComponent,
    Configuration.PaymentConditionsComponent,
    Configuration.PaymentMethodsComponent,
    Configuration.VatClassesComponent,
    Configuration.VatSchemesComponent
  ],
  providers: [GridService, CustomDirectoryService]
})
export class ConfigurationModule {}
