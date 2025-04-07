import { Routes, RouterModule } from "@angular/router";

import { ProjectsComponent } from "./projects.component";
import { ProjectTabsComponent } from "./project-tabs.component";
import { ProjectFormDataComponent } from "./data/project-form-data.component";
import { ProjectEquipmentsComponent } from "./equipments/project-equipments.component";
import { ProjectFinanceComponent } from "./finance/project-finance.component";
import { ProjectStaffAndVehiclesComponent } from "./staff-and-vehicles/project-staff-and-vehicles.component";
import { ProjectAdditionalCostComponent } from "./additional-cost/project-additional-cost.component";
import { ProjectSubleaseComponent } from "./sublease/project-sublease.component";
import { ProjectPlanningComponent } from "./planning/project-planning.component";
import { ProjectFinanceDocumentComponent } from "./finance/project-finance-document.component";
import { ProjectShortageComponent } from "./shortage/project-shortage.component";

const routes: Routes = [
	{
		path: "",
		children: [
			{
				path: "",
				component: ProjectsComponent
			},
			{
				path: "create",
				component: ProjectTabsComponent,
				children: [
					{ path: "", redirectTo: "data" },
					{
						path: "data",
						component: ProjectFormDataComponent,
						data: {
							title: "Новый проект"
						}
					}
				]
			},
			{
				path: "edit/:id",
				component: ProjectTabsComponent,
				children: [
					{ path: "", redirectTo: "data" },
					{
						path: "data",
						component: ProjectFormDataComponent,
						data: {
							title: "Редактировать проект"
						}
					},
					{
						path: "equipments",
						component: ProjectEquipmentsComponent
					},
					{
						path: "staff_and_vehicles",
						component: ProjectStaffAndVehiclesComponent
					},
					{
						path: "additional_cost",
						component: ProjectAdditionalCostComponent
					},
					{
						path: "finance",
						component: ProjectFinanceComponent
					},
					{
						path: "finance/documents/:type/:id",
						component: ProjectFinanceDocumentComponent
					},
					{
						path: "sublease",
						component: ProjectSubleaseComponent
					},
					{
						path: "shortage",
						component: ProjectShortageComponent
					},
					{
						path: "planning",
						component: ProjectPlanningComponent
					}
				]
			}
		]
	}
];

export const ROUTES: any = RouterModule.forChild(routes);
