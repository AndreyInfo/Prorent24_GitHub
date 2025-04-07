import { BaseModel } from "@models/base.model";
import { AddressModel } from "@models/common/address.model";
import { SocialNetwork } from "@shared/models/social-network.model";

export class ContactModel extends BaseModel {
  companyTypeId: number;
  companyName: string;
  companyShortName: string;
  firstName: string;
  middleName: string;
  lastName: string;
  birthDate: Date = null;
  nameLine: string;
  phone: string;
  email: string;
  bankAccountNumber: string;
  databaseNumber: string;
  defaultContactPersonId: number;

  folderId: number;
  folderName: string;

  visitingAddressId?: number;
  visitingAddress: AddressModel = new AddressModel();
  postalAddressId?: number;
  postalAddress: AddressModel = new AddressModel();
  billingAddressId?: number;
  billingAddress: AddressModel = new AddressModel();

  phones: Array<string> = new Array<string>();
  companyPhones: Array<string> = new Array<string>();
  email2: string;
  website: string;
  cocNumber: string;
  vatIdentificationNumber: string;
  fiscalCode: string;
  purchaseNumber: string;
  warning: string;
  subjectProjNote: string;
  projNote: string;

  inn: string;
  kpp: string;
  ogrn: string;
  okpo: string;
  checkingAccount: string;
  correspondentAccount: string;
  bank: string;
  bic: string;

  isCompany: boolean;
  specialization: boolean;
  
  socialNetworks: Array<SocialNetwork> = new Array<SocialNetwork>();

  contactPerson: string;

  isPublic: boolean;
    crewMembers: Array<any>;
}
