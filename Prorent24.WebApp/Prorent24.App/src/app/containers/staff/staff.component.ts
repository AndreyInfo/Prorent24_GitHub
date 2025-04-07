import { Component, OnInit, ViewChild, Injector } from "@angular/core";
import { formatDate } from "@angular/common";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { CrewMemberModel } from "@models/crew/crew-member.model";
import {
  StaffEndpoints,
  StaffShedulerEndpoints,
  CrewPlannerEndpoints
} from "@endpoints";
import { GridService } from "@services/grid.service";
import { Entity } from "@shared/enums/entity.enum";
import { TableEntity } from "@shared/enums/table-entity.enum";
import { PermissionService } from "@services/permission.service";
import { SchedulerComponent } from "@components/sheduler/sheduler.component";
import { ShedulerModel } from "@shared/models/sheduler.model";
import { HttpService } from "@core/http.service";
import { TreeGridAbstract } from "@abstractions/tree-grid.abstraction";
import { ShedulerEventAction } from "@shared/enums/sheduler-event-action.enum";
import { ModalDirective } from "ngx-bootstrap";
import { StringExt } from "@shared/utils/string.extension";

@Component({
  selector: "app-staff",
  templateUrl: "./staff.component.html"
})
export class StaffComponent extends TreeGridAbstract<CrewMemberModel>
  implements OnInit {
  @ViewChild("sheduler", { static: true }) sheduler: SchedulerComponent;
  @ViewChild("shedulerInfo", { static: true }) shedulerInfo: ModalDirective;

  permissions: any;
  entityType: Entity = Entity.crewMembers;
  tableEntityType: TableEntity = TableEntity.crewMembers;

  shedulerStaffSource: ShedulerModel;

  showTimeline: boolean=false;

  constructor(
    public injector: Injector,
    private http: HttpService,
    private toggler: PagesToggleService,
    public gridService: GridService,
    private permissionService: PermissionService
  ) {
    super(injector, CrewMemberModel, StaffEndpoints);

    this.permissionService
      .getPermissions(this.entityType)
      .subscribe(data => (this.permissions = data));
  }

  ngOnInit(): void {
    this.loadData(this.filter);

      console.log("p1 - 111111111111111111111111");

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });

    this.treeGrid.onBindingComplete.subscribe(() => {
      //this.treeGrid.selectRow(0);
    });

    this.treeGrid.onRowCheck.subscribe(() => {
        this.loadStaffShedulerData();
        
    });

    this.treeGrid.onRowUncheck.subscribe(() => {
      setTimeout(() => {
          this.loadStaffShedulerData();
          
      });
    });

    // splitter
    this.panelOptions = {
      mainSplitter: [] = [{ size: "70%", collapsible: false }, { size: "30%" }],
      nestedSplitter: [] = [
        { size: "100%", collapsible: false },
        { size: 260, min: 260, max: 350 }
      ]
    };
    }

    setDefaultColumns(): void {
        console.log("p1 - 1111111111111111111");
        let data: any = {};
        data.moduleEntity = this.tableEntityType;
        data.columns = null;

        this.gridService.setGridColumn(data).then(() => {
            this.loadData();
        });
    }

    loadStaffShedulerData(): void {
    let checkedRows: any[] = this.treeGrid.getCheckedRows();
    if (checkedRows.length > 0) {
      let ids: any[] = checkedRows.map(e => {
        return e["id"];
      });
      this.http
        .post(
          StringExt.Format(CrewPlannerEndpoints.scheduler, "crew"),
          ids
          // new HttpParams().set("filters", JSON.stringify(this.filter))
        )
        .subscribe((data: ShedulerModel) => {
          data.resources.forEach(e => {
            e.availabilities.forEach(a => {
              a.color = this.getAvailabilityColor(a.type);
            });
          });
          data.events.forEach((e: any) => {
            e.fontColor = "#fff";
            e.backColor = "#3C93CE";
          });
            this.shedulerStaffSource = data;
        });
    }
  }

  private getAvailabilityColor(type: string): string {
    let color: string;
    switch (type) {
      case ShedulerEventAction[ShedulerEventAction.available]:
        color = "#DAEFFD";
        break;
      case ShedulerEventAction[ShedulerEventAction.notAvailable]:
        color = "#FDDDDD";
        break;
      case ShedulerEventAction[ShedulerEventAction.unknown]:
        color = "#D8DADC";
        break;
      case ShedulerEventAction[ShedulerEventAction.reserved]:
        color = "#E2DEEF";
        break;
      case ShedulerEventAction[ShedulerEventAction.invite]:
        color = "#CFF5F2";
        break;
      case ShedulerEventAction[ShedulerEventAction.appointment]:
        color = "#FEF6DD";
        break;
    }

    return color;
  }
}
