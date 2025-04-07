import { BaseModel } from "@models/base.model";
import { SocialNetwork } from "@shared/models/social-network.model";

export class CrewMemberModel extends BaseModel {
  colorAppointments: string;
  description: string;
  email: string = null;
  firstName: string;
  lastName: string;
  middleName: string;
  phone: string;
  folderId: any;
  folderName: string;
  availability: number;
  displayInPlanner: boolean=true;
  drivingLicense: string;
  emergencyContact: string;
  receiveEmails: string;
  active: boolean;
  isPowerUser: boolean;
  lastLogin?: Date = null;
  userRoleId: string;
  username: string;
  addressId?: number;
  defaultRateId?: number;
  bankAccountNumber: string;
  birthdate?: Date = null;
  coCNumber: string;
  companyName: string;
  contractDate?: Date = null;
  hoursInContract?: number;
  passportNumber: string;
  passportCompany: string;
  passportDate?: Date = null;
  vat: string;

  address: string;
  number: string;
  city: string;
  postalCode: string;
  countryId: string;

  isSystemUser: boolean;
  socialNetworks: Array<SocialNetwork> = new Array<SocialNetwork>();
}
