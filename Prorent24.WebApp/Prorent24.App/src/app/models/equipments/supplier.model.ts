import { BaseModel } from "@models/base.model";

export class SupplierModel extends BaseModel {
    price: number;
    details: number;
    contactId: number;
    contact: string;
}
