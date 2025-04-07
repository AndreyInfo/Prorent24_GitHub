import { BaseModel } from "@models/base.model";

export class InvoiceMomentModel extends BaseModel {
    name: string;
    text: string;
    afterAgreement: number;
    beforeFirstDay: number;
    afterwards: number;
}
