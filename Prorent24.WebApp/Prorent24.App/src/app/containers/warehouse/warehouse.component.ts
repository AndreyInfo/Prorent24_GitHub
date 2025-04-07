import { Component, OnInit } from "@angular/core";
import { HttpParams } from "@angular/common/http";
import { HttpService } from "@core/http.service";
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem
} from "@angular/cdk/drag-drop";
import { WarehouseEndpoints } from "@endpoints";
import "@shared/utils/date.extensions";
import { ShedulerModel } from "@shared/models/sheduler.model";

class KanbanItemModel {
  id: number;
  title: string;
  resource: string;
  startPlaning: Date;
  code: string;
}

class KanbanModel {
  pack: Array<KanbanItemModel> = new Array<KanbanItemModel>();
  prepped: Array<KanbanItemModel> = new Array<KanbanItemModel>();
  onLocation: Array<KanbanItemModel> = new Array<KanbanItemModel>();
  inUse: Array<KanbanItemModel> = new Array<KanbanItemModel>();
}

@Component({
  selector: "app-warehouse",
  templateUrl: "./warehouse.component.html"
})
export class WarehouseComponent implements OnInit {
  widjetSwitchPeriod: boolean = false;

  sheduleCrewSource: any;
  sheduleVehiclesSource: any;

  isCollapsedTransport: boolean = false;
  isCollapsedCrew: boolean = false;

  dateFilter: Date = new Date(Date.now());
  dateFilterDate: number = new Date().getDate();
  todayDate: number = new Date().getDate();
  tomorrowDate: number = new Date().addDays(1).getDate();

  tasks: KanbanModel = new KanbanModel();

  constructor(private http: HttpService) {}

  ngOnInit(): void {
    this.loadKanban();
    this.buildShedulerDeferredRequest();
  }

  changeDateFilter(): void {
    this.dateFilterDate = this.dateFilter.getDate();
    this.loadKanban();
    this.buildShedulerDeferredRequest();
  }

  onChangeWidjetPeriod(event) {
    this.widjetSwitchPeriod = event;
    if(event){
      this.changeBtnDate('tomorrow')
    }
    else{
      this.changeBtnDate('today');
    }
	}

  changeBtnDate(date: string): void {
    switch (date) {
      case "today":
        this.dateFilter = new Date(Date.now());
        break;
      case "tomorrow":
        this.dateFilter = new Date().addDays(1);
        break;
    }

    this.dateFilterDate = this.dateFilter.getDate();
    this.changeDateFilter();
  }

  drop(event: CdkDragDrop<string[]>): void {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      this.http
        .post(WarehouseEndpoints.tasks, {
          projectId: event.item.data.id,
          status: event.container.id
        })
        .subscribe();

      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }

  loadKanban(): void {
    let filterParams: any = new HttpParams().set(
      "date",
      this.dateFilter.toUTCString()
    );

    this.http
      .get(WarehouseEndpoints.tasks, filterParams)
      .subscribe(data => (this.tasks = data));
  }

  buildShedulerDeferredRequest(): void {
    let filterParams: any = new HttpParams().set(
      "date",
      this.dateFilter.toUTCString()
    );

    this.http
      .get(WarehouseEndpoints.shedule_crew, filterParams)
      .subscribe((data: ShedulerModel) => (this.sheduleCrewSource = data));

    this.http
      .get(WarehouseEndpoints.shedule_vehicles, filterParams)
      .subscribe((data: ShedulerModel) => (this.sheduleVehiclesSource = data));
  }
}
