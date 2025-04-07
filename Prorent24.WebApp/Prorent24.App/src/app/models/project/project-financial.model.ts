import { BaseModel } from "@models/base.model";
import { DepositStatus } from "@shared/enums/deposit-status.enum";

export class ProjectFinancialModel extends BaseModel {
    projectId: number;
    deposit: number;
    depositStatus: DepositStatus;
    invoiceMomentId: number;
    additionalConditionId?: number;
    addittionalCondition: string;
    totalIncVat: number;
    vatSchemeId: number;
}
