export class CompanyContactModel {
  firstName: string;
  lastName: string;
  middleName: string;
}

export class CompanyOfficeModel {
  street: string;
  houseNumber: string;
  city: string;
  postcode: string;
  countryId: string;
}

export class CompanyDetailModel {
  contactPersonName: string;
  contactPersonEmail: string;
  companyName: string;
  shortName: string;
  invoiceEmail: string;
  phones: Array<string> = new Array<string>();
  website: string;
  street: string;
  houseNumber: string;
  city: string;
  postcode: string;
  countryId: string;

  directorInfo: CompanyContactModel = new CompanyContactModel();
  accountantInfo: CompanyContactModel = new CompanyContactModel();
  additionalOffice: CompanyOfficeModel = new CompanyOfficeModel();

  inn: string;
  kpp: string;
  ogrn: string;
  okpo: string;
  checkingAccount: string;
  correspondentAccount: string;
  bank: string;
  bic: string;
}
