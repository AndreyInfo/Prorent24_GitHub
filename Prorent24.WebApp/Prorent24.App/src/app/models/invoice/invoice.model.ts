import { ContactModel } from "@models/contacts/contact.model";
import { ContactPersonModel } from "@models/contacts/contact-person.model";
import { ProjectModel } from "@models/project/project.model";
import { CrewMemberShortModel } from "@models/crew/crew-member-short.model";
import { DocumentModel } from "@models/common/document.model";
import { InvoiceLine } from "./invoice-line.model";
import { InvoiceState } from "@shared/enums/invoice-state.enum";

export class InvoiceModel extends DocumentModel {
  name: string;
  projectId?: number;
  project: ProjectModel = new ProjectModel();
  clientId?: number;
  client: ContactModel = new ContactModel();
  contactPersonId?: number;
  contactPerson: ContactPersonModel = new ContactPersonModel();

  state: InvoiceState;

  ownerId?: string;
  owner: CrewMemberShortModel = new CrewMemberShortModel();
  accountingCode: string;
  creditDebit?: number;

  // financial
  paymentConditionId?: number;
  paymentMethodId?: number;
  vatSchemeId?: number;
  vatClassId?: number;
  ledgerId?: number;
  totalNewPrice?: number;
  priceExcludeVAT?: number = 0;
  priceIncludeVAT?: number = 0;
  dueDate?: Date = null;
  paymentDate?: Date;
  sendAtDate?: Date;
  invoiceLines: InvoiceLine[] = [];

  url?: any;
  vat?: number = 0;
}
