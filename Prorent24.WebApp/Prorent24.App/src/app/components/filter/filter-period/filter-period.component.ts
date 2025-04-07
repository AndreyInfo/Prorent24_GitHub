import {
  Component,
  OnInit,
  Input,
  ViewChild,
  TemplateRef,
  Output,
  EventEmitter
} from "@angular/core";
import { Entity } from "@shared/enums/entity.enum";
import { FilterPeriod } from "@models/common/filter-period.model";
import { PeriodType } from "@shared/enums/period-type.enum";
import { DurationType } from "@shared/enums/duration-type.enum";
import { TimeUnitExtend } from "@shared/enums/time-unit.enum";
import "@shared/utils/date.extensions";

@Component({
  selector: "rent-filter-period",
  templateUrl: "./filter-period.component.html"
})
export class FilterPeriodComponent implements OnInit {
  @Input() entityType: Entity;

  @Output() onChanged = new EventEmitter<FilterPeriod>();
  changeFilter(filter: FilterPeriod): void {
    this.onChanged.emit(filter);
  }

  @ViewChild("periodTaskTemplate", { static: true }) periodTaskTemplate: TemplateRef<any>;
  @ViewChild("periodTimeRegTemplate", { static: true }) periodTimeRegTemplate: TemplateRef<any>;
  @ViewChild("periodProjectsTemplate", { static: true }) periodProjectsTemplate: TemplateRef<any>;

  periodType: any = PeriodType;
  durationType: any = DurationType;
  timeUnitExtend: any = TimeUnitExtend;

  filterPeriod: FilterPeriod = new FilterPeriod();

  ngOnInit(): void {
    switch (this.entityType) {
      case Entity.tasks:
        this.filterPeriod.duration = 7;
        this.filterPeriod.durationTime = DurationType.past;
        this.filterPeriod.periodType = PeriodType.relative;
        this.filterPeriod.timeUnit = TimeUnitExtend.days; 
        break;
      case Entity.timeRegistration:
        this.filterPeriod.duration = 1;
        this.filterPeriod.durationTime = DurationType.today;
        this.filterPeriod.periodType = PeriodType.period;
        this.filterPeriod.timeUnit = TimeUnitExtend.months;
        break;
      case Entity.subhire:
      case Entity.subhireShortage:
      case Entity.invoices:
        this.filterPeriod.duration = 7;
        this.filterPeriod.durationTime = DurationType.past;
        this.filterPeriod.periodType = PeriodType.period;
        this.filterPeriod.timeUnit = TimeUnitExtend.days;
        this.filterPeriod.fromDate = new Date(Date.now());
        this.filterPeriod.toDate = new Date().addDays(7);
        break;
      case Entity.projects:
        this.filterPeriod.duration = 1;
        this.filterPeriod.durationTime = DurationType.next;
        this.filterPeriod.periodType = PeriodType.period;
        this.filterPeriod.timeUnit = TimeUnitExtend.months;
        break;
      case Entity.crewPlanner:
        this.filterPeriod.duration = 7;
        this.filterPeriod.durationTime = DurationType.next;
        this.filterPeriod.periodType = PeriodType.period;
        this.filterPeriod.timeUnit = TimeUnitExtend.days;
        this.filterPeriod.fromDate = new Date(Date.now());
        this.filterPeriod.toDate = new Date().addDays(7);
        break;
      default:
        return null;
    }
  }

  loadTemplate(entityType: Entity): any {
    switch (entityType) {
      case Entity.tasks:
        return this.periodTaskTemplate;
      case Entity.timeRegistration:
        return this.periodTimeRegTemplate;
      case Entity.projects:
      case Entity.subhire:
      case Entity.subhireShortage:
      case Entity.invoices:
      case Entity.crewPlanner:
        return this.periodProjectsTemplate;
      default:
        return null;
    }
  }

  change(): void {
    this.changeFilter(this.filterPeriod);
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
}
