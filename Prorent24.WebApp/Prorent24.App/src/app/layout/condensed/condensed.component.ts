import { Component, OnInit, HostListener, HostBinding, ViewEncapsulation, ViewChildren, TemplateRef } from "@angular/core";
import { RootLayout } from "../root/root.component";

declare var pg: any;

import {
  IdentityStoreActions,
  IdentityStoreSelectors,
  NotificationsStoreActions,
  NotificationsStoreSelectors
} from "@store";
import { Observable } from "rxjs";
import { NotificationModel } from "@models/notifications/notification.model";
import { TooltipDirective } from 'ng2-tooltip-directive';

@Component({
  selector: "condensed-layout",
  templateUrl: "./condensed.component.html",
  styleUrls: ["./condensed.component.scss"],
  encapsulation: ViewEncapsulation.None
})
export class CondensedComponent extends RootLayout implements OnInit {

  @ViewChildren(TooltipDirective) tooltipDirective;

  profileMenuContent;

  current_year: number = new Date(Date.now()).getFullYear();

  lastNotifications$: Observable<Array<NotificationModel>>;

  profileOptions = {
    'placement': 'right',
    'show-delay': 500,
    'width': 220,
    'hideDelayAfterClick': 1000000000,
    'position': {
      'top': 108,
      'left': 176
    },
    'trigger': 'click',
    'pointerEvents': 'auto'
  }

  menuLinks: Array<any> = new Array<any>();
  menuCollection = [
    {
      id: 1,
      label: "Рабочая панель",
      routerLink: "/dashboard",
      iconType: "fa",
      iconName: "dashboard"
    },
    {
      id: 1,
      label: "Рабочая панель",
      routerLink: "/dashboard",
      iconType: "fa",
      iconName: "dashboard"
    },
    {
      id: 3,
      label: "Проекты",
      routerLink: "/projects",
      iconType: "fa",
      iconName: "projects"
    },
    {
      id: 16,
      label: "Мое расписание",
      routerLink: "/myschedule",
      iconType: "pg",
      iconName: "calender"
    },
    {
      id: 4,
      label: "Планировщик",
      routerLink: "/staff-planner",
      iconType: "fa",
      iconName: "staff-planner"
    },
    {
      id: 5,
        label: "Субаренда",
        routerLink: "/sublease",
      iconType: "share-alt",
      iconName: "share-alt",
      toggle: "close",
      submenu: [
        {
          label: "Субаренда",
          routerLink: "/sublease",
          iconType: "fa",
          iconName: "point"
        },
        {
          label: "Дефицит",
          routerLink: "/sublease/shortage",
          iconType: "fa",
          iconName: "point"
        }
      ]
    },
    {
      id: 6,
      label: "Счета",
      routerLink: "/invoices",
      iconType: "pg",
      iconName: "invoices"
    },
    {
      id: 7,
      label: "Оборудование",
      routerLink: "/equipments",
      iconType: "fa",
      iconName: "equipments",
      toggle: "close",
      submenu: [
        {
          id: 7,
          label: "Карточка оборудования",
          routerLink: "/equipments",
          iconType: "fa",
          iconName: "equipments"
        },
        {
          id: 2,
          label: "Движение оборудования",
          routerLink: "/warehouse",
          iconType: "fa",
          iconName: "warehouse"
        },
        {
          id: 16,
          label: "Мониторы ",
          routerLink: "#",
          iconType: "fa",
          iconName: "eqipment-disabled"
        },
        {
          id: 17,
          label: "Отчеты",
          routerLink: "#",
          iconType: "fa",
          iconName: "eqipment-disabled"
        }
      ]
    },
    {
      id: 8,
      label: "Контакты",
      routerLink: "/contacts",
      iconType: "fa",
      iconName: "phone"
    },
    {
      id: 9,
      label: "Персонал",
      routerLink: "/staff",
      iconType: "fa",
      iconName: "staff",
      toggle: "close",
      submenu: [
        {
          id: 9,
          label: "Персонал",
          routerLink: "/staff",
          iconType: "fa",
          iconName: "staff",
        },
        {
          id: 12,
          label: "Регистрация рабочего времени",
          routerLink: "/time-registration",
          iconType: "pg",
          iconName: "time-registration"
        },
        {
          id: 18,
          label: "Начисление заработной платы",
          routerLink: "#",
          iconType: "pg",
          iconName: "time-disabled"
        },
      ]
    },
    {
      id: 10,
      label: "Транспорт",
      routerLink: "/vehicles",
      iconType: "fa",
      iconName: "vehicles"
    },
    {
      id: 11,
      label: "Задачи",
      routerLink: "/tasks",
      iconType: "fa",
      iconName: "tasks"
    },
    {
      id: 13,
        label: "Обслуживание",
        routerLink: "/maintenance/repair",
      iconType: "fa",
      iconName: "maintenance",
      toggle: "close",
      submenu: [
        {
          label: "Ремонт",
          routerLink: "/maintenance/repair",
          iconType: "fa",
          iconName: "point"
        },
        {
          label: "Техническое обслуживание",
          routerLink: "/maintenance/inspections",
          iconType: "fa",
          iconName: "point"
        }
      ]
    },
    {
      id: 19,
      label: "Менеджмент заказчика",
      iconType: "fa",
      iconName: "maintenance",
      toggle: "close",
      submenu: [
        {
          label: "Монитор запросов",
          routerLink: "#",
          iconType: "fa",
          iconName: "time-disabled"
        },
        {
          label: "Отправить предложение",
          routerLink: "#",
          iconType: "fa",
          iconName: "time-disabled"
        },
        {
          label: "Заявка на страхование",
          routerLink: "#",
          iconType: "fa",
          iconName: "time-disabled"
        }
      ]
    },
    {
      id: 14,
      label: "Свернуть меню",
      routerLink: "",
      iconType: "pg",
      iconName: "row-right"
    },
    {
      id: 15,
      label: "Конфигурация",
      routerLink: "/configuration",
      iconType: "fa",
      iconName: "configuration"
    }
  ];

  @HostBinding("class.visible") mobileSidebar: boolean;
  @HostListener("mouseenter", ["$event"])
  @HostListener("click", ["$event"])
  tooltipMenuEvents($event) {
    if ($event.target.id != "profileMenuContent") {
      this.profileMenuContent.hide();
      if ($event.target.id != '') {
        this.menuLinks.forEach(item => {
          item.toggle = "close"
        });
      }
    }
  }


  @HostListener("click", ["$event"])
  tooltipProfileMenuEvents($event) {
    if ($event.target.id != "profileMenuContent") {
      this.profileMenuContent.hide();
      if ($event.target.id != '') {
        this.menuLinks.forEach(item => {
          item.toggle = "close"
        });
      }
    }
  }



  ngAfterViewInit() {
    this.profileMenuContent = this.tooltipDirective.find(elem => elem.id === "profileMenuContent");
  }


  ngOnInit(): void {
    // identity
    this.store$.dispatch(new IdentityStoreActions.GetCheck());
    this.user$ = this.store$.select(IdentityStoreSelectors.selectIdenityUser);
    this.logo$ = this.store$.select(IdentityStoreSelectors.selectLogo);
    this._subscriptions.push(
      this.store$
        .select(IdentityStoreSelectors.selectModules)
        .subscribe((data: Array<any>) => {
          if (data && data.length > 0) {
            data = data.sort((a: any, b: any) => {
              if (a.order < b.order) {
                return -1;
              } else if (a.order > b.order) {
                return 1;
              } else {
                return 0;
              }
            });
          }

          if (data && this.menuLinks.length === 0) {
            data.forEach((element: any) => {
              let menu: any = this.menuCollection.find(
                f => f.id === element.id
              );
              if (menu) {
                this.menuLinks.push(
                  this.menuCollection.find(f => f.id === element.id)
                );
              }

              if (element.id == 13) {

                let menu: any = this.menuCollection.find(
                  f => f.id === 19
                );
                this.menuLinks.push(menu);
              }

            });

          }
        })
    );

    // notifications
    this.store$.dispatch(new NotificationsStoreActions.GetLastUnreadNotifications());
    this.lastNotifications$ = this.store$.select(NotificationsStoreSelectors.selectLastUnreadNotifications);

    // router tabs
    Object.assign(this.tabService.tabOptions, this.menuCollection);
  }

  visibleMiniLogo() {
    let pageSidebar = document.getElementsByClassName("page-sidebar")[0];

    if (pageSidebar.hasAttribute("close")) {
      return true;
    }
    else if (pageSidebar.hasAttribute("open")) {
      return false;
    }
    else {
      return true;
    }
  }

  closeSideBar() {
    let pageSidebar = document.getElementsByClassName("page-sidebar")[0];
    if (pageSidebar.hasAttribute("open")) {
      document.getElementById('toggle-menu').click();
    }
  }

  tabsClose() {
    this.tabService.tabs = [];
  }
}
