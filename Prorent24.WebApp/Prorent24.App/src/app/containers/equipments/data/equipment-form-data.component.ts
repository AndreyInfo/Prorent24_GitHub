import {
  Component,
  OnInit,
  ViewChild,
  Injector,
  Output,
  EventEmitter
} from "@angular/core";
import { TreeFoldersComponent } from "@components/tree-folders/tree-folders.component";
import { Entity } from "@shared/enums/entity.enum";
import { EquipmentModel } from "@models/equipments/equipment.model";
import { SupplyType } from "@shared/enums/supply-type.enum";
import { StockManagement } from "@shared/enums/stock-management.enum";
import { QuantityMode } from "@shared/enums/quantity-mode.enum";
import { SetType } from "@shared/enums/set-type.enum";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { FormAbstract } from "@abstractions/form.abstraction";
import { EquipmentsEndpoints } from "@endpoints";
import { KitType } from "@shared/enums/kit.enum";
import { TransportationType } from "@shared/enums/transportation-type.enum";
import { DirectoryService } from "@services/directory.service";
import { LenghtUnit } from "@shared/enums/lenght-unit.enum";
import { WeightUnit } from "@shared/enums/weight-unit.enum";
import { EqTabEventService } from '../eq-tab-event.service';

@Component({
  selector: "app-equipment-form-data",
  templateUrl: "./equipment-form-data.component.html"
})
export class EquipmentDataComponent extends FormAbstract<EquipmentModel>
  implements OnInit {
  @ViewChild(TreeFoldersComponent, { static: true }) foldersModal: TreeFoldersComponent;

  @Output() onChangeEqType = new EventEmitter<any>();
  changeEqTypeEvent(type: any): void {
    this.onChangeEqType.emit(type);
  }

  supplyType: any = SupplyType;
  setType: any = SetType;
  stockManagementType = StockManagement;
  quantityModeType: any = QuantityMode;
  kitType: any = KitType;
  transportationType: any = TransportationType;
  discountGroups: Array<any>;
  //factorGroups: Array<any>;
  vatTypes: Array<any>;
  measuringUnits: Array<any>;
  lenghtUnits: any = LenghtUnit;
  weightUnit: any = WeightUnit;

  constructor(
    public injector: Injector,
    private directory: DirectoryService,
    private customDirectory: CustomDirectoryService,
    public eqTabEventService: EqTabEventService
  ) {
    super(
      injector,
      Entity.equipment,
      EquipmentModel,
      EquipmentsEndpoints,
      true
    );

    this.customDirectory
      .getDiscountGroups()
      .subscribe(data => (this.discountGroups = data));

    // this.customDirectory
    //   .getFactorGroups()
    //   .subscribe(data => (this.factorGroups = data));

    this.customDirectory
      .getVatClasses()
      .subscribe(data => (this.vatTypes = data));

    /*  this.directory
      .getMeasuringUnits()
      .subscribe(data => (this.measuringUnits = data)); */
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  changeEqType(type: any): void {
    this.changeEqTypeEvent(type);
    this.eqTabEventService.emitChange(type);
  }

  calcVolume(): void {
    let a: number = this.convertToСentimeter(
      this.entity.length,
      this.entity.lengthUnit
    );
    let b: number = this.convertToСentimeter(
      this.entity.width,
      this.entity.widthUnit
    );
    let c: number = this.convertToСentimeter(
      this.entity.height,
      this.entity.heightUnit
    );

    this.entity.volume = parseFloat(((a * b * c) / 1000000).toFixed(4));
  }

  setFolder(folder: any): void {
    this.entity.folderId = folder.id;
    this.entity.folderName = folder.name;

    /*     if (this.id > 0) {
      this.http
        .post(
          StringExt.Format(EquipmentsEndpoints.single, this.id) +
            `/set_folder/${this.entity.folderId}`
        )
        .subscribe();
    } */
  }

  private convertToСentimeter(value: number, unit: any): number {
    let cm: number = 0;

    let _unit: LenghtUnit = LenghtUnit[LenghtUnit[unit]];

    switch (_unit) {
      case LenghtUnit.cm:
        cm = value;
        break;
      case LenghtUnit.m:
        cm = value * 100;
        break;
      case LenghtUnit.km:
        cm = value * 100000;
        break;
      case LenghtUnit.mm:
        cm = value / 10;
        break;
    }
    
    return cm;
  }
}
