import { TableEntity } from "@shared/enums/table-entity.enum";
import { ConfidentialType } from "@shared/enums/confidential-type.enum";

export class NoteModel {
  id: string;
  confidential: ConfidentialType;
  text: string;
  belongsTo: TableEntity;
  referenceId: string;
}
