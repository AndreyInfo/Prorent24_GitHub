import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { SupplierModel } from "@models/equipments/supplier.model";
import { EquipmentsSuppliersEndpoints } from "@endpoints";
import { CustomDirectoryService } from "@services/custom-directory.service";

@Component({
  selector: "app-equipment-suppliers",
  templateUrl: "./equipment-suppliers.component.html"
})
export class EquipmentSuppliersComponent extends GridAbstract<SupplierModel>
  implements OnInit {
  parentId: any;

  constructor(
    public gridService: GridService,
    private customDirectory: CustomDirectoryService,
    private route: ActivatedRoute
  ) {
    super(gridService, SupplierModel, EquipmentsSuppliersEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  contacts: Array<any>;

  ngOnInit(): void {
    this.loadSubData(this.parentId);

    this.customDirectory
      .getContacts()
      .subscribe(data => (this.contacts = data));
  }
}
