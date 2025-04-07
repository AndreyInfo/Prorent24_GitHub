import { BaseModel } from "@models/base.model";
import { PlanningTransportType } from "@shared/enums/planning-transport-type.enum";
import { PlanningCrewMemberRateType } from "@shared/enums/planning-crewmember-rate-type.enum";
import { ProjectFunctionType } from "@shared/enums/project-function-type.enum";

export class ProjectPlanningModel extends BaseModel {
  entityId: any;
  entityName: string;
  functionId: any;
  functionType: ProjectFunctionType;
  functionName: string;
  projectLeader: boolean;
  rateType: PlanningCrewMemberRateType =
  PlanningCrewMemberRateType.priceAgreement;
  transportType: PlanningTransportType;
  planFrom: Date = null;
  planUntil: Date = null;
  crewMemberRateId?: number;
  costs?: number;
  plannedCosts?: number;
  actualCosts?: number;
  remark: string;
}
