import {
	Component,
	OnInit,
	ViewChild,
	Injector,
	AfterViewInit
} from "@angular/core";
import { NgForm } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { TreeGridAbstract } from "@abstractions/tree-grid.abstraction";
import {
	ProjectEquipmentGroupsEndpoints,
	ProjectEquipmentEndpoints,
	ProjectsEndpoints
} from "@endpoints";
import { ProjectEquipmentModel } from "@models/project/project-equipments.model";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { ModalDirective } from "ngx-bootstrap";
import { EquipmentGroupModel } from "@models/common/equipment-group.model";
import { ProjectModel } from "@models/project/project.model";
import { ProjectTime } from "@models/project/project-time.model";
import { TableEntity } from "@shared/enums/table-entity.enum";
import { Entity } from "@shared/enums/entity.enum";

@Component({
	selector: "app-project-equipments",
	templateUrl: "./project-equipments.component.html"
})
export class ProjectEquipmentsComponent
	extends TreeGridAbstract<ProjectEquipmentModel>
	implements OnInit, AfterViewInit {
	@ViewChild("groupModal", { static: true }) groupModal: ModalDirective;
	parentId: any;

	vatTypes: Array<any>;
	group: EquipmentGroupModel = new EquipmentGroupModel();
	tableEntityType: TableEntity = TableEntity.project_equipments;

	defaultPeriodUseStart: Date;
	defaultPeriodUseEnd: Date;
	defaultPeriodPlanningStart: Date;
	defaultPeriodPlanningEnd: Date;

	constructor(
		public injector: Injector,
		private toggler: PagesToggleService,
		private customDirectory: CustomDirectoryService,
		private http: HttpService,
		private route: ActivatedRoute
	) {
		super(injector, ProjectEquipmentModel, ProjectEquipmentEndpoints);
		this.entityType = Entity.project_equipments;
		this.parentId = this.route.parent.snapshot.params.id;
	}

	ngOnInit(): void {
		this.loadSubData(this.parentId);

		this.http
			.get(StringExt.Format(ProjectsEndpoints.single, this.parentId))
			.subscribe((data: ProjectModel) => {
				let usePeriod: ProjectTime = data.times.find(f => f.defaultUsagePeriod);
				if (usePeriod) {
					this.defaultPeriodUseStart = usePeriod.from;
					this.defaultPeriodUseEnd = usePeriod.until;
				}

				let planningPeriod: ProjectTime = data.times.find(
					f => f.defaultPlanPeriod
				);
				if (planningPeriod) {
					this.defaultPeriodPlanningStart = planningPeriod.from;
					this.defaultPeriodPlanningEnd = planningPeriod.until;
				}
			});

		setTimeout(() => {
			this.toggler.setContent("full-height");
			this.toggler.setPageContainer("full-height");
			this.toggler.toggleFooter(false);
		});

		this.customDirectory
			.getVatClasses()
			.subscribe(data => (this.vatTypes = data));
	}

	ngAfterViewInit(): void {
		this.treeGrid.onBindingComplete.subscribe(() => {
			this.treeGrid.setColumnProperty("groupName", "width", 150);

			let absentEquipments: any[] = [];

			this.treeGrid.getRows().forEach((r: any) => {
				this.treeGrid.expandRow(r["uid"]);

				r.childrens.forEach((i: any) => {
					if (i.quantity > i.availableQuantity) {
						absentEquipments.push(i);
					}
				});
			});

			if (absentEquipments.length > 0) {
				Object.getOwnPropertyNames(absentEquipments[0]).forEach(key => {
					this.treeGrid.setColumnProperty(key, "cellclassname", function(
						row,
						dataField,
						cellValueInternal,
						rowData,
						cellText
					) {
						let isHasIndex: any = absentEquipments.find(
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

	//#region group equipments
	addGroup(): void {
		this.group = new EquipmentGroupModel();
		this.group.startUsePeriod = this.defaultPeriodUseStart;
		this.group.endUsePeriod = this.defaultPeriodUseEnd;
		this.group.startPlanPeriod = this.defaultPeriodPlanningStart;
		this.group.endPlanPeriod = this.defaultPeriodPlanningEnd;

		this.groupModal.show();
	}

	submitGroup(form: NgForm): void {
		if (form.valid) {
			if (form.value.id) {
				this.http
					.post(
						StringExt.Format(
							ProjectEquipmentGroupsEndpoints.single,
							this.parentId,
							form.value.id
						),
						form.value
					)
					.subscribe(
						data => {
							console.log(this.selected_entity["uid"]);
							this.treeGrid.updateRow(this.selected_entity["uid"], {
								data
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
							ProjectEquipmentGroupsEndpoints.root,
							this.parentId
						),
						form.value
					)
					.subscribe(
						data => {
							this.treeGrid.addRow(null, data, "last");
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
		let group: any = this.treeGrid.getRow(groupUid);

		if (group.records) {
			equipmentExist = group["records"].find(
				(f: any) => f.equipmentId === equipment.equipmentId
			);
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
					ProjectEquipmentEndpoints.single,
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

				this.treeGrid.expandRow(groupUid);

				if (data.quantity > data.availableQuantity) {
					this.treeGrid.refresh();
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

	deleteRows(): void {
		let rows: any[] = this.treeGrid.getCheckedRows();
		let groupIds: any[] = [];
		rows.forEach((item: any) => {
			if (item.level === 0) {
				groupIds.push({
					uid: item.uid,
					id: item.id
				});
			}

			if (item.level > 0) {
				this.grid.deleteRow(item.id, this.parentId).then(_ => {
					this.treeGrid.deleteRow(item.uid);
				});
			}
		});

		groupIds.forEach(g => {
			this.http
				.post(
					StringExt.Format(
						ProjectEquipmentGroupsEndpoints.delete,
						this.parentId,
						g.id
					)
				)
				.subscribe(null, null, () => {
					this.treeGrid.deleteRow(g.uid);
				});
		});

		this.changeRemoveArrayMode();
	}

	loadShoratges(): void {
		this.http
			.get(StringExt.Format(ProjectEquipmentEndpoints.shortages, this.parentId))
			.subscribe(
				data => {
					/*console.log(this.selected_entity["uid"]);
					this.treeGrid.updateRow(this.selected_entity["uid"], {
						data
          });*/
					console.log(data);
				},
				null,
				() => {
					// this.groupModal.hide();
				}
			);
	}
}
