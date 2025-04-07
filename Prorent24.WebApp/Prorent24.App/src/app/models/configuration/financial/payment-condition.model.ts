import { BaseModel } from "@models/base.model";

export class PaymentConditionModel extends BaseModel {
  name: string;
  textOnInvoice: string;
  accountingCode: string;
  termInDays: number;
  paymentMethodId?: number;
}
