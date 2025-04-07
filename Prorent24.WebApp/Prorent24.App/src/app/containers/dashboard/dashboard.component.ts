import { Component, OnInit, ViewChild } from "@angular/core";
import { ModalDirective } from "ngx-bootstrap";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";
import { WorkPanelEndpoints } from "@endpoints";

@Component({
  selector: "app-dashboard",
    templateUrl: "./dashboard.component.html",
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor(private http: HttpService) { }

  @ViewChild("rowModal", { static: true }) rowModal: ModalDirective;
  modalData: any;
  data = null;

  ngOnInit(): void {
    this.http.get(WorkPanelEndpoints.root)
      .subscribe(res => {
        this.data = res;
      });

  }

  getColorByStatus(status) {
    switch (status) {
      case "available": {
        return 'bg-complete-lighter';
      }
      case "notAvailable": {
        return 'bg-danger-lighter';
      }
      case "unknown": {
        return 'bg-info-lighter';
      }
      case "reserved": {
        return 'bg-primary-lighter';
      }
      case "invite": {
        return 'bg-success-lighter';
      }
      case "appointment": {
        return 'bg-warning-lighter';
      }
    }

    return '';
  }

  getClassByCount(count: number) {
    if (count > 0 && count < 6) {
      return "counter-block-dark-blue";
    }
    else if (count > 5) {
      return "counter-block-dark-red";
    }
    else {
      return "counter-block-blue";
    }
  }

  // 'projects.projectRequests'
  goToRequests(data: any) {
    console.log(data);
  }
  //'hasCrewOnCancelledSubprojects', 'projects.cancelledWithCrew'
  openCancelledProjects(params: any, data: any) {
    console.log(params, data);
  }
  goToWarehouse(filter: any): void {
    console.log(filter);
  }

  //'projects', ['isOption'], 'projects.projectsOptie'
  goToProjectList(module: any, params: any, data: any): void {
    console.log(module);
  }
  // 'root.invoices.invoices-tab.list', 'todo', ['notPayed', 'deadline'], 'todo.openInvoices.expired'
  goToState(path: any, module: any, params: any, data: any): void {
    console.log(path, module, params, data);
  }
  //['isOption', 'unplannedCrewFunctions'], 'planning.unscheduledCrew.option'
  goToPlanner(params: any, action: any): void {
    console.log(params, action);
  }

  // false, 'todo.openTasks.open'
  goToTaskList(value: any, action: any) {
    console.log(value, action);
  }
  //['notRepaired'], 'todo.openRepairs'
  goToRepairList(params: any, data: any) {
    console.log(params, data);
  }
  onTimelineItemClick(data: any) { }
  openTimeModal(destination: any): void {
    //
  }

  toBrowserTime(data: any): Date {
    var date = new Date(data);
    var timeZone = new Date().getTimezoneOffset() / (-60);
    var hour = date.getHours();
    date.setHours(hour + timeZone);
    return date;
  }

  showDetails(title:string,type:string, items: any) {
    this.modalData = {};
    this.modalData.title = title;
    this.modalData.type = type;
    this.modalData.items = items;
    this.rowModal.show();
    }
}
