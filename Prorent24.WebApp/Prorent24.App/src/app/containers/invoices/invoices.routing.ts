import { Routes, RouterModule } from "@angular/router";

import { InvoicesComponent } from "./invoices.component";
import { InvoiceFormComponent } from "./invoice-form/invoice-form.component";

const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: InvoicesComponent
      },
      {
        path: "create",
        component: InvoiceFormComponent,
        data: {
          title: "Новый счет"
        }
      },
      {
        path: "edit/:id",
        component: InvoiceFormComponent,
        data: {
          title: "Редактирование счета"
        }
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
