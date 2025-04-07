import { Injectable } from "@angular/core";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";
import { FoldersEndpoints } from "@endpoints";
import { Observable } from "rxjs";

@Injectable()
export class FoldersService {
  constructor(private http: HttpService) { }

  editFolder(data: any, id?: any): Observable<any> {
    if (id) {
      return this.http.post(
        StringExt.Format(FoldersEndpoints.single, id),
        data
      );
    } else {
      return this.http.post(FoldersEndpoints.root, data);
    }
  }

  deleteFolder(id: any): Observable<any> {
    if (id) {
      return this.http.post(StringExt.Format(FoldersEndpoints.delete, id));
    }
  }

  buildFolders(items: Array<any>): any {
    let data: any[] = [];
    items.forEach((f: any) => {
      let folder: any = {
        id: f.id,
        parentid: -1,
        text: f.name,
        checked: f.selected
      };
      data.push(folder);

      //LEVEL 1
      f.childs.forEach((level1: any) => {
        let leve1_folder: any = {
          id: level1.id,
          parentid: level1.parentId,
          text: level1.name,
          checked: level1.selected
        };

        data.push(leve1_folder);

        //LEVEL 2
        level1.childs.forEach((level2: any) => {
          let leve2_folder: any = {
            id: level2.id,
            parentid: level2.parentId,
            text: level2.name,
            checked: level2.selected
          };

          data.push(leve2_folder);

          //LEVEL 3
          level2.childs.forEach((level3: any) => {
            let leve3_folder: any = {
              id: level3.id,
              parentid: level3.parentId,
              text: level3.name,
              checked: level3.selected
            };

            data.push(leve3_folder);

            //LEVEL 4
            level3.childs.forEach((level4: any) => {
              let leve4_folder: any = {
                id: level4.id,
                parentid: level4.parentId,
                text: level4.name,
                checked: level4.selected
              };

              data.push(leve4_folder);

              //LEVEL 5
              level4.childs.forEach((level5: any) => {
                let leve5_folder: any = {
                  id: level5.id,
                  parentid: level5.parentId,
                  text: level5.name,
                  checked: level5.selected
                };

                data.push(leve5_folder);
              });
            });
          })
        });
      });
    });

    let source: any = {
      datatype: "json",
      datafields: [
        { name: "id" },
        { name: "parentid" },
        { name: "text" },
        { name: "checked" }
      ],
      id: "id",
      localdata: data
    };

    let dataAdapter: any = new jqx.dataAdapter(source, { autoBind: true });
    let folders: any = dataAdapter.getRecordsHierarchy(
      "id",
      "parentid",
      "items",
      [{ name: "text", map: "label" }]
    );

    return folders;
  }
}
