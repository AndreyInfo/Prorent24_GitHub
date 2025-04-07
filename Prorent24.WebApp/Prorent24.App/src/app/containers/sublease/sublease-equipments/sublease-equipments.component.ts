import { Component, OnInit, ViewChild, Injector } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { TreeGridAbstract } from "@abstractions/tree-grid.abstraction";
import { SubleaseEquipmentModel } from "@models/sublease/sublease-equipment.model";
import {
  SubleaseEquipmentEndpoints,
  SubleaseEquipmentGroupsEndpoints
} from "@endpoints";
import { ModalDirective } from "ngx-bootstrap";
import { NgForm } from "@angular/forms";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";
import { EquipmentGroupModel } from "@models/common/equipment-group.model";

@Component({
  selector: "app-sublease-equipments",
  templateUrl: "./sublease-equipments.component.html"
})
export class SubleaseEquipmentsComponent
  extends TreeGridAbstract<SubleaseEquipmentModel>
  implements OnInit {
  @ViewChild("groupModal", { static: true }) groupModal: ModalDirective;
  parentId: any;

  group: EquipmentGroupModel = new EquipmentGroupModel();

  constructor(
    public injector: Injector,
    private toggler: PagesToggleService,
    private route: ActivatedRoute,
    private http: HttpService
  ) {
    super(injector, SubleaseEquipmentModel, SubleaseEquipmentEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.loadSubData(this.parentId);

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });

    this.treeGrid.onBindingComplete.subscribe(() => {
      this.treeGrid.setColumnProperty("groupName", "width", 150);

      this.treeGrid.getRows().forEach(r => {
        this.treeGrid.expandRow(r["uid"]);
      });
    });
  }

  //#region group equipments
  addGroup(): void {
    this.group = new EquipmentGroupModel();
    this.groupModal.show();
  }

  submitGroup(form: NgForm): void {
    if (form.valid) {
      if (form.value.id) {
        this.http
          .post(
            StringExt.Format(
              SubleaseEquipmentGroupsEndpoints.single,
              this.parentId,
              form.value.id
            ),
            form.value
          )
          .subscribe(
            data => {
              this.treeGrid.updateRow(this.selected_entity["uid"], {
                groupId: data.id,
                groupName: data.name,
                records: []
              });
            },
            null,
            () => {
              this.groupModal.hide();
            }
          );
      } else {
        this.http
          .post(
            StringExt.Format(
              SubleaseEquipmentGroupsEndpoints.root,
              this.parentId
            ),
            form.value
          )
          .subscribe(
            data => {
              this.treeGrid.addRow(
                null,
                {
                  groupId: data.id,
                  groupName: data.name
                },
                "last"
              );
            },
            null,
            () => {
              this.groupModal.hide();
            }
          );
      }
    }
  }
  //#endregion

  //#region equipments
  addRow(data: any): void {
    let equipment: any = {
      equipmentId: data.id,
      groupId: this.selected_entity.groupId,
      quantity: 1,
      factor: 1
    };

    let groupUid: any =
      this.selected_entity["level"] === 0
        ? this.selected_entity["uid"]
        : this.selected_entity["parent"]["uid"];
    let equipmentExistId: number;

    let equipmentExist: any;

    let row: any = this.treeGrid.getRow(groupUid);

    if (row.records) {
      equipmentExist = this.treeGrid
        .getRow(groupUid)
        .records.find(f => f["equipmentId"] === equipment.equipmentId);
    }

    if (equipmentExist) {
      equipmentExistId = equipmentExist.id;
      equipment.quantity = equipment.quantity + equipmentExist.quantity;
      equipment.price = equipmentExist.price;
      equipment.factor = equipmentExist.factor;
      equipment.discount = equipmentExist.discount;
    }

    this.http
      .post(
        StringExt.Format(
          SubleaseEquipmentEndpoints.single,
          this.parentId,
          equipmentExistId
        ),
        equipment
      )
      .subscribe(data => {
        if (equipmentExist) {
          this.treeGrid.updateRow(equipmentExist["uid"], data);
        } else {
          data.records = data.childrens;
          this.treeGrid.addRow(null, data, "last", groupUid);
        }
      });
  }
  //#endregion

  onEditModal(id?: any): void {
    if (id) {
      if (this.selected_entity["level"] === 0) {
        Object.assign(this.group, this.selected_entity);
        this.groupModal.show();
      }
      if (this.selected_entity["level"] === 1) {
        this.entity = this.selected_entity;
        this.rowModal.show();
      }
    }
  }
}
