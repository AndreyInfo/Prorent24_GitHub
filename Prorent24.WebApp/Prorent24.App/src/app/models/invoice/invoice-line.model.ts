import { BaseModel } from "@models/base.model";

export class InvoiceLine extends BaseModel {
	invoiceId?: number;
	name: string;
	description: string;
	price: number = 0;
	vatClassId?: number;
	ledgerId?: number;
	amount: number;
}
