import {
  Component,
  ViewChild,
  Input,
  OnInit,
  Output,
  EventEmitter
} from "@angular/core";
import { DayPilot, DayPilotSchedulerComponent } from "daypilot-pro-angular";
import { ShedulerModel } from "@shared/models/sheduler.model";

@Component({
  selector: "rent-scheduler-component",
  templateUrl: "./sheduler.component.html",
  styleUrls: ["./sheduler.component.scss"]
})
export class SchedulerComponent implements OnInit {
  @ViewChild("scheduler", { static: true })
  scheduler: DayPilotSchedulerComponent;

  _source: ShedulerModel;
  _heightSpec: string = "Max";
  _treeEnabled: boolean = false;
  _startDate: Date = new Date(Date.now());
  _moveHandling: string = "Disabled";
  _deleteHandling: string = "Disabled";
  _timeRangeSelected: string = "Disabled";
  _zoomPlus: number;
  _zoomMinus: number;
  _controlPanel: boolean = false;
  selectedEvent: any = {};
  zLevel: string = "Week";

  config: any = {
    resources: new Array<any>(),
    startDate: Date
  };

  events: Array<any> = new Array<any>();

  @Input() height:string="200px";
  @Input()
  set source(model: ShedulerModel) {
    if (model) {
      this.config.resources = model.resources;
      this.events = model.events;
    }
  }
  get source(): ShedulerModel {
    return this._source;
  }

  @Input()
  set controlPanel(value: boolean) {
    this._controlPanel = value;
  }
  get controlPanel(): boolean {
    return this._controlPanel;
  }

  @Input()
  set startDate(value: Date) {
    if (value) {
      this._startDate = value;
      this.config.startDate = value;
    }
  }
  get startDate(): Date {
    return this._startDate;
  }

  @Input()
  set heightSpec(value: string) {
    this._heightSpec = value;
  }
  get heightSpec(): string {
    return this._heightSpec;
  }

  @Input()
  set treeEnabled(value: boolean) {
    this._treeEnabled = value;
  }
  get treeEnabled(): boolean {
    return this._treeEnabled;
  }

  @Input()
  set moveHandling(value: string) {
    this._moveHandling = value;
    this.config.eventMoveHandling = value;
  }
  get moveHandling(): string {
    return this._moveHandling;
  }

  @Input()
  set deleteHandling(value: string) {
    this._deleteHandling = value;
    this.config.eventDeleteHandling = value;
  }
  get deleteHandling(): string {
    return this._deleteHandling;
  }

  @Input()
  set timeRangeSelected(value: string) {
    this._timeRangeSelected = value;
    this.config.timeRangeSelected = value;
  }
  get timeRangeSelected(): string {
    return this._deleteHandling;
  }

  @Output() onTimeRangeSelected = new EventEmitter<any>();
  timeRangeSelectedEvent(data: any): void {
    this.onTimeRangeSelected.emit(data);
  }

  @Output() onScrollingX = new EventEmitter<any>();
  scrollingXEvent(position: any): void {
    this.onScrollingX.emit(position);
  }

  ngOnInit(): void {
    this.config = {
      locale: "ru-ru",
      cellWidthSpec: "Fixed",
      cellWidth: 20,
      crosshairType: "Full",
      timeHeaders: [
        { groupBy: "Day", format: "dddd, d MMM" },
        { groupBy: "Hour" }
      ],
      scale: "CellDuration",
      cellDuration: 30,
      days: DayPilot.Date.today().daysInMonth(),
      startDate: this._startDate,
      separators: [
        {
          color: "#F55753",
          location: new DayPilot.Date(new Date(), true),
          layer: "AboveEvents"
        }
      ],
      rowHeaderWidthAutoFit: false,
      rowHeaderWidth: 140,
      businessEndsHour: 19,
      businessWeekends: true,
      eventHeight: 30,
      durationBarVisible: true,
      heightSpec: this._heightSpec,
      hideBorderFor100PctHeight: true,
      onUpdateHeight: () => {
        this.scheduler.control.update();
      },
      timeRangeSelectedHandling: this._timeRangeSelected,
      onTimeRangeSelected: (args: any) => {
        this.timeRangeSelectedEvent(args);
      },
      allowMultiSelect: false,
      eventClickHandling: "Select",
      onEventClick: (args: any) => {
        this.selectedEvent = args.e.data;
      },
      eventMoveHandling: this._moveHandling,
      onEventMoved: (args: any) => {},
      eventResizeHandling: "Disabled",
      onEventResized: (args: any) => {},
      eventDeleteHandling: this._deleteHandling,
      onEventDeleted: (args: any) => {},
      eventHoverHandling: "Bubble",
      bubble: new DayPilot.Bubble(),
      treeEnabled: this._treeEnabled,
      rowHeaderHideIconEnabled: true,
      onBeforeEventRender: (args: any) => {},
      onBeforeCellRender: (args: any) => {
        let dp: any = this.scheduler.control;
        let row: any = dp.rows.find(args.cell.resource);
        if (row) {
          let availabilities: any = row.data.availabilities;
          if (!availabilities) {
            return;
          }
          let item: any = availabilities.find((range: any) => {
            let start: any = new DayPilot.Date(range.start);
            let end: any = new DayPilot.Date(range.end);
            return DayPilot.Util.overlaps(
              start,
              end,
              args.cell.start,
              args.cell.end
            );
          });
          if (item) {
            args.cell.backColor = item.color;
          }
        }
      },
      dynamicLoading: true,
      onScroll: () => {
        let _scrollX: any = this.scheduler.control.getScrollX();
        this.scrollingXEvent(_scrollX);
      }
    };
  }

  // height
  updateHeight(): void {
    this.config.onUpdateHeight();
  }

  // scroll
  setHorizontalScrollPosition(position: number): void {
    let dp: any = this.scheduler.control;
    dp.setScrollX(position);
  }

  // zoom
  setZoomLevel(): void {
    console.log(this.zLevel);
    /* switch (this.zLevel) {
      case "Day":
        this.config.zoom = 0;
        break;
      case "Week":
        this.config.zoom = 1;
        break;
      case "Month":
        this.config.zoom = 2;
        break;
    } */
  }
}
//zoom: 1,
/*       zoomLevels: [
        {
          name: "Day",
          properties: {
            scale: "CellDuration",
            cellDuration: 15,
            cellWidth: 40,
            timeHeaders: [
              { groupBy: "Day", format: "dddd MMMM d, yyyy" },
              { groupBy: "Hour" },
              { groupBy: "Cell" }
            ],
            startDate: function(args) {
              return this._startDate;
            },
            days: function() {
              return 1;
            }
          }
        },
        {
          name: "Week",
          properties: {
            scale: "Hour",
            cellWidth: 40,
            timeHeaders: [
              { groupBy: "Month" },
              { groupBy: "Day", format: "dddd d" },
              { groupBy: "Hour" }
            ],
            startDate: function(args) {
              return this._startDate;
            },
            days: function() {
              return 7;
            }
          }
        },
        {
          name: "Month",
          properties: {
            scale: "CellDuration",
            cellDuration: 720,
            cellWidth: 40,
            timeHeaders: [
              { groupBy: "Month" },
              { groupBy: "Day", format: "ddd d" },
              { groupBy: "Cell", format: "tt" }
            ],
            startDate: function(args) {
              return args.date.firstDayOfMonth();
            },
            days: function(args) {
              return args.date.daysInMonth();
            }
          }
        }
      ], */
