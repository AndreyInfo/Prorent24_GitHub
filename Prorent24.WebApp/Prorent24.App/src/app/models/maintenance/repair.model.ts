import { BaseModel } from "@models/base.model";
import { UsableType } from '@shared/enums/usable-type.enum';

export class RepairModel extends BaseModel {
    equipmentId: number;
    serialNumberId?: number;
    reportedById: string;
    assignToId?: number;
    externalRepairId?: number;
    quantity: number = 1;
    from: Date = null;
    until: Date = null;
    cost: number = 0;
    usable: UsableType;
    remark: string;

    equipment: any;
    externalRepair: any;
}
