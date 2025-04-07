import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { ActivatedRoute } from "@angular/router";
import { VehicleModel } from "@models/vehicle/vehicle.model";
import { VehiclesGroupEndpoints } from "@endpoints";
import { ProjectFunctionType } from "@shared/enums/project-function-type.enum";

@Component({
  selector: "rent-vehicles-group",
  templateUrl: "./vehicles-group.component.html",
  providers: [GridService]
})
export class VehiclesGroupComponent extends GridAbstract<VehicleModel>
  implements OnInit {
  parentId: any;

  @Input() selectMode: string;
  @Input() checkedObserve: boolean;

  @Output() doubleClickRow = new EventEmitter<any>();
  doubleClickRowEvent(data: any): void {
    data.type = ProjectFunctionType.transport;
    this.doubleClickRow.emit(data);
  }

  @Output() checkedRows = new EventEmitter<any>();
  // checkedRowsEvent(): void {
  //   if (this.checkedObserve) {
  //     let data: any = {};
  //     data.type = ProjectFunctionType.transport;
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
    data.type = ProjectFunctionType.transport;
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
    super(gridService, VehicleModel, VehiclesGroupEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.loadSubData(this.parentId);

    // this.dataGrid.onBindingcomplete.subscribe(() => {
    //  // this.dataGrid.setcolumnproperty("id", "hidden", "true");
    //   /*  this.dataGrid.addgroup("folderName");
    // this.dataGrid.expandallgroups(); */
    // });
  }
}
