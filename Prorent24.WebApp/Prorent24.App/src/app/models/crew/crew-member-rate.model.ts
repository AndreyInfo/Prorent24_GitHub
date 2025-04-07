import { BaseModel } from "@models/base.model";

export class CrewMemberRateModel extends BaseModel {
  name: string;
  crewMemberId: string;
  hourlyRate: number;
  dailyRate: number;
  maxNumberOfTimeUnit: number;
  timeUnit: string;
  isDefaultRate: boolean = false;
}
