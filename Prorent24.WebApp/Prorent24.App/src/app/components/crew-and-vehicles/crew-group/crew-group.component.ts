import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { ActivatedRoute } from "@angular/router";
import { CrewMemberModel } from "@models/crew/crew-member.model";
import { StaffGroupEndpoints } from "@endpoints";
import { ProjectFunctionType } from "@shared/enums/project-function-type.enum";

@Component({
  selector: "rent-crew-group",
  templateUrl: "./crew-group.component.html",
  providers: [GridService]
})
export class CrewGroupComponent extends GridAbstract<CrewMemberModel>
  implements OnInit {
  parentId: any;

  @Input() selectMode: string;
  @Input() checkedObserve: boolean;

  @Output() doubleClickRow = new EventEmitter<any>();
  doubleClickRowEvent(data: any): void {
    if (data) {
      data.type = ProjectFunctionType.crew;
      this.doubleClickRow.emit(data);
    }
  }

  @Output() checkedRows = new EventEmitter<any>();
  // checkedRowsEvent(): void {
  //   if (this.checkedObserve) {
  //     let data: any = {};
  //     data.type = ProjectFunctionType.crew;
  //     data.functions = new Array<any>();

  //     this.dataGrid.getselectedrowindexes().forEach(i => {
  //       let row: any = this.dataGrid.getrowdatabyid(i.toString());
  //       data.functions.push({
  //         id: row.id,
  //         name: row.name
  //       });
  //     });

  //     this.checkedRows.emit(data);
  //   }
  // }

  checkedRowsEvent(nodes: any){
    let data: any = {};
    data.type = ProjectFunctionType.crew;
    data.functions = new Array<any>();
    for(let node = 0 ;node < nodes.length; node++){
      for(let child = 0 ;child < nodes[node].children.length; child++){
        if(nodes[node].children[child].isActive){
          data.functions.push({
            id: nodes[node].children[child].id,
            name: nodes[node].children[child].name
          });
        }
      }
    }
    this.checkedRows.emit(data);
  }

  constructor(public gridService: GridService, private route: ActivatedRoute) {
    super(gridService, CrewMemberModel, StaffGroupEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.loadSubData(this.parentId);
    // this.dataGrid.onBindingcomplete.subscribe(() => {
    //   this.dataGrid.addgroup("folderName");
    //   this.dataGrid.expandallgroups();

    //   this.dataGrid.setcolumnproperty("folderName", "hidden", "true");
    // });
  }
}
