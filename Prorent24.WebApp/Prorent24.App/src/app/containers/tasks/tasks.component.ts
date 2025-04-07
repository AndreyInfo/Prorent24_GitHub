import { Component, OnInit, ViewChild, AfterViewInit } from "@angular/core";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";
import { ModalDirective } from "ngx-bootstrap";
import { TaskModel } from "@models/task/task.model";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { SearchService } from "@services/search.service";

import { TasksEndpoints } from "@endpoints";
import { Entity } from "@shared/enums/entity.enum";
import { NgForm } from "@angular/forms";
import { TableEntity } from "@shared/enums/table-entity.enum";
import { JqxGridModel } from "@shared/models/jqx-grid.model";

@Component({
	selector: "app-tasks",
	templateUrl: "./tasks.component.html"
})
export class TasksComponent extends GridAbstract<TaskModel>
	implements OnInit, AfterViewInit {
	@ViewChild("closeTasksModal", { static: true })
	closeTasksModal: ModalDirective;
	entityType: Entity = Entity.tasks;
	tableEntityType: TableEntity = TableEntity.tasks;
	users: Array<any> = new Array<any>();

	constructor(
		private http: HttpService,
		private toggler: PagesToggleService,
		private search: SearchService,
		public gridService: GridService
	) {
		super(gridService, TaskModel, TasksEndpoints);
		this.entity.id = 0;
	}

	tableSource: JqxGridModel = new JqxGridModel();
	source: any;

	ngOnInit(): void {
		super.ngOnInit();
		this.loadUsers();

		setTimeout(() => {
			this.toggler.setContent("full-height");
			this.toggler.setPageContainer("full-height");
			this.toggler.toggleFooter(false);
		});
	}

	ngAfterViewInit(): void {
		this.dataGrid.onBindingcomplete.subscribe(() => {
			this.dataGrid.addgroup("deadLineGroupName");
			this.dataGrid.expandallgroups();

			let expiredTimes: any[] = this.getRowsByCellValue("expiredTime", true);

			if (expiredTimes.length > 0) {
				Object.getOwnPropertyNames(expiredTimes[0]).forEach(key => {
					this.dataGrid.setcolumnproperty(key, "cellclassname", function(
						index
					) {
						let isHasIndex: any = expiredTimes.find(f => f.uid === index);
						if (isHasIndex) {
							return "bg-danger-lighter";
						}
					});
				});
			}
		});

		this.onChanged.subscribe(() => {
			this.dataGrid.expandallgroups();
		});

		this.onStartEditFull.subscribe((data: TaskModel) => {
			setTimeout(() => {
				this.users = [...data.crewMembers, ...data.executors];
			});
		});
	}

	onShowCloseTask(): void {
		this.entity = this.selected_entity;
		this.closeTasksModal.show();
	}

	submitCloseTask(form: NgForm): void {
		if (form.valid) {
			this.http
				.post(
					StringExt.Format(TasksEndpoints.single, this.selected_entity.id) +
						`/close`,
					form.value
				)
				.subscribe(_ => {
					this.loadData();
					this.closeTasksModal.hide();
				});
		}
	}

	loadUsers(){
		this.search.users("null").subscribe((data: Array<any>) => {
			this.users = [...this.users, ...data];
		});
	}

	onChangeUserSearch(event: any): void {
		let search_val: string = event.target.value;
		if (search_val.length > 2 && search_val.length < 6) {
			this.search.users(search_val).subscribe((data: Array<any>) => {
				if (data.length > 0) {
					console.log("p1 - onChangeUserSearch - " + data[0].lastName);
					data.forEach((e: any, index: number) => {
						let u: any = this.users.find(f => f.id === e.id);
						if (u) {
							data.splice(index, 1);
						}
					});
					this.users = [...this.users, ...data];
					console.log("p2 - onChangeUserSearch - " + data[0].lastName);
				}
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
}
