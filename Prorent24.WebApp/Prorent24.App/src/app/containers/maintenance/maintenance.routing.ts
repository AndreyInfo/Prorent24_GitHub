import { Routes, RouterModule } from "@angular/router";

import { MaintenanceComponent } from "./maintenance.component";
import { MaintenanceRepairComponent } from "./repair/maintenance-repair.component";
import { MaintenanceInspectionsComponent } from "./inspections/maintenance-inspections.component";
import { MaintenanceRepairFormComponent } from "./repair/repair-form/maintenance-repair-form.component";
import { MaintenanceInspectionFormComponent } from "./inspections/inspection-form/maintenance-inspection-form.component";

const routes: Routes = [
  {
    path: "",
    component: MaintenanceComponent,
    children: [
      { path: "", redirectTo: "repair" },
      {
        path: "repair",
        component: MaintenanceRepairComponent,
        data: {
          title: "Ремонт"
        }
      },
      {
        path: "repair/create",
        component: MaintenanceRepairFormComponent
      },
      {
        path: "repair/edit/:id",
        component: MaintenanceRepairFormComponent
      },
      {
        path: "inspections",
        component: MaintenanceInspectionsComponent,
        data: {
          title: "Проверки"
        }
      },
      {
        path: "inspections/create",
        component: MaintenanceInspectionFormComponent
      },
      {
        path: "inspections/edit/:id",
        component: MaintenanceInspectionFormComponent
      }
    ]
  }
];

export const ROUTES: any = RouterModule.forChild(routes);
