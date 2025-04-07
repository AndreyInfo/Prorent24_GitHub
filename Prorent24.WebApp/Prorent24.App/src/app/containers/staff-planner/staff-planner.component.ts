import { Component, OnInit, ViewChild } from "@angular/core";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { Entity } from "@shared/enums/entity.enum";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";
import { CrewPlannerEndpoints } from "@endpoints";
import { HttpParams } from "@angular/common/http";
import { ModalDirective } from "ngx-bootstrap";
import { NgForm } from "@angular/forms";
import { ShedulerEventAction } from "@shared/enums/sheduler-event-action.enum";
import { StaffAccessTimeModel } from "@models/staff-planner/staff-access-time.model";
import {
  ShedulerModel,
  Availability,
  Resource,
  EventModel
} from "@shared/models/sheduler.model";
import { SchedulerComponent } from "@components/sheduler/sheduler.component";
import { TranslateService } from "@ngx-translate/core";
import { formatDate } from "@angular/common";
import { FilterPanelComponent } from "@components/filter/filter-panel.component";
import { FilterPeriod } from "@models/common/filter-period.model";
import { PeriodType } from "@shared/enums/period-type.enum";
import { jqxSplitterComponent } from "jqwidgets-ng/jqxsplitter";
import { FilterModel } from "@models/common/filter.model";
import { FilterType } from "@shared/enums/filter-type.enum";
import { DurationType } from "@shared/enums/duration-type.enum";
import { TimeUnitExtend } from "@shared/enums/time-unit.enum";

@Component({
  selector: "app-staff-planner",
  templateUrl: "./staff-planner.component.html"
})
export class StaffPlannerComponent implements OnInit {
  @ViewChild("filterPanel", { static: true }) filterPanel: FilterPanelComponent;
  @ViewChild("shedulerProjects", { static: true })
  shedulerProjects: SchedulerComponent;
  @ViewChild("shedulerAvailabilities", { static: true })
  shedulerAvailabilities: SchedulerComponent;
  @ViewChild("accessShedulerTimeModal", { static: true })
  accessShedulerTimeModal: ModalDirective;
  @ViewChild("shedulerInfo", { static: true }) shedulerInfo: ModalDirective;

  @ViewChild("nestedSplitter", { static: true })
	nestedSplitter: jqxSplitterComponent;

  entityType: Entity = Entity.crewPlanner;

  shedulerAvailabilitiesSource: ShedulerModel;
  shedulerProjectsSource: ShedulerModel;

  widjetSwitchPeriod: boolean = false;

  filters: Array<any> = new Array<any>();
  filterPeriod: FilterPeriod = new FilterPeriod();
  periodType: any = PeriodType;
	durationType: any = DurationType;
	timeUnitExtend: any = TimeUnitExtend;
  checkedFunctions: any = {
    type: "",
    functions: []
  };

  staffAccess: StaffAccessTimeModel = new StaffAccessTimeModel();
  timeEventType: any = ShedulerEventAction;

  fromDate: Date = null;
  toDate: Date = null;

  panelOptions: any = {
    mainSplitter: [] = [{ size: 300 }, { size: "100%", collapsible: false }],
    nestedSplitter: [] = [
      { size: "60%", collapsible: false },
      { size: "40%", collapsible: false }
    ]
  };

  widjetSwitchState: boolean = true;
  showInfoWidget: boolean = true;

  constructor(
    private toggler: PagesToggleService,
    private translate: TranslateService,
    private http: HttpService
  ) {}

  ngOnInit(): void {
    this.loadProjectsShedulerData();

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
     // this.nestedSplitter.expand();
    });
  }

  onChangeWidjetSwitch(event){
		this.widjetSwitchState = !event;
		this.toggleWidget();
    }
    
    toggleWidget(): void {
      this.showInfoWidget = !this.showInfoWidget;
      if (this.showInfoWidget) {
        this.nestedSplitter.expand();
      } else {
        this.nestedSplitter.collapse();
      }
    }

    onChangedFilterByPeriod(): void {
		
      let selectedFilter: Array<any> = new Array<any>();
      let periodModel = new FilterModel();
      this.filterPeriod.periodType = !this.widjetSwitchPeriod ? PeriodType.fromToUntil : PeriodType.period;
      periodModel.filterType = FilterType.period;
      periodModel.values = [];
      periodModel.values.push(JSON.stringify(this.filterPeriod));
      selectedFilter.push(periodModel);
      this.onChangedFilter(selectedFilter);
    }

    	
	  // date range
	  _startValueChange = () => {
      if (this.filterPeriod.fromDate > this.filterPeriod.toDate) {
        this.filterPeriod.toDate = null;
      }
      };
    
      _endValueChange = () => {
      if (this.filterPeriod.fromDate > this.filterPeriod.toDate) {
        this.filterPeriod.fromDate = null;
      }
      };
    
      _disabledStartDate: any = (startValue: Date) => {
      if (!startValue || !this.filterPeriod.toDate) {
        return false;
      }
      return (
        new Date(startValue).getTime() >=
        new Date(this.filterPeriod.toDate).getTime()
      );
      };
    
      _disabledEndDate: any = (endValue: Date) => {
      if (!endValue || !this.filterPeriod.fromDate) {
        return false;
      }
      return (
        new Date(endValue).getTime() <=
        new Date(this.filterPeriod.fromDate).getTime()
      );
      };

  onChangedFilter(filters: Array<any>): void {
    this.filters = filters;

    let periodFilter: any = filters.find(f => f.filterType === "Period");
    if (periodFilter && periodFilter.values) {
      let periodFilterValue: FilterPeriod = periodFilter.values[0];
      if (periodFilterValue.periodType === PeriodType.fromToUntil) {
        this.fromDate = periodFilterValue.fromDate;
        this.toDate = periodFilterValue.toDate;
      }
    }

    this.loadAccessibilityShedulerData();
    this.loadProjectsShedulerData();
  }

  onCheckedFunctions(event: any): void {
    Object.assign(this.checkedFunctions, event);
    this.checkedFunctions.functions.forEach((f: any) => (f.selected = "true"));
    this.loadAccessibilityShedulerData();
  }

  onPlanning(): void {
    let newFunctionsCount: number = 0;
    let originSelectedFunctionGroup: any = {};
    Object.assign(
      originSelectedFunctionGroup,
      this.shedulerProjects.selectedEvent
    );

    let functionGroup: any = this.shedulerProjects.selectedEvent;
    let functions: any = this.checkedFunctions;
    let functionsList: any[] = functions.functions;

    if(originSelectedFunctionGroup.type.toLowerCase() !== functions.type){
      return;
    }

    let plan_obj: any = {
      type: functions.type,
      planningEntities: functionsList.map((e: any) => {
        return {
          entityId: e.id,
          name: e.name
        };
      }),
      functionId: Number.parseInt(functionGroup.entityId),
      start: functionGroup.start,
      end: functionGroup.end
    };

    this.http
      .post(
        StringExt.Format(
          CrewPlannerEndpoints.plannings,
          functionGroup.resource
        ),
        plan_obj
      )
      .subscribe(
        (data: ShedulerModel) => {
          let icon: string = this.setFunctionGroupIcon(plan_obj.type);

          let _functionGroup: any = this.shedulerProjectsSource.events.find(
            (f: any) => f.entityId.toString() === functionGroup.entityId
          );
          _functionGroup.plannedQuantity =
            _functionGroup.plannedQuantity + data.events.length;

          data.events.forEach((e: EventModel) => {
            let current_function: any = plan_obj.planningEntities.find(
              (f: any) => f.entityId.toString() === e.resource
            );

            var new_event: EventModel = new EventModel();
            new_event.id = e.id;
            new_event.resource = `${functionGroup.resource}_1`;
            new_event.start = e.start;
            new_event.end = e.end;
            new_event.text = current_function
              ? `${icon} ${current_function.name}`
              : "";
            new_event.fontColor = "#fff";
            new_event.backColor = new_event.isAvailability
              ? "#3C93CE"
              : "#F77975";
            new_event.barColor = "#f9f9f9";

            this.shedulerProjectsSource.events.push(new_event);
            this.shedulerAvailabilitiesSource.events.push(e);

            newFunctionsCount++;
          });
        },
        null,
        () => {
          if (
            originSelectedFunctionGroup.plannedQuantity != newFunctionsCount
          ) {
            originSelectedFunctionGroup.plannedQuantity += newFunctionsCount;
            originSelectedFunctionGroup = this.setFunctionGroupQuantity(
              originSelectedFunctionGroup, originSelectedFunctionGroup.temp_text
            );

            let functionGroupIndex: number = this.shedulerProjectsSource.events.findIndex(
              (f: any) => f.id === functionGroup.id
            );

            this.shedulerProjectsSource.events[
              functionGroupIndex
            ] = originSelectedFunctionGroup;
          }
        }
      );
  }

  //#region sheduler projects
  loadProjectsShedulerData(): void {
    this.http
      .get(
        CrewPlannerEndpoints.root,
        new HttpParams().set("filters", JSON.stringify(this.filters))
      )
      .subscribe((data: ShedulerModel) => {
        data.resources.forEach(r => {
          r.expanded = true;
          r.children.forEach(f => (f.name = this.translate.instant(f.name)));
        });
        data.events.forEach((e: any) => {
          let icon: string = this.setFunctionGroupIcon(e.type);

          e.temp_text = e.text;

          if (e.needQuantity > 0) {
            e = this.setFunctionGroupQuantity(e);
          } else {
            e.text = `${icon} ${e.text}`;
            e.fontColor = "#fff";
            e.backColor = e.isAvailability ? "#3C93CE" : "#F77975";
            e.barColor = "#f9f9f9";
          }
        });

        this.shedulerProjectsSource = data;
      });
  }
  //#endregion

  //#region sheduler availabilities
  loadAccessibilityShedulerData(): void {
    let functions: [] = this.checkedFunctions.functions;
    if (functions.length > 0) {
      let ids: any[] = functions.map(e => {
        return e["id"];
      });

      this.http
        .post(
          StringExt.Format(
            CrewPlannerEndpoints.scheduler,
            this.checkedFunctions.type
          ),
          ids,
          new HttpParams().set("filters", JSON.stringify(this.filters))
        )
        .subscribe((data: ShedulerModel) => {
          data.resources.forEach(e => {
            e.availabilities.forEach(a => {
              a.color = this.getAvailabilityColor(a.type);
            });
          });

          data.events.forEach((e: any) => {
            e.fontColor = "#fff";
            e.backColor = "#3C93CE";
            e.barColor = "#f9f9f9";

            e.bubbleHtml = `<div>${e.text}</div><span>${formatDate(
              e.start,
              "dd/MM/yy HH:mm",
              "en-US"
            )} - ${formatDate(e.end, "dd/MM/yy HH:mm", "en-US")}</span>
            `;
          });

          this.shedulerAvailabilitiesSource = data;
        });
    }
    else{
      let data = new ShedulerModel();
      data.resources = [];
      data.events.forEach((e: any) => {
        e.fontColor = "#fff";
        e.backColor = "#3C93CE";
        e.barColor = "#f9f9f9";

        e.bubbleHtml = `<div>${e.text}</div><span>${formatDate(
          e.start,
          "dd/MM/yy HH:mm",
          "en-US"
        )} - ${formatDate(e.end, "dd/MM/yy HH:mm", "en-US")}</span>
        `;
      });
      this.shedulerAvailabilitiesSource = data;
    }
  }

  onTimeRangeSelected(event: any): void {
    this.staffAccess.type = this.checkedFunctions.type;
    this.staffAccess.from = event.start.value;
    this.staffAccess.until = event.end.value;

    this.accessShedulerTimeModal.show();
  }

  onSubmitAccessShedulerTimeData(form: NgForm): void {
    if (form.valid) {
      let functions: [] = this.checkedFunctions.functions;
      if (functions.length > 0) {
        let ids: any[] = functions
          .filter(f => f["selected"] === "true")
          .map(e => {
            return e["id"];
          });

        if (ids.length > 0) {
          form.value.functionIds = ids;

          let _accessTimeResponse: StaffAccessTimeModel = new StaffAccessTimeModel();

          this.http
            .post(CrewPlannerEndpoints.root, form.value)
            .subscribe(
              (data: StaffAccessTimeModel) => (_accessTimeResponse = data),
              null,
              () => {
                this.accessShedulerTimeModal.hide();

                let _tempResource: Array<Resource> = new Array<Resource>();
                Object.assign(
                  _tempResource,
                  this.shedulerAvailabilitiesSource.resources
                );

                let availability: Availability = new Availability();
                availability.start = _accessTimeResponse.from;
                availability.end = _accessTimeResponse.until;
                availability.color = this.getAvailabilityColor(
                  ShedulerEventAction[
                    ShedulerEventAction[_accessTimeResponse.action]
                  ]
                );

                _accessTimeResponse.functionIds.forEach(e => {
                  _tempResource
                    .find(f => f.id === e)
                    .availabilities.unshift(availability);
                });

                this.shedulerAvailabilitiesSource.resources = _tempResource;
              }
            );
        }
      }
    }
  }
  //#endregion

  onResizeShedulerSplitter(): void {
    this.shedulerProjects.updateHeight();
    this.shedulerAvailabilities.updateHeight();
  }

  onScrollProjectSheduler(posX: number): void {
    this.shedulerAvailabilities.setHorizontalScrollPosition(posX);
  }

  onScrollAvailabilitiesSheduler(posX: number): void {
    this.shedulerProjects.setHorizontalScrollPosition(posX);
  }

  private setFunctionGroupIcon(groupType: string): string {
    let icon: string = "";
    switch (groupType.toLowerCase()) {
      case "crew":
        icon = `<i class="fa fa-user ml-1 mr-1"></i>`;
        break;
      case "transport":
        icon = `<i class="fa fa-truck ml-1 mr-1"></i>`;
        break;
    }

    return icon;
  }

  private setFunctionGroupQuantity(e: any, text?: string): any {
    let icon: string = this.setFunctionGroupIcon(e.type);

    e.text = `${icon}(${e.plannedQuantity}/${e.needQuantity}) ${
      text ? text : e.text
    }`;

    e.barColor = e.plannedQuantity >= e.needQuantity ? "#10CFBD" : "#F77975";

    e.bubbleHtml = `<div>${e.text}</div><span>${formatDate(
      e.start,
      "dd/MM/yy HH:mm",
      "en-US"
    )} - ${formatDate(e.end, "dd/MM/yy HH:mm", "en-US")}</span>`;
    /*     <br>
            <div>Статус: ***</div> */

    return e;
  }

  private getAvailabilityColor(type: string): string {
    let color: string;
    switch (type) {
      case ShedulerEventAction[ShedulerEventAction.available]:
        color = "#DAEFFD";
        break;
      case ShedulerEventAction[ShedulerEventAction.notAvailable]:
        color = "#FDDDDD";
        break;
      case ShedulerEventAction[ShedulerEventAction.unknown]:
        color = "#D8DADC";
        break;
      case ShedulerEventAction[ShedulerEventAction.reserved]:
        color = "#E2DEEF";
        break;
      case ShedulerEventAction[ShedulerEventAction.invite]:
        color = "#CFF5F2";
        break;
      case ShedulerEventAction[ShedulerEventAction.appointment]:
        color = "#FEF6DD";
        break;
    }

    return color;
  }

  onChangeWidjetPeriod(event) {
      this.widjetSwitchPeriod = event;
      this.onChangedFilterByPeriod();
    }

    clearFilterByPeriod() {
        this.filterPeriod.fromDate = null;
        this.filterPeriod.toDate = null;

        this.onChangedFilterByPeriod();
    }
}

