import { BaseModel } from "@models/base.model";

export class AccsessoryModel extends BaseModel {
  equipmentId: boolean;
  equipment: string;
  accessoryId: number;
  accessoryName: string;
  quantity: number;
  automatic: boolean;
  skipIfAlreadyPresent: boolean;
  free: boolean;
}
