import { Component, OnInit, Input, Injector, ViewChild } from "@angular/core";
import { HttpService } from "@core/http.service";
import { TreeGridAbstract } from "@abstractions/tree-grid.abstraction";
import { GridService } from "@services/grid.service";
import { GeneralFilesEndpoints } from "@endpoints/api.endpoints";
import { ActivatedRoute } from "@angular/router";
import { QrCodeModel } from "@models/equipments/qr-code.model";
import { StringExt } from '@shared/utils/string.extension';
import { saveAs } from 'file-saver';
import { ModalDirective } from "ngx-bootstrap";

@Component({
  selector: "app-equipment-files",
  templateUrl: "./equipment-files.component.html"
})
export class EquipmentFilesComponent extends TreeGridAbstract<any>
  implements OnInit {

  @ViewChild("fileModal", { static: true }) fileModal: ModalDirective;
  parentId: any;
  url: string;
  checkboxState:boolean;

  constructor(public injector: Injector, private route: ActivatedRoute, private http: HttpService) {
    super(injector, Object, GeneralFilesEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.url = StringExt.Format(GeneralFilesEndpoints.list, "equipment", this.parentId, false, "null");
    this.loadDataByUrl(this.url)
  }

  searchByText($event) {
    let text = $event.target.value;
    text = text == '' ? 'null' : text;
    this.url = StringExt.Format(GeneralFilesEndpoints.list, "equipment", this.parentId, false, text);
    this.loadDataByUrl(this.url)
  }

  onCheckAllRows(state) {
    let rows: any = this.treeGrid.getRows();
    rows.forEach((row: any) => {
      let id = row["uid"];
      if (state) {
        this.treeGrid.checkRow(id);
      }
      else {
        this.treeGrid.uncheckRow(id);
      }
    });
  }

  onRowSelectItem($event) {
    var args = $event.args;
    var dataField = args.dataField;
    if (dataField == "file") {
      var url = StringExt.Format(this.http.host + GeneralFilesEndpoints.single,args.row.id);
      var element = document.createElement('a');
      element.setAttribute('href',url);
      element.setAttribute('download', args.row.file);
      element.style.display = 'none';
      document.body.appendChild(element);
      element.click();
      document.body.removeChild(element);
    }
    this.onRowSelect($event);
  }

  onUploadFile($event: any) {
    let uploadUrl = StringExt.Format(GeneralFilesEndpoints.upload, "equipment", this.parentId, false);
    let selectedFile: any = $event.target.files[0];
    const formData = new FormData();
    formData.append("file", selectedFile);
    this.http.postArrayBuffer(uploadUrl, formData).subscribe(data => {
      this.loadDataByUrl(this.url);
    });
  }


  onEditModal(id?: any): void {
    if (id) {
      this.fileModal.show();
    }
  }

  onDeleteFiles() {
    let rows: any = this.treeGrid.getRows();
    rows.forEach((row: any) => {
      if (row.checked) {
        this.http.post(StringExt.Format(GeneralFilesEndpoints.delete, row.id)).subscribe(data => {
          this.treeGrid.deleteRow(row.uid);
        });
      }
    });
  }


}
