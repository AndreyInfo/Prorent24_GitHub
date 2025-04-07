import { BaseModel } from "@models/base.model";
import { ContactModel } from "@models/contacts/contact.model";

export class SerialNumberModel extends BaseModel {
  equipmentId: number;
  serialNumber: string;
  internalReference: string;
  purchaseDate: Date = null;
  purchasePrice: number;
  calculateBookValueAutomatically: boolean;
  depreciationPerMonth: number;
  bookValue: number;
  remark: string;
  active: boolean;
  suppliersInfo: Array<ContactModel> = new Array<ContactModel>();
}
