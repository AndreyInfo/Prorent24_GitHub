import { Component, OnInit, ViewChild, TemplateRef } from "@angular/core";
import { HttpService } from "@core/http.service";
import { ActivatedRoute } from "@angular/router";
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem
} from "@angular/cdk/drag-drop";
import { WarehouseEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import {
  BookingModel,
  BookingEquipment,
  BookingItemModel
} from "@models/warehouse/booking.model";
import { SetType } from "@shared/enums/set-type.enum";
import { ModalDirective } from "ngx-bootstrap";
import { NgForm } from "@angular/forms";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { MovementOptions } from "./movement-option";
import { MatOptionSelectionChange } from '@angular/material';
import { MessageService } from "@ui-components/notification/notification.service";

enum MovementStep {
  pack_packed,
  packed_transportation,
  transportation_mounting,
  mounting_inuse,
  inuse_dismantling,
  dismantling_returned
}

@Component({
  selector: "app-warehouse-booking",
  templateUrl: "./booking.component.html"
})
export class WarehouseBookingComponent implements OnInit {
  @ViewChild("eqSplitModal", { static: false }) eqSplitModal: ModalDirective;

  @ViewChild("pack_packed", { static: true }) pack_packed: TemplateRef<any>;
  @ViewChild("packed_transportation", { static: true }) packed_transportation: TemplateRef<any>;
  @ViewChild("transportation_mounting", { static: true }) transportation_mounting: TemplateRef<
    any
  >;
  @ViewChild("mounting_inuse", { static: true }) mounting_inuse: TemplateRef<any>;
  @ViewChild("inuse_dismantling", { static: true }) inuse_dismantling: TemplateRef<any>;
  @ViewChild("dismantling_returned", { static: true }) dismantling_returned: TemplateRef<any>;

  id: any;
  bookings: BookingModel = new BookingModel();
  movementStep: any = MovementStep;
  selectedBookingStep: string = MovementStep[MovementStep.pack_packed];
  equipmentType: any = SetType;
  splitObj: BookingEquipment = new BookingEquipment();
  scanCode: string = '';

  constructor(
    private toggler: PagesToggleService,
    private activateRoute: ActivatedRoute,
    private http: HttpService,
    private _notification: MessageService
  ) {
    this.id = this.activateRoute.snapshot.params.id;
  }

  ngOnInit(): void {
    this.http
      .get(StringExt.Format(WarehouseEndpoints.booking, this.id))
      .subscribe((data: BookingModel) => {
        Object.assign(this.bookings, data);
      });

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });
  }

  loadTemplate(type: string): TemplateRef<any> {
    switch (type) {
      case MovementStep[MovementStep.pack_packed]:
        return this.pack_packed;
      case MovementStep[MovementStep.packed_transportation]:
        return this.packed_transportation;
      case MovementStep[MovementStep.transportation_mounting]:
        return this.transportation_mounting;
      case MovementStep[MovementStep.mounting_inuse]:
        return this.mounting_inuse;
      case MovementStep[MovementStep.inuse_dismantling]:
        return this.inuse_dismantling;
      case MovementStep[MovementStep.dismantling_returned]:
        return this.dismantling_returned;
      default:
        break;
    }
  }

  scaningCode() {

    switch (this.selectedBookingStep) {
      case "pack_packed": {
        this.bookings.data.pack.forEach(data => {
          if (data.equipments) {
            let index = data.equipments.findIndex(x => x.serialNumbers ? x.serialNumbers.find(s => s.trim().toLowerCase() == this.scanCode.trim().toLowerCase()) : false);
            if (index > -1) {
              let equipment = data.equipments[index];
              let options: MovementOptions = new MovementOptions();
              options.equipment = equipment;
              options.equipment.selectedQuantity = 1;
              options.newState = "packed";
              options.from = "pack";
              options.to = "packed";
              options.index = index;
              this.move(options);
              this.scanCode = "";
              this._notification.create("success", equipment.equipmentName, {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
              return;
            }
            else {
              this._notification.create("danger", "Оборудование не найдено", {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
            }
          }
        });
        break;
      }
      case "packed_transportation": {
        this.bookings.data.packed.forEach(data => {
          if (data.equipments) {
            let index = data.equipments.findIndex(x => x.serialNumbers ? x.serialNumbers.find(s => s.trim().toLowerCase() == this.scanCode.trim().toLowerCase()) : false);
            if (index > -1) {
              let equipment = data.equipments[index];
              let options: MovementOptions = new MovementOptions();
              options.equipment = equipment;
              options.equipment.selectedQuantity = 1;
              options.newState = "transportation";
              options.from = "packed";
              options.to = "transportation";
              options.index = index;
              this.move(options);
              this.scanCode = "";
              this._notification.create("success", equipment.equipmentName, {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
              return;
            }
            else {
              this._notification.create("danger", "Оборудование не найдено", {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
            }
          }
        });
        break;
      }
      case "transportation_mounting": {
        this.bookings.data.transportation.forEach(data => {
          if (data.equipments) {
            let index = data.equipments.findIndex(x => x.serialNumbers ? x.serialNumbers.find(s => s.trim().toLowerCase() == this.scanCode.trim().toLowerCase()) : false);
            if (index > -1) {
              let equipment = data.equipments[index];
              let options: MovementOptions = new MovementOptions();
              options.equipment = equipment;
              options.equipment.selectedQuantity = 1;
              options.newState = "mounting";
              options.from = "transportation";
              options.to = "mounting";
              options.index = index;
              this.move(options);
              this.scanCode = "";
              this._notification.create("success", equipment.equipmentName, {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
              return;
            }
            else {
              this._notification.create("danger", "Оборудование не найдено", {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
            }
          }
        });
        break;
      }
      case "mounting_inuse": {
        this.bookings.data.mounting.forEach(data => {
          if (data.equipments) {
            let index = data.equipments.findIndex(x => x.serialNumbers ? x.serialNumbers.find(s => s.trim().toLowerCase() == this.scanCode.trim().toLowerCase()) : false);
            if (index > -1) {
              let equipment = data.equipments[index];
              let options: MovementOptions = new MovementOptions();
              options.equipment = equipment;
              options.equipment.selectedQuantity = 1;
              options.newState = "inUse";
              options.from = "mounting";
              options.to = "inUse";
              options.index = index;
              this.move(options);
              this.scanCode = "";
              this._notification.create("success", equipment.equipmentName, {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
              return;
            }
            else {
              this._notification.create("danger", "Оборудование не найдено", {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
            }
          }
        });
        break;
      }
      case "inuse_dismantling": {
        this.bookings.data.inUse.forEach(data => {
          if (data.equipments) {
            let index = data.equipments.findIndex(x => x.serialNumbers ? x.serialNumbers.find(s => s.trim().toLowerCase() == this.scanCode.trim().toLowerCase()) : false);
            if (index > -1) {
              let equipment = data.equipments[index];
              let options: MovementOptions = new MovementOptions();
              options.equipment = equipment;
              options.equipment.selectedQuantity = 1;
              options.newState = "dismantling";
              options.from = "inUse";
              options.to = "dismantling";
              options.index = index;
              this.move(options);
              this.scanCode = "";
              this._notification.create("success", equipment.equipmentName, {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
              return;
            }
            else {
              this._notification.create("danger", "Оборудование не найдено", {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
            }
          }
        });
        break;
      }
      case "dismantling_returned": {
        this.bookings.data.dismantling.forEach(data => {
          if (data.equipments) {
            let index = data.equipments.findIndex(x => x.serialNumbers ? x.serialNumbers.find(s => s.trim().toLowerCase() == this.scanCode.trim().toLowerCase()) : false);
            if (index > -1) {
              let equipment = data.equipments[index];
              let options: MovementOptions = new MovementOptions();
              options.equipment = equipment;
              options.equipment.selectedQuantity = 1;
              options.newState = "returned";
              options.from = "dismantling";
              options.to = "returned";
              options.index = index;
              this.move(options);
              this.scanCode = "";
              this._notification.create("success", equipment.equipmentName, {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
              return;
            }
            else {
              this._notification.create("danger", "Оборудование не найдено", {
                Position: "top-right",
                Style: "flip",
                Duration: 5000
              });
            }
          }
        });
        break;
      }
    }
  }

  nextStep(step: MovementStep): void {
    this.selectedBookingStep = MovementStep[step];
  }

  //#region movement
  move(options: MovementOptions): void {
    if (
      options.equipment.selectedQuantity > options.equipment.limitQuantity ||
      options.equipment.selectedQuantity <= 0
    ) {
      return;
    }

    let _equipment: BookingEquipment = new BookingEquipment();
    Object.assign(_equipment, options.equipment);

    _equipment.movementStatus = options.newState;

    if (options.kit) {
      let _kit: BookingEquipment = new BookingEquipment();
      Object.assign(_kit, options.kit);

      _kit.kitCaseEquipments = [];
      _kit.kitCaseEquipments.push(_equipment);
      _kit.movementStatus = options.newState;
      _equipment = _kit;
    }

    this.http
      .post(
        StringExt.Format(WarehouseEndpoints.booking_movement, this.id),
        _equipment
      )
      .subscribe((response: BookingEquipment) => {
        response.serialNumbers = options.equipment.serialNumbers;
        let _toEquipments: Array<BookingEquipment> = this.bookings.data[
          options.to
        ].find((f: any) => f.groupId === options.equipment.groupId).equipments;

        let indexEq: any = _toEquipments.findIndex(
          (f: any) => f.id === response.id
        );

        // add or update equipment
        if (indexEq >= 0) {
          _toEquipments[indexEq] = response;
        } else {
          _toEquipments.unshift(response);
        }

        // remove from state collection or update quantity
        if (options.kit) {
          if (
            options.equipment.selectedQuantity ===
            options.equipment.limitQuantity
          ) {
            options.kit.kitCaseEquipments.splice(options.index, 1);
            if (options.kit.kitCaseEquipments.length === 0) {
              this.bookings.data[options.from]
                .find((f: any) => f.groupId === options.kit.groupId)
                .equipments.splice(options.kitIndex, 1);
            }
          } else {
            options.equipment.selectedQuantity =
              options.equipment.limitQuantity -
              options.equipment.selectedQuantity;

            options.equipment.limitQuantity =
              options.equipment.selectedQuantity;
          }
        } else {
          if (
            options.equipment.selectedQuantity ===
            options.equipment.limitQuantity
          ) {
            this.bookings.data[options.from]
              .find((f: any) => f.groupId === options.equipment.groupId)
              .equipments.splice(options.index, 1);
          } else {
            options.equipment.selectedQuantity =
              options.equipment.limitQuantity -
              options.equipment.selectedQuantity;

            options.equipment.limitQuantity =
              options.equipment.selectedQuantity;
          }
        }
      });
  }

  moveAll(from: string, to: string, toState: string): void {
    let movement_equipments: Array<BookingItemModel> = this.bookings.data[from];
    movement_equipments.forEach(i => {
      i.movementStatus = toState;
      i.equipments.forEach(j => {
        j.movementStatus = toState;
        j.selectedQuantity = j.limitQuantity;
        if (j.equipmentType !== "item") {
          j.kitCaseEquipments.forEach(k => {
            k.movementStatus = toState;
            k.selectedQuantity = k.limitQuantity;
          });
        }
      });
    });

    this.http
      .post(
        StringExt.Format(WarehouseEndpoints.booking_movements, this.id),
        movement_equipments
      )
      .subscribe((response: Array<any>) => {
        this.bookings.data[to] = response;

        let _from: Array<BookingItemModel> = this.bookings.data[from];
        _from.forEach(i => (i.equipments = []));
      });
  }
  //#endregion

  //#region spliter
  openSplitForm(_package: string, equipment: any): void {
    equipment.package = _package;
    Object.assign(this.splitObj, equipment);
    this.eqSplitModal.show();
  }

  onSplitSubmit(form: NgForm): void {
    if (form.valid) {
      let tempEq: BookingEquipment = new BookingEquipment();
      let copy_equipment: BookingEquipment = new BookingEquipment();

      Object.assign(tempEq, form.value);

      if (
        tempEq.selectedQuantity > 0 &&
        tempEq.selectedQuantity < tempEq.totalQuantity
      ) {
        let _package: Array<BookingItemModel> = this.bookings.data[
          tempEq.package
        ];
        let group: BookingItemModel = _package.find(
          f => f.groupId === tempEq.groupId
        );
        let equipmentIndex: number = group.equipments.findIndex(
          f => f.equipmentId === tempEq.equipmentId
        );

        Object.assign(copy_equipment, group.equipments[equipmentIndex]);

        group.equipments[equipmentIndex].totalQuantity =
          group.equipments[equipmentIndex].totalQuantity -
          tempEq.selectedQuantity;
        copy_equipment.totalQuantity = tempEq.selectedQuantity;
        copy_equipment.selectedQuantity = tempEq.selectedQuantity;

        group.equipments.splice(equipmentIndex + 1, 0, copy_equipment);
        this.eqSplitModal.hide();
      }
    }
  }
  //#endregion

  drop(event: CdkDragDrop<string[]>): void {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      let eq: BookingEquipment = new BookingEquipment();
      eq.groupId = event.item.data.groupId;
      eq.equipmentId = event.item.data.equipmentId;
      eq.movementStatus = event.container.id;
      eq.totalQuantity = event.item.data.totalQuantity;
      eq.selectedQuantity = event.item.data.selectedQuantity;

      this.http
        .post(
          StringExt.Format(WarehouseEndpoints.booking_movement, this.id),
          eq
        )
        .subscribe();

      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }
}
