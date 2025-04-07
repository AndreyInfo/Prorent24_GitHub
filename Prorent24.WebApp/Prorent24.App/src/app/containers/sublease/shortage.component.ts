import { Component, OnInit, Injector } from "@angular/core";
import { TreeGridAbstract } from "@abstractions/tree-grid.abstraction";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { GridService } from "@services/grid.service";
import { Entity } from "@shared/enums/entity.enum";
import { SubleaseShortagesEndpoints, SubleaseEndpoints } from "@endpoints";
import { ShortageModel } from "@models/sublease/shortage.model";
import { HttpService } from "@core/http.service";
import { HttpParams } from "@angular/common/http";
import { Router } from "@angular/router";
import * as moment from "moment";
import { TableEntity } from '@shared/enums/table-entity.enum';
import { GridAbstract } from "@abstractions/grid.abstraction";

class ExistSubleaseModel {
  dateFrom: Date;
  dateTo: Date;
  subhires: Array<EquipmentShortModel> = new Array<EquipmentShortModel>();
}

class EquipmentShortModel {
  id: number;
  name: string;
  supplierName: string;
  status: string;
  planningPeriodStart: Date;
  planningPeriodEnd: Date;
  check: boolean;
}

@Component({
  selector: "app-shortage",
  templateUrl: "./shortage.component.html"
})
export class ShortageComponent extends TreeGridAbstract<ShortageModel>
  implements OnInit {
    entityType: Entity = Entity.subhireShortage;
    tableEntityType: TableEntity = TableEntity.subhireShortage;
  users: Array<any> = new Array<any>();

  sublease: ExistSubleaseModel = new ExistSubleaseModel();

  shortageInfo: Array<any> = new Array<any>();

  canAcceptSublease: boolean = false;

  constructor(
    public injector: Injector,
    private toggler: PagesToggleService,
    private http: HttpService,
    private router: Router,
    public gridService: GridService
  ) {
    super(injector, ShortageModel, SubleaseShortagesEndpoints);

    // splitter
    this.panelOptions = {
      mainSplitter: [] = [{ size: "70%", collapsible: false }, { size: "30%" }],
      nestedSplitter: [] = [
        { size: "100%", collapsible: false },
        { size: 260, min: 260, max: 350 }
      ]
    };
  }

  ngOnInit(): void {
   super.ngOnInit();

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });

    this.treeGrid.onBindingComplete.subscribe(() => {
      this.treeGrid.setColumnProperty("projectName", "width", 200);
      this.treeGrid.selectRow(0);

      let groups: any[] = this.treeGrid.getRows();
      let shortageQuantity: Array<any> = new Array<any>();

      groups.forEach(e => {
        let childs: [] = e.childrens;
        let shortageQuantityFiltered: any[] = childs.filter(
          f => f["shortageQuantity"] > 0
        );
        shortageQuantity = [...shortageQuantity, ...shortageQuantityFiltered];

        this.treeGrid.expandRow(e.uid);
      });

      if (shortageQuantity.length > 0) {
        Object.getOwnPropertyNames(shortageQuantity[0]).forEach(key => {
          this.treeGrid.setColumnProperty(key, "cellclassname", function(
            row,
            dataField,
            cellValueInternal,
            rowData,
            cellText
          ) {
            let isHasIndex: any = shortageQuantity.find(
              f => f.uid === rowData.uid
            );
            if (isHasIndex) {
              return "bg-danger-lighter";
            }
          });
        });
      }
    });
  }

  onShortageRowSelect(event: any): void {
    if (event.args.row) {
      this.selected_entity = event.args.row;
      this.selected_entity.rowIndex = event.args.rowindex;

      if (this.selected_entity.projectId) {
        this.canAction = true;
      }

      let params: any = new HttpParams()
        .set("projectId", this.selected_entity.projectId.toString())
        .set("equipmentId", this.selected_entity.equipmentId.toString());

      if (this.selected_entity["level"] > 0) {
        this.http
          .get(SubleaseShortagesEndpoints.details, params)
          .subscribe((data: any[]) => {
            let infoProject = data.find((f: any) => f.entity == "Project");
            infoProject.data.forEach((f: any) => {
              if(f.type === "date"){
                if (moment(f.value).isValid()) {
                  var stillUtc = moment.utc(f.value).toDate();
                  f.value = moment(stillUtc)
                    .local()
                    .format("DD.MM.YYYY HH:mm");
                }
              }
            });

            this.shortageInfo = data
          });
      }
    }
  }

  redirectToProject(): void {
    this.router.navigate([
      `/projects/edit/${this.selected_entity.projectId}/data`
    ]);
  }

  onShowCreateSublease(): void {
    let checkedRows: any[] = this.treeGrid.getCheckedRows();

    if (checkedRows.length > 0) {
      let projectIds: any[] = checkedRows.map(e => {
        return e.projectId;
      });

      this.http
        .get(
          SubleaseEndpoints.root + "/byprojects",
          new HttpParams({
            fromObject: { projectIds: Array.from(new Set(projectIds)) }
          })
        )
        .subscribe(data => (this.sublease = data));

      this.rowModal.show();
    }
  }

  onCheckedSublease(event: any, index: number): void {
    this.sublease.subhires.forEach(e => (e.check = false));
    this.sublease.subhires[index].check = event.target.checked;
    this.canAcceptSublease = event.target.checked;
  }

  createSublease(fromExist?: boolean): void {
    let subleaseEquipment: any[] = [];
    let checkedRows: any[] = this.treeGrid.getCheckedRows();
    if (checkedRows.length > 0) {
      let path: string = SubleaseEndpoints.root + "/fromShortage";

      if (fromExist) {
        let checkedSublease: any = this.sublease.subhires.find(
          f => f.check === true
        );

        if (checkedSublease) {
          path = `${path}/${checkedSublease.id}`;
        } else {
          return;
        }
      }

      subleaseEquipment = checkedRows
        .filter(f => f.equipmentId > 0 && f.shortageQuantity > 0)
        .map(e => {
          return {
            projectId: e.projectId,
            equipmentId: e.equipmentId,
            quantity: e.shortageQuantity
          };
        });

      this.http
        .post(path, {
          dateFrom: this.sublease.dateFrom,
          dateTo: this.sublease.dateTo,
          equipments: subleaseEquipment
        })
        .subscribe(data => {
          this.router.navigate([`/sublease/edit/${data.id}/data`]);
        });
    }
    }

    clearFilterByPeriod() {
        this.filterPeriod.fromDate = null;
        this.filterPeriod.toDate = null;

        this.onChangedFilterByPeriod();
    }

    setDefaultColumns(): void {
        let data: any = {};
        data.moduleEntity = this.tableEntityType;
        data.columns = null;

        this.gridService.setGridColumn(data).then(() => {
            this.loadData();
        });
    }

    onChangeWidjetPeriod(event) {
        this.widjetSwitchPeriod = event;
        this.onChangedFilterByPeriod();
    }

}
