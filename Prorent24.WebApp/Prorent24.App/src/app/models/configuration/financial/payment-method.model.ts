import { BaseModel } from "@models/base.model";

export class PaymentMethodModel extends BaseModel {
  name: string;
  accountingCode: string;
}
