import { Routes, RouterModule } from "@angular/router";

import { ConfigurationComponent } from "./configuration.component";
import * as Configuration from "@containers/configuration";

const routes: Routes = [
  {
    path: "",
    component: ConfigurationComponent,
    children: [
      {
        path: "",
        redirectTo: "account/company-details",
        pathMatch: "full"
      },
      {
        path: "account/company-details",
        component: Configuration.CompanyDetailsComponent,
        data: {
          title: "Конфигурация"
        }
      },
      {
        path: "account/licenses",
        component: Configuration.LicensesComponent
      },
      {
        path: "account/payments",
        component: Configuration.PaymentsComponent
      },
      {
        path: "account/user-roles",
        component: Configuration.UserRolesComponent
      },
      {
        path: "account/user-roles/create",
        component: Configuration.UserRoleFormComponent
      },
      {
        path: "account/user-roles/edit/:id",
        component: Configuration.UserRoleFormComponent
      },
      {
        path: "account/extensions",
        component: Configuration.ExtensionsComponent
      },
      {
        path: "settings/time-and-location",
        component: Configuration.TimeAndLocationComponent
      },
      {
        path: "settings/range-numbers",
        component: Configuration.RangeNumbersComponent
      },
      {
        path: "settings/project-types",
        component: Configuration.ProjectTypesComponent
      },
      {
        path: "settings/project-templates",
        component: Configuration.ProjectTemplatesComponent
      },
      {
        path: "settings/inspections",
        component: Configuration.InspectionsComponent
      },
      {
        path: "settings/time-registration",
        component: Configuration.TimeRegistrationComponent
      },
      {
        path: "settings/custom-fields",
        component: Configuration.CustomFieldsComponent
      },
      {
        path: "settings/database",
        component: Configuration.DatabaseComponent
      },
      {
        path: "communication",
        component: Configuration.CommunicationComponent
      },
      {
        path: "communication/document-templates",
        component: Configuration.DocumentTemplatesComponent
      },
      {
        path: "communication/blanks",
        component: Configuration.BlanksComponent
      },
      {
        path: "communication/blanks/create",
        component: Configuration.BlankFormComponent
      },
      {
        path: "communication/blanks/edit/:id",
        component: Configuration.BlankFormComponent
      },
      {
        path: "communication/salutation",
        component: Configuration.SalutationComponent
      },
      {
        path: "discount-groups",
        component: Configuration.DiscountGroupsComponent
      },
      {
        path: "electronic-invoicing",
        component: Configuration.ElectronicInvoicingComponent
      },
      {
        path: "factor-groups",
        component: Configuration.FactorGroupsComponent
      },
      {
        path: "financial",
        component: Configuration.FinancialComponent
      },
      {
        path: "invoice-moments",
        component: Configuration.InvoiceMomentsComponent
      },
      {
        path: "ledger",
        component: Configuration.LedgersComponent
      },
      {
        path: "payment-conditions",
        component: Configuration.PaymentConditionsComponent
      },
      {
        path: "payment-methods",
        component: Configuration.PaymentMethodsComponent
      },
      {
        path: "vat-classes",
        component: Configuration.VatClassesComponent
      },
      {
        path: "vat-schemes",
        component: Configuration.VatSchemesComponent
      },
      {
        path: "additional-conditions",
        component: Configuration.AdditionalConditionsComponent
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
