import { BaseModel } from "@models/base.model";

export class EquipmentContentModel extends BaseModel {
    equipmentId: number;
    equipment: string;
    name: string;
    contentName: string;
    quantity: number;
    unfoldFinancialDocuments: boolean;
    unfoldPackingSlip: boolean;
    contentId: number;
}
