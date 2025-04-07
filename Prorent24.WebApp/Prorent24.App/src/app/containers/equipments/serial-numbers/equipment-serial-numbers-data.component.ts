import { Component, OnInit, Injector, Input } from "@angular/core";
import { FormAbstract } from "@abstractions/form.abstraction";
import { SerialNumberModel } from "@models/equipments/serial-number.model";
import { Entity } from "@shared/enums/entity.enum";
import { EquipmentsSerialNumbersEndpoints } from "@endpoints";
import { ContactModel } from "@models/contacts/contact.model";
import { NgForm } from "@angular/forms";

@Component({
  selector: "app-equipment-serial-numbers-data",
  templateUrl: "./equipment-serial-numbers-data.component.html"
})
export class EquipmentSerialNumbersDataComponent
  extends FormAbstract<SerialNumberModel>
  implements OnInit {
  supplier_Form: ContactModel = new ContactModel();

  constructor(private injector: Injector) {
    super(
      injector,
      Entity.equipment,
      SerialNumberModel,
      EquipmentsSerialNumbersEndpoints,
      true
    );

    this.parentId = this.activateRoute.parent.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  addSupplier(form: NgForm): void {
    if (form.valid) {
      this.entity.suppliersInfo.push(form.value);
      this.supplier_Form = new ContactModel();
    }
  }

  removeSupplier(index: number): void {
    this.entity.suppliersInfo.splice(index, 1);
  }
}
