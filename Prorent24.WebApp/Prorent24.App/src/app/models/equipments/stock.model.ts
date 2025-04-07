import { BaseModel } from "@models/base.model";

export class StockModel extends BaseModel {
    equipmentId: number;
    quantity: number;
    deliveryDate: Date = null;
    description: string;
    details: string;
}
