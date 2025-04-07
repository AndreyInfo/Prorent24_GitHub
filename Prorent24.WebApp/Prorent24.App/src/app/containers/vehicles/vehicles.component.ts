import { Component, OnInit, ViewChild, Injector, AfterViewInit, Output, EventEmitter } from "@angular/core";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { GridService } from "@services/grid.service";
import { VehicleModel } from "@models/vehicle/vehicle.model";
import { VehiclesEndpoints, CrewPlannerEndpoints } from "@endpoints";
import { ExcelComponent } from "@components/excel/excel.component";
import { Entity } from "@shared/enums/entity.enum";
import { SchedulerComponent } from "@components/sheduler/sheduler.component";
import { TreeGridAbstract } from "@abstractions/tree-grid.abstraction";
import { StringExt } from "@shared/utils/string.extension";
import { ShedulerModel } from "@shared/models/sheduler.model";
import { ShedulerEventAction } from "@shared/enums/sheduler-event-action.enum";
import { HttpService } from "@core/http.service";
import { ModalDirective } from "ngx-bootstrap";
import { TableEntity } from '@shared/enums/table-entity.enum';
//import { GeneralGridColumnsEndpoints } from "@endpoints";

@Component({
  selector: "app-vehicles",
  templateUrl: "./vehicles.component.html"
})
export class VehiclesComponent extends TreeGridAbstract<VehicleModel>
  implements OnInit, AfterViewInit {
  @ViewChild("sheduler", { static: true }) sheduler: SchedulerComponent;
  @ViewChild("shedulerInfo", { static: true }) shedulerInfo: ModalDirective;
    @ViewChild(ExcelComponent, { static: true }) excelModal: ExcelComponent;

    @Output() onAddColumn = new EventEmitter<any>();
    addColumnEvent(columns: any): void {
        this.onAddColumn.emit(columns);
    }

  entityType: Entity = Entity.vehicles;
    tableEntityType: TableEntity = TableEntity.vehicles;
       
 
  shedulerVehiclesSource: ShedulerModel;

  showTimeline: boolean=false;

  constructor(
    public injector: Injector,
    private http: HttpService,
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(injector, VehicleModel, VehiclesEndpoints);
    }


    setDefaultColumns(): void {
        let data: any = {};
        data.moduleEntity = this.tableEntityType;
        data.columns = null;

        this.gridService.setGridColumn(data).then(() => {
            this.loadData();
        });
    }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
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

  ngAfterViewInit(): void {
    this.treeGrid.onBindingComplete.subscribe(() => {
        this.treeGrid.setColumnProperty("displayInPlanner", "cellsAlign", "center");
      this.treeGrid.selectRow(0);
    });

    this.treeGrid.onRowCheck.subscribe(() => {
      this.loadVehiclesShedulerData();
    });

    this.treeGrid.onRowUncheck.subscribe(() => {
      setTimeout(() => {
        this.loadVehiclesShedulerData();
      });
    });
  }

  loadVehiclesShedulerData(): void {
    let checkedRows: any[] = this.treeGrid.getCheckedRows();
    if (checkedRows.length > 0) {
      let ids: any[] = checkedRows.map(e => {
        return e["id"];
      });

      this.http
        .post(
          StringExt.Format(CrewPlannerEndpoints.scheduler, "transport"),
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

          this.shedulerVehiclesSource = data;
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

    onChangedFilterByTextT(val: any) {
        console.log("p1-onChangedFilterByTextT " + val)
    }
}
