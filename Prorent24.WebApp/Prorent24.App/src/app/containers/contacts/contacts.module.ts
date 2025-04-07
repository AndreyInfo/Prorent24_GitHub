import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ROUTES } from "./contacts.routing";

import { SharedModule } from "@shared/shared.module";
import { GridService } from "@services/grid.service";

import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { MatTabsModule } from "@angular/material/tabs";
import { NgxMaskModule } from "ngx-mask";

import { WidgetInfoModule } from "@components/widget-info/widget-info.module";
import { FilterPanelModule } from "@components/filter/filter-panel.module";
import { TreeFoldersModule } from "@components/tree-folders/tree-folders.module";

import { ContactsComponent } from "./contacts.component";
import { ContactTabsComponent } from "./contact-tabs.component";
import { ContactPersonalContactsComponent } from "./contact-personal-contacts.component";
import { ContactFormDataComponent } from "./contact-form/contact-form-data.component";
import { ContactFormPersonalContactComponent } from "./contact-form/contact-form-personal-contact.component";
import { ContactFormDigitalInvoicingComponent } from "./contact-form/contact-form-digital-invoicing.component";
import { ContactFormPaymentComponent } from "./contact-form/contact-form-payment.component";

@NgModule({
  declarations: [
    ContactsComponent,
    ContactTabsComponent,
    ContactFormDataComponent,
    ContactPersonalContactsComponent,
    ContactFormPersonalContactComponent,
    ContactFormPaymentComponent,
    ContactFormDigitalInvoicingComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ROUTES,
    SharedModule,
    MatTabsModule,
    FilterPanelModule,
    WidgetInfoModule,
    TreeFoldersModule,
    pgDatePickerModule,
    NgxMaskModule.forRoot()
  ],
  exports: [],
  providers: [GridService]
})
export class ContactsModule {}
