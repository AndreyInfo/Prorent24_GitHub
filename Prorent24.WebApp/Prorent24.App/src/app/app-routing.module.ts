import { NgModule } from "@angular/core";
import { Routes, RouterModule, PreloadAllModules } from "@angular/router";

import { AuthenticatedGuard } from "@core/authentication.guard";

import { CondensedComponent, BlankComponent } from "./layout";

const routes: Routes = [
  {
    path: "",
    component: CondensedComponent,
    canActivate: [AuthenticatedGuard],
    children: [
      {
        path: "",
        redirectTo: "dashboard",
        pathMatch: "full"
      },
      {
        path: "dashboard",
        loadChildren: () => import('./containers/dashboard/dashboard.module').then(m => m.DashboardModule),
        data: {
          title: "Рабочая панель"
        }
      },
      {
        path: "myschedule",
        loadChildren: () => import('./containers/schedule/schedule.module').then(m => m.ScheduleModule),
        data: {
          title: "Мое расписание"
        }
      },
      {
        path: "warehouse",
        loadChildren: () => import('./containers/warehouse/warehouse.module').then(m => m.WarehouseModule),
        data: {
          title: "Движение оборудования"
        }
      },
      {
        path: "projects",
        loadChildren: () => import('./containers/projects/projects.module').then(m => m.ProjectsModule),
        data: {
          title: "Проекты"
        }
      },
      {
        path: "staff-planner",
        loadChildren:
          () => import('./containers/staff-planner/staff-planner.module').then(m => m.StaffPlannerModule),
        data: {
          title: "Планировщик персонала"
        }
      },
      {
        path: "sublease",
        loadChildren: () => import('./containers/sublease/sublease.module').then(m => m.SubleaseModule),
        data: {
          title: "Субаренда"
        }
      },
      {
        path: "invoices",
        loadChildren: () => import('./containers/invoices/invoices.module').then(m => m.InvoicesModule),
        data: {
          title: "Счета"
        }
      },
      {
        path: "equipments",
        loadChildren:
          () => import('./containers/equipments/equipments.module').then(m => m.EquipmentsModule),
        data: {
          title: "Оборудование"
        }
      },
      {
        path: "contacts",
        loadChildren: () => import('./containers/contacts/contacts.module').then(m => m.ContactsModule),
        data: {
          title: "Контакты"
        }
      },
      {
        path: "staff",
        loadChildren: () => import('./containers/staff/staff.module').then(m => m.StaffModule),
        data: {
          title: "Персонал"
        }
      },
      {
        path: "vehicles",
        loadChildren: () => import('./containers/vehicles/vehicles.module').then(m => m.VehiclesModule),
        data: {
          title: "Транспорт"
        }
      },
      {
        path: "tasks",
        loadChildren: () => import('./containers/tasks/tasks.module').then(m => m.TasksModule),
        data: {
          title: "Задачи"
        }
      },
      {
        path: "time-registration",
        loadChildren:
          () => import('./containers/time-registration/time-registration.module').then(m => m.TimeRegistrationModule),
        data: {
          title: "Регистрация времени"
        }
      },
      {
        path: "maintenance",
        loadChildren:
          () => import('./containers/maintenance/maintenance.module').then(m => m.MaintenanceModule),
        data: {
          title: "Обслуживание"
        }
      },
      {
        path: "configuration",
        loadChildren:
          () => import('./containers/configuration/configuration.module').then(m => m.ConfigurationModule),
        data: {
          title: "Конфигурация"
        }
      },
      {
        path: "account",
        loadChildren: () => import('./containers/account/account.module').then(m => m.AccountModule),
        data: {
          title: "Профиль"
        }
      },
      {
        path: "notifications",
        loadChildren:
          () => import('./containers/notifications/notifications.module').then(m => m.NotificationsModule),
        data: {
          title: "Уведомления"
        }
      }
    ]
  },
  {
    path: "",
    component: BlankComponent,
    loadChildren: () => import('./containers/session/session.module').then(m => m.SessionModule)
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
