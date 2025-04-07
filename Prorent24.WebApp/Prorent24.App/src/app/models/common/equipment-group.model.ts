import { BaseModel } from "@models/base.model";

export class EquipmentGroupModel extends BaseModel {
  name: string;
  groupName: string;
  projectId: number;
  subhireId: number;
  startPlanPeriod: Date = null;
  endPlanPeriod: Date = null;
  startUsePeriod: Date = null;
  endUsePeriod: Date = null;
}
