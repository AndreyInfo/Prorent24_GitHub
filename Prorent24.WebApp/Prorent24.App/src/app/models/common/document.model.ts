import { BaseModel } from "@models/base.model";
import { DocumentTypeEnum } from "@shared/enums/document-type.enum";
import { OpenKitsAndCasesTypeEnum } from "@shared/enums/open-kit-cases-type.enum";

export class DocumentModel extends BaseModel {
  name: string;
  documentType: DocumentTypeEnum = DocumentTypeEnum.Default;
  description: string;
  confidential: boolean;
  number: string;
  date?: Date = null;
  generatedOn?: Date;
  generatedById?: string;
  templateId?: number;
  letterheadId?: number;
  useLetterhead?: boolean;
  subject: string;
  fileName: string;
  text: string;
  openKitsAndCases: OpenKitsAndCasesTypeEnum =
    OpenKitsAndCasesTypeEnum.defaultKitOrCase;
}
