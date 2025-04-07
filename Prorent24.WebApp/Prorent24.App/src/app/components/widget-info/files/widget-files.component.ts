import { Component, OnInit, Input } from "@angular/core";
import { StringExt } from "@shared/utils/string.extension";
import { GeneralFilesEndpoints } from "@endpoints";
import { Entity } from "@shared/enums/entity.enum";
import { HttpService } from "@core/http.service";

@Component({
  selector: "rent-widget-files",
  templateUrl: "./widget-files.component.html"
})
export class WidgetFilesComponent implements OnInit {
  @Input() entityId: any;
  @Input() entityType: Entity;
  @Input() data: Array<any> = new Array<any>();

  fileUploadUrl: string;
  fileDownloadUrl: string;

  users: Array<any> = new Array<any>();

  constructor(private http: HttpService) {}

  ngOnInit(): void {
    this.fileUploadUrl =
      "api/" +
      StringExt.Format(
        GeneralFilesEndpoints.root,
        this.entityType,
        this.entityId
      );

    this.buildUploadUrl();
  }

  // fileupload HandleChange
  fileHandleChange(event: any): void {
    if (event.file.response) {
      this.data.push(event.file.response);
      this.buildUploadUrl();
    }
  }

  removeFile(id: any): void {
    this.http
      .post(StringExt.Format(GeneralFilesEndpoints.delete, id))
      .subscribe(null, null, () => {
        let index: number = this.data.findIndex(f => f.id === id);
        this.data.splice(index, 1);
      });
  }

  buildUploadUrl(): void {
    this.data.forEach(element => {
      element.url = this.fileDownloadUrl =
        "api/" + StringExt.Format(GeneralFilesEndpoints.single, element.id);
    });
  }
}
