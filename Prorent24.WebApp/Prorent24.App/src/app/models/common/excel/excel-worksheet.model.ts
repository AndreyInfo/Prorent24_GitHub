import { ExcelRow } from './excel-row.model'
import { ExcelEntity } from './excel-entity.model'
import { ExcelSubEntity } from './excel-sub-entity.model'

export class ExcelWorkSheet {
  folderId: number;
  folderName: string;
  containsHeader: boolean = true;
  requiredFields: string[];
  entities: ExcelEntity[];
  subEntities: ExcelSubEntity[];
  rows: ExcelRow[];
  columns: ExcelRow[];
}