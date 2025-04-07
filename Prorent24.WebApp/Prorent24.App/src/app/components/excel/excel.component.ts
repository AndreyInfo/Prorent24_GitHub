import {
  Component,
  Input,
  ViewChild,
  Output,
  EventEmitter
} from "@angular/core";
import { ModalDirective } from "ngx-bootstrap";
import { HttpService } from "@core/http.service";
import { GeneralExcelEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { Entity } from "@shared/enums/entity.enum";
import { ExcelWorkSheet } from "@models/common/excel/excel-worksheet.model";
import { ExcelSubEntityColumn } from "@models/common/excel/excel-sub-entity.model";
import { TreeFoldersComponent } from "@components/tree-folders/tree-folders.component";

@Component({
  selector: "excel",
  templateUrl: "./excel.component.html"
})
export class ExcelComponent {
  @ViewChild("excelImportModal", { static: true }) excelImportModal: ModalDirective;
  @ViewChild(TreeFoldersComponent, { static: true }) foldersModal: TreeFoldersComponent;
  // @ViewChild("foldersTree") foldersTree: jqxTreeComponent;

  @Input() entityType: Entity;
  // @Output() onSelect = new EventEmitter<any>();
  // select(folder: any): void {
  //   this.onSelect.emit(folder);
  // }

  worksheet: ExcelWorkSheet;
  subEntitiyColumns: ExcelSubEntityColumn[] = [];
  urlTamplateExcel: string = null;


  constructor(private http: HttpService) { }

  showExcelImport() {
    this.worksheet = new ExcelWorkSheet();
    this.urlTamplateExcel = `api/${StringExt.Format(GeneralExcelEndpoints.root, this.entityType)}/download-template`;
    this.excelImportModal.show();
  }

  // fileupload HandleChange
  fileHandleChange(event: any): void {
    const formData: FormData = new FormData();
    formData.append("files", event.file.originFileObj);
    this.http
      .postT<ExcelWorkSheet>(StringExt.Format(GeneralExcelEndpoints.root, this.entityType), formData)
      .subscribe(result => {
        this.worksheet = result;
        this.setDafaultConfiguration();
      });
  }

  showFoldersModal(): void {
    this.foldersModal.showFolders();
  }

  setFolder(folder: any): void {
    this.worksheet.folderId = folder.id;
    this.worksheet.folderName = folder.name;
  }

  setDafaultConfiguration() {
    //Set subEntities
    for (let groupIndx = 0; groupIndx < this.worksheet.subEntities.length; groupIndx++) {
      for (let entityIndx = 0; entityIndx < this.worksheet.subEntities[groupIndx].collection.length; entityIndx++) {
        this.subEntitiyColumns.push(this.worksheet.subEntities[groupIndx].collection[entityIndx]);
      }
    }
  }
  //TODO: Don't use in this iteration
  // IsValidExcel() {
  //   for (let rowIndx = 0; rowIndx < this.worksheet.rows.length; rowIndx++) {
  //     for (let cellIndx = 0; cellIndx < this.worksheet.rows[rowIndx].cells.length; cellIndx++) {
  //       let cell = this.worksheet.rows[rowIndx].cells[cellIndx];
  //       let entity = this.subEntitiyColumns.find(x => x.text == cell.key);
  //       if(entity!=undefined){
  //         if (entity.type == "number") {
  //           debugger
  //           let result = parseFloat(cell.value.toString());
  //         }
  //       }
  //     }
  //     return true;
  //   }
  // }

  import() {
    let cells = this.worksheet.rows[0].cells;
    for(let i = 0; i < this.worksheet.rows.length; i++){
      for(let c = 0; c < cells.length; c++){
      this.worksheet.rows[i].cells[c].key = cells[c].key;
      }
    }
    this.http
      .post(StringExt.Format(GeneralExcelEndpoints.root, this.entityType) + "/import", this.worksheet)
      .subscribe(result => {
        this.excelImportModal.hide();
      });
  }
}
