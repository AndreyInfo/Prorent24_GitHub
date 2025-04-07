import { Component, OnInit, AfterViewInit, ViewChild } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { EquipmentModel } from "@models/equipments/equipment.model";
import { Entity } from "@shared/enums/entity.enum";
import { ExcelComponent } from "@components/excel/excel.component";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { GridService } from "@services/grid.service";
import { EquipmentsEndpoints } from "@endpoints";
import { TableEntity } from "@shared/enums/table-entity.enum";

@Component({
  selector: "app-equipments",
  templateUrl: "./equipments.component.html"
})
export class EquipmentsComponent extends GridAbstract<EquipmentModel>
  implements OnInit, AfterViewInit {
  entityType: Entity = Entity.equipment;
  tableEntityType: TableEntity = TableEntity.equipment;

  @ViewChild(ExcelComponent, { static: true }) excelModal: ExcelComponent;

  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(gridService, EquipmentModel, EquipmentsEndpoints);
  }

  ngOnInit(): void {
    this.loadData(this.filter);

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });
  }

  ngAfterViewInit(): void {
    this.dataGrid.onBindingcomplete.subscribe(() => {
      this.dataGrid.setcolumnproperty(
        "availableQuantity",
        "cellclassname",
        (index: number, columnfield: string, value: any) => {
          if (value === 0) {
            return "bg-danger-lighter";
          }
        }
      );
    });
    
    }

setDefaultColumns(): void {
    let data: any = {};
    data.moduleEntity = this.tableEntityType;
    data.columns = null;

    this.gridService.setGridColumn(data).then(() => {
        this.loadData();
    });
}

}
