import { BaseModel } from "@models/base.model";
import { AddressModel } from "@models/common/address.model";

export class ContactPersonModel extends BaseModel {
  title: string;
  firstName: string;
  lastName: string;
  middleName: string;
  function: string;
  address: AddressModel = new AddressModel();
  mobile: string;
  phone: string;
  email: string;
  salutationId: number;
  salutation: string;
}
