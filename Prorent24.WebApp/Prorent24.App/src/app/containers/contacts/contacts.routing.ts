import { Routes, RouterModule } from "@angular/router";

import { ContactsComponent } from "./contacts.component";
import { ContactTabsComponent } from "./contact-tabs.component";
import { ContactFormDataComponent } from "./contact-form/contact-form-data.component";
import { ContactFormPersonalContactComponent } from "./contact-form/contact-form-personal-contact.component";
import { ContactFormPaymentComponent } from "./contact-form/contact-form-payment.component";
import { ContactFormDigitalInvoicingComponent } from "./contact-form/contact-form-digital-invoicing.component";
import { ContactPersonalContactsComponent } from "./contact-personal-contacts.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: ContactsComponent
      },
      {
        path: "create",
        component: ContactTabsComponent,
        children: [
          { path: "", redirectTo: "data" },
          {
            path: "data",
            component: ContactFormDataComponent,
            data: {
              title: "Новый контакт"
            }
          }
        ]
      },
      {
        path: "edit/:id",
        component: ContactTabsComponent,
        children: [
          { path: "", redirectTo: "data" },
          {
            path: "data",
            component: ContactFormDataComponent,
            data: {
              title: "Редактирование контакта"
            }
          },
          {
            path: "personalContacts",
            component: ContactPersonalContactsComponent,
          },
          {
            path: "personalContacts/create",
            component: ContactFormPersonalContactComponent
          },
          {
            path: "personalContacts/edit/:id",
            component: ContactFormPersonalContactComponent
          },
          {
            path: "payments",
            component: ContactFormPaymentComponent
          },
          {
            path: "digitalInvoicing",
            component: ContactFormDigitalInvoicingComponent
          }
        ]
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
