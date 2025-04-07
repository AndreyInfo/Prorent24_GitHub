import {
  Component,
  OnInit,
  ViewChild,
  Output,
  EventEmitter,
  Input,
  ElementRef
} from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { EquipmentModel } from "@models/equipments/equipment.model";
import { EquipmentCatalogEndpoints } from "@endpoints";
import { jqxGridComponent } from "jqwidgets-ng/jqxgrid";
import { filter } from "rxjs/operators/filter";
import { FilterModel } from "@models/common/filter.model";
import { FilterType } from "@shared/enums/filter-type.enum";

@Component({
  selector: "rent-equipments-catalog",
  templateUrl: "./equipment-catalog.component.html",
  providers: [GridService]
})
export class EquipmentsCatalogComponent extends GridAbstract<EquipmentModel>
  implements OnInit {
  constructor(private gridService: GridService) {
    super(gridService, EquipmentModel, EquipmentCatalogEndpoints);
  }

  filters: Array<FilterModel>;
  euipmentGroups: any;
  innerWidth: any;
  @ViewChild("eqCatalogGrid", { static: true }) eqCatalogGrid: jqxGridComponent;

  @Input() currentEquipment: any;

  @Output() onAddRow = new EventEmitter<any>();
  addRowEvent(data: any): void {

    this.onAddRow.emit(data);
  }

  ngOnInit(): void {
    this.loadEquipmentCatalog();
    this.innerWidth = window.innerWidth;
    // this.eqCatalogGrid.onBindingcomplete.subscribe(() => {
    //   this.eqCatalogGrid.addgroup("category");
    //   this.eqCatalogGrid.expandallgroups();

    //   // this.eqCatalogGrid.setcolumnproperty("category", "hidden", "true");
    // });
  }

loadEquipmentCatalog(){
  this.grid.getDataGrid(this.filters).then((data: any) => {
    this.euipmentGroups = data.source.dataGroups;
  });
}

  searchByText(value){
    this.filters = new Array<FilterModel>();
    let filterSearch = new FilterModel();
    filterSearch.filterType = FilterType.searchText;
    filterSearch.values = [];
    filterSearch.values.push(value.target.value);
    this.filters.push(filterSearch);
    this.loadEquipmentCatalog();
  }


  getLengthTrancateText(){
    let standartWidth = 1280;
    let standartLength = 28;

    if(this.innerWidth <= standartWidth){
      return standartLength;
    }
    else{
      let customWidth = this.innerWidth - standartWidth;
      let res = (customWidth/25);
      if(res > 1){
        return Math.round(standartLength+res);
      }
      else{
        return standartLength;
      }
    }
  }

  select(event) {

  }
}
