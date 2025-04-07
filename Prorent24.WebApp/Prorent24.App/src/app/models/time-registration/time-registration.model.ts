import { BaseModel } from "@models/base.model";
import { TimeUnit } from "@shared/enums/time-unit.enum";

export class TimeRegistrationModel extends BaseModel {
  name: string;
  crewMemberId: string;
  crewMember: string;
  crewMembers: Array<string>;
  from: Date = null;
  until: Date = null;
  distance: number;
  breakDuration: number;
  breakTimeUnit: TimeUnit = TimeUnit.minutes;
  hourRegistrationType: string;
  travelDuration: number;
  travelTimeUnit: TimeUnit = TimeUnit.minutes;
  lunch: boolean;
  remark: string;
  activities: any;
}
