import { BaseModel } from "@models/base.model";
import { InspectionStatus } from "@shared/enums/inspection-status.enum";

export class MaintenanceInspectionModel extends BaseModel {
  periodicInspectionId: number;
  status: InspectionStatus;
  date: Date = null;
  description: string;
}
