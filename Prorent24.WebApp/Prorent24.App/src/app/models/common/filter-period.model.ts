import { PeriodType } from "@shared/enums/period-type.enum";
import { DurationType } from "@shared/enums/duration-type.enum";
import { TimeUnitExtend } from "@shared/enums/time-unit.enum";

export class FilterPeriod {
  periodType: PeriodType;
  durationTime: DurationType;
  duration: number;
  timeUnit: TimeUnitExtend;
  fromDate: Date = null;
  toDate: Date = null;

  FilterPeriod(){
    this.toDate = new Date().addDays(-7);
    this.fromDate = new Date().addDays(7);
  }
}
