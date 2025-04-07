import { Component, OnInit, ViewChild, AfterViewInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ModalDirective } from "ngx-bootstrap";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { jqxSchedulerComponent } from "jqwidgets-ng/jqxscheduler";
import { HttpService } from "@core/http.service";
import { MySheduleEndpoints } from "@endpoints";
import "@shared/utils/date.extensions";
import { getLocalization } from "@shared/utils/jqxsheduler-localization";
import { ShedulerEventAction } from "@shared/enums/sheduler-event-action.enum";
import { Store } from "@ngrx/store";
import { RootStoreState } from "@store";
import { IdentityStoreSelectors } from "@store";
import { User } from "@models/session/user.model";
import { StaffAccessTimeModel } from "@models/staff-planner/staff-access-time.model";
import { TranslateService } from "@ngx-translate/core";
import { StringExt } from "@shared/utils/string.extension";
import { ProjectFunctionType } from "@shared/enums/project-function-type.enum";
import { CalendarModel } from "@models/calendar/calendar..model";
import * as moment from "moment";

@Component({
  selector: "app-schedule",
  templateUrl: "./schedule.component.html"
})
export class ScheduleComponent implements OnInit, AfterViewInit {
  @ViewChild("myScheduler", { static: true })
  myScheduler: jqxSchedulerComponent;
  @ViewChild("calendarCellModal", { static: true })
  calendarCellModal: ModalDirective;

  localization: any = getLocalization("ru");

  user: User = new User();

  timeEventType: any = ShedulerEventAction;
  calendarFormData: CalendarModel = new CalendarModel();
  calendarData: Array<CalendarModel> = new Array<CalendarModel>();

  buildProgress: boolean = false;

  date: any;
  source: any = {};
  dataAdapter: any = {};
  resources: any = {};
  appointmentDataFields: any = {
    id: "id",
    subject: "subject",
    from: "start",
    to: "end",
    action: "action",
    comment: "comment",
    background: "background",
    borderColor: "background",
    color: "color"
  };
  views: string[] | any[] = [
    {
      type: "dayView",
      timeRuler: { scale: "hour", formatString: "HH:mm" },
      workTime: {
        fromDayOfWeek: 1,
        toDayOfWeek: 5,
        fromHour: 8,
        toHour: 20
      }
    },
    {
      type: "weekView",
      timeRuler: { scale: "hour", formatString: "HH:mm" },
      workTime: {
        fromDayOfWeek: 1,
        toDayOfWeek: 5,
        fromHour: 8,
        toHour: 20
      }
    },
    {
      type: "monthView",
      timeRuler: { scale: "hour", formatString: "HH:mm" },
      monthRowAutoHeight: true
    },
    //{ type: "agendaView", timeRuler: { scale: "hour", formatString: "HH:mm" } }
  ];
  renderAppointment: any = {};

  constructor(
    private toggler: PagesToggleService,
    private http: HttpService,
    public store$: Store<RootStoreState.IState>,
    private translate: TranslateService
  ) { }

  ngOnInit(): void {
    this.http
      .get(StringExt.Format(MySheduleEndpoints.calendar, this.user.id))
      .subscribe(
        (data: Array<any>) => {
          this.calendarData = data;
          this.calendarData
            .filter((filter: CalendarModel) => filter.isAvailable)
            .forEach((item: CalendarModel) => {
              item.action = item.subject;
              item.background = this.getAvailabilityColor(item.subject);
              item.subject = `${this.translate.instant(item.subject)}. ${
                item.comment ? item.comment : ""
                }`;
              item.color = "#767676";
              item.borderColor = "#767676";
              item.start = this.dateUTCToLocal(item.start);
              item.end = this.dateUTCToLocal(item.end);
            });
        },
        null,
        () => {
          this.buildCalendar();
        }
      );

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });

    this.store$
      .select(IdentityStoreSelectors.selectIdenityUser)
      .subscribe(data => (this.user = data));
  }

  ngAfterViewInit(): void {
    this.myScheduler.onBindingComplete.subscribe(() => {
      this.myScheduler.getAppointments().forEach(i => {
        this.myScheduler.setAppointmentProperty(
          i.id.toString(),
          "resizable",
          false
        );
        this.myScheduler.setAppointmentProperty(
          i.id.toString(),
          "draggable",
          false
        );
      });
    });
  }

  showEditDialog(event: any): void {
    this.calendarFormData = new CalendarModel();
    this.calendarFormData.start = event.args.date.toDate();
    this.calendarFormData.end = null;
    this.calendarCellModal.show();
  }

  onClickEvent($event: any): void {
    this.calendarFormData = new CalendarModel();
    Object.assign(this.calendarFormData, $event.args.appointment);

    this.calendarFormData.action = $event.args.appointment.action;
    this.calendarFormData.start =
      $event.args.appointment.from instanceof Date
        ? $event.args.appointment.from
        : $event.args.appointment.from.toDate();
    this.calendarFormData.end =
      $event.args.appointment.to instanceof Date
        ? $event.args.appointment.to
        : $event.args.appointment.to.toDate();

    this.calendarCellModal.show();
  }

  onSubmitCalendarData(form: NgForm): void {
    if (form.valid) {
      let _availability: CalendarModel = form.value;

      let req_model: any = {
        id: _availability.id,
        type: ProjectFunctionType.crew,
        functionIds: [],
        action: _availability.action,
        from: _availability.start,
        until: _availability.end,
        comment: _availability.comment
      };

      req_model.functionIds.push(this.user.id);

      let postReq = req_model.id
        ? this.http.post(
          StringExt.Format(
            MySheduleEndpoints.update_accessibility,
            req_model.id
          ),
          req_model
        )
        : this.http.post(MySheduleEndpoints.new_accessibility, req_model);

      this.buildProgress = true;
      postReq.subscribe(
        (data: StaffAccessTimeModel) => {
          let appointmentType: string =
            ShedulerEventAction[ShedulerEventAction[_availability.action]];
          _availability.subject = `${this.translate.instant(
            appointmentType
          )}. ${_availability.comment ? _availability.comment : ""}`;
          _availability.background = this.getAvailabilityColor(appointmentType);
          _availability.color = "#767676";
          _availability.draggable = false;
          _availability.resizable = false;

          if (_availability.id) {
            this.refreshdata(_availability, false);
          } else {
            _availability.id = data.id;
            this.refreshdata(_availability, true);
          }

          this.calendarCellModal.hide();
        },
        () => (this.buildProgress = false),
        () => {
          this.buildProgress = false;
        }
      );
    }
  }

  private buildCalendar(): void {
    let dateNow: Date = new Date(Date.now());
    this.date = new jqx.date(
      dateNow.getFullYear(),
      dateNow.getMonth() + 1,
      dateNow.getDate()
    );

    this.source = {
      dataType: "json",
      dataFields: [
        { name: "id", type: "string" },
        { name: "action", type: "string" },
        { name: "comment", type: "string" },
        { name: "subject", type: "string" },
        { name: "start", type: "date" },
        { name: "end", type: "date" },
        { name: "background", type: "string" },
        { name: "borderColor", type: "string" },
        { name: "color", type: "string" }
      ],
      id: "id",
      localData: this.calendarData
    };
    this.dataAdapter = new jqx.dataAdapter(this.source);
    this.resources = {
      colorScheme: "scheme04",
      source: new jqx.dataAdapter(this.source),
    };
    this.renderAppointment = (data) => {
      if(data && data.appointment){
        let start = moment(data.appointment.originalData.start).format('HH:mm')
        let end = moment(data.appointment.originalData.end).format('HH:mm')
        let html = `<span class="myschedule-date">${start} - ${end} <span class="myschedule-text">   ${data.html}</span></span>`;
  
        data.html = html;
      }
      return data;
    }
  }

  refreshdata(appointment: any, isNew: boolean) {
    if (!isNew) {
      let dataArray: any[] = this.source.localData;
      let index = dataArray.findIndex((f: any) => f.id === appointment.id);
      this.source.localData[index] = appointment;
    } else {
      this.source.localData.push(appointment);
    }

    this.dataAdapter = new jqx.dataAdapter(this.source);
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
        color = "#E7A953";
        break;
    }

    return color;
  }

  private dateUTCToLocal(value: any): Date {
    console.log(value);
    if (value) {
      value = value + "Z";
      if (moment(value).isValid()) {
        value = moment.utc(value).toDate();
      }
    }
    else {

    }

    return value;
  }

  //#region date range
  _startValueChange = () => {
    if (this.calendarFormData.start > this.calendarFormData.end) {
      this.calendarFormData.end = null;
    }
  };

  _endValueChange = () => {
    if (this.calendarFormData.start > this.calendarFormData.end) {
      this.calendarFormData.start = null;
    }
  };

  _disabledStartDate: any = (startValue: Date) => {
    if (!startValue || !this.calendarFormData.end) {
      return false;
    }
    return (
      new Date(startValue).getTime() >=
      new Date(this.calendarFormData.end).getTime()
    );
  };

  _disabledEndDate: any = (endValue: Date) => {
    if (!endValue || !this.calendarFormData.start) {
      return false;
    }
    return (
      new Date(endValue).getTime() <=
      new Date(this.calendarFormData.start).getTime()
    );
  };
  //#endregion
}
