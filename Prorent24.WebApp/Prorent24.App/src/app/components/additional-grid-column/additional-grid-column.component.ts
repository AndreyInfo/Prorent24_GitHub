import {
  Component,
  ViewChild,
  Input,
  Output,
  EventEmitter
} from "@angular/core";
import { ModalDirective } from "ngx-bootstrap";
import { jqxTreeComponent } from "jqwidgets-ng/jqxtree";
import { HttpService } from "@core/http.service";
import { GeneralGridColumnsEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { Entity } from "@shared/enums/entity.enum";
import { TranslateService } from "@ngx-translate/core";
import { take } from "rxjs/operators";

@Component({
  selector: "rent-additional-grid-column",
  templateUrl: "./additional-grid-column.component.html"
})
export class AdditionalGridColumnComponent {
  @ViewChild("columnModal", { static: true }) columnModal: ModalDirective;
  @ViewChild("columnTree", { static: true }) columnTree: jqxTreeComponent;

  @Input() entity: Entity;

  @Output() onAddColumn = new EventEmitter<any>();
  addColumnEvent(columns: any): void {
    this.onAddColumn.emit(columns);
  }

  source: any = {};
  items: Array<any> = new Array<any>();
  records: any = {};

  constructor(private http: HttpService, private translate: TranslateService) {}
        
  showModal(): void {
    this.columnModal.show();

    this.items = [];
    
    this.columnModal.onShown.pipe(take(1)).subscribe(() => {
      this.http
        .get(
          StringExt.Format(
            GeneralGridColumnsEndpoints.column_groups,
            this.entity
          )
        )
          .subscribe((data: any[]) => {
          data.forEach((f: any) => {
            let groupName = f.groupName;

            f.collection.forEach((e: any) => {
              
            let text = this.translate.instant(`entity.${e.text.toLowerCase()}`);
              e.text = String(text).length > 25 ? text.slice(0, 25) + '...' : text;
              e.type = "string";
            });

            f.collection.unshift({
              id: groupName,
              text: this.translate.instant(`${groupName.toLowerCase()}`),
              type: "string",
              parentId: "-1"
            });

            this.items = [...this.items, ...(f.collection as Array<any>)];
          });

          this.source = {
            datatype: "json",
            datafields: [
              { name: "id" },
              { name: "parentId" },
              { name: "text" },
              { name: "disabled" },
              { name: "selected" }
            ],
            id: "id",
            localdata: this.items
          };

          let dataAdapter: any = new jqx.dataAdapter(this.source, {
            autoBind: true
          });

          this.records = dataAdapter.getRecordsHierarchy(
            "id",
            "parentId",
            "items",
            [
              { name: "text", map: "label" },
              { name: "selected", map: "checked" }
            ]
          );

          this.columnTree.expandAll();
        });
    });
  }

  addColumn(): void {
    let data: any = {};
    data.moduleEntity = this.entity;

    data.columns = this.columnTree.getCheckedItems().map(node => {
      let col: any = {};
      col.id = node["id"];
      col.parentId = node["parentId"];
      return col;
    });

    this.addColumnEvent(data);
    this.columnModal.hide();
  }
}
