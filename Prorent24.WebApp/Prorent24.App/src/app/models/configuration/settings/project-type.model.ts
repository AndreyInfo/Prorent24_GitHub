import { BaseModel } from "@models/base.model";

export class ProjectTypeModel extends BaseModel {
    name: string;
    color: string;
    packingSlipTemplateId: number;
    quotationTemplateId: number;
    contractTemplateId: number;
    invoiceTemplateId: number;
    letterheadTemplateId: number;
    invoiceMommentId: number;
    defaultAdditionalConditionId: number;
}
