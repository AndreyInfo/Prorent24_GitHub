import { BaseModel } from "@models/base.model";
import { SubleaseStatus } from "@shared/enums/sublease-status.enum";
import { SubleaseType } from "@shared/enums/sublease-type.enum";
import { ContactModel } from "@models/contacts/contact.model";

export class SubleaseModel extends BaseModel {
  name: string;
  number: number;
  status: SubleaseStatus = SubleaseStatus.option;
  accountManagerId: number;

  supplierContactId: number;
  locationContactId: number;
  supplierContactPersonId: number;
  locationContactPersonId: number;

  supplierContact: ContactModel = new ContactModel();
  locationContact: ContactModel = new ContactModel();

  supplierContactPerson: ContactModel = new ContactModel();
  locationContactPerson: ContactModel = new ContactModel();

  projectId: number;
  projectName: string;

  reference: string;
  automaticCalculate: boolean;
  equipmentCost: number;
  additionalCost: number;
  totalCost: number;
  type: SubleaseType = SubleaseType.deliveredInWarehouse;
  remark: string;

  usagePeriodStart?: Date = null;
  usagePeriodEnd?: Date = null;
  planningPeriodStart: Date = null;
  planningPeriodEnd: Date = null;
  deliveryCollectionStart?: Date = null;
  deliveryCollectionEnd?: Date = null;
}

// export class ContactViewModel{
//   companyName: string;
//   phone: string;
//   email: string;
// }
