import { BaseModel } from "@models/base.model";

export class ShortageModel extends BaseModel {
    projectId: number;
    projectName: string;
    equipmentId: number;
    equipmentName: string;
    plannedQuantity: number;
    ownStockQuantity: number;
    subhiredQuantity: number;
    shortageQuantity: number;
    subhireIds: number[] = [];
}
