import {
	Component,
	OnInit,
	Injector,
	AfterViewInit,
	ViewChild
} from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { TreeGridAbstract } from "@abstractions/tree-grid.abstraction";
import { Entity } from "@shared/enums/entity.enum";
import { ProjectPlanningEndpoints, CrewPlannerEndpoints } from "@endpoints";
import { ProjectPlanningModel } from "@models/project/project-planning.model";
import { ProjectPlanningFilter } from "@models/project/project-planning-filter.model";
import { ProjectFunctionType } from "@shared/enums/project-function-type.enum";
import { PlanningTransportType } from "@shared/enums/planning-transport-type.enum";
import { PlanningCrewMemberRateType } from "@shared/enums/planning-crewmember-rate-type.enum";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { SchedulerComponent } from "@components/sheduler/sheduler.component";
import { ShedulerModel } from "@shared/models/sheduler.model";
import { ShedulerEventAction } from "@shared/enums/sheduler-event-action.enum";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";

@Component({
	selector: "app-project-planning",
	templateUrl: "./project-planning.component.html"
})
export class ProjectPlanningComponent
	extends TreeGridAbstract<ProjectPlanningModel>
	implements OnInit, AfterViewInit {
	@ViewChild("sheduler", { static: true }) sheduler: SchedulerComponent;

	entityType: Entity = Entity.projects;
	planning_filter: ProjectPlanningFilter = new ProjectPlanningFilter();

	transportType: any = PlanningTransportType;
	crewMemberRateType: any = PlanningCrewMemberRateType;
	crewMemberRates: Array<any> = new Array<any>();
	functionType: any = ProjectFunctionType;

	shedulerAvailabilitiesSource: ShedulerModel;

	checkedFunctions: any = {
		type: "",
		functions: []
	};

	// controls
	showTimeline: boolean = true;
	shedulerDate: Date;

	constructor(
		public injector: Injector,
		private http: HttpService,
		private toggler: PagesToggleService,
		private customDirectory: CustomDirectoryService,
		private route: ActivatedRoute
	) {
		super(injector, ProjectPlanningModel, ProjectPlanningEndpoints);

		this.parentId = this.route.parent.snapshot.params.id;

		this.panelOptions = {
			mainSplitter: [] = [
				{ size: 300, min: 300, collapsible: true },
				{ size: "100%", collapsible: true }
			],
			nestedSplitter: [] = [
				{ size: "60%", collapsible: true },
				{ size: "40%", collapsed: false }
			]
		};
	}

	ngOnInit(): void {
		this.loadSubData(this.parentId, this.planning_filter);

		this.onDataLoadComplete.subscribe((data: any) => {
			let arrayRows: Array<any> = data.source.localdata;
			if (arrayRows.length > 0) {
				let dates: number[] = arrayRows.map((i: any) => {
					return new Date(i.functionPlanFrom).getTime();
				});

				let minDate = Math.min(...dates);
				this.shedulerDate = new Date(minDate);
			}
		});

		setTimeout(() => {
			this.toggler.setContent("full-height");
			this.toggler.setPageContainer("full-height");
			this.toggler.toggleFooter(false);
		});
	}

	ngAfterViewInit(): void {
		this.treeGrid.onBindingComplete.subscribe(() => {
			this.treeGrid.setColumnProperty("name", "width", 200);
			this.treeGrid.setColumnProperty("functionQuantity", "width", 80);
			this.treeGrid.setColumnProperty("functionQuantity", "text", " ");
			this.treeGrid.setColumnProperty(
				"functionQuantity",
				"cellsAlign",
				"center"
			);
			this.treeGrid.setColumnProperty(
				"visibleToCrewMember",
				"cellsAlign",
				"center"
			);
			this.treeGrid.setColumnProperty("projectLeader", "cellsAlign", "center");

			this.treeGrid.getRows().forEach((row: any) => {
				this.treeGrid.expandRow(row.uid);
				this.setGroupQuantity(row);
				row.records.forEach((r: any) => {
					this.setGridIcons(r);
					this.setGridFunctionIcons(r);
				});
			});
		});
	}

	changeTab(index: number): void {
		switch (index) {
			case 0:
				this.planning_filter.type = ProjectFunctionType.crew;
				break;
			case 1:
				this.planning_filter.type = ProjectFunctionType.transport;
				break;
		}

		this.loadSubData(this.parentId, this.planning_filter);
	}

	onShowEditFunctionModal(event: any): void {
		if (this.selected_entity["functionType"] === this.functionType.crew) {
			this.customDirectory
				.getCrewMemberRates(this.selected_entity.entityId)
				.subscribe(data => (this.crewMemberRates = data));
		}
	}

	toggleTimeline(): void {
		this.showTimeline = !this.showTimeline;
		if (this.showTimeline) {
			this.nestedSplitter.expand();
		} else {
			this.nestedSplitter.collapse();
		}
	}

	onSelectFunction(event: any): void {
		if (this.selected_entity) {
			let group: any =
				this.selected_entity["level"] === 0
					? this.selected_entity
					: this.selected_entity["parent"];

			let func: any = {
				type: event.type,
				functionId: group.id,
				entityId: event.id
			};

			this.onSubmitCustomData(func).then((data: any) => {
				this.treeGrid.addRow(null, data, "last", group["uid"]);
				this.treeGrid.expandRow(group["uid"]);
				this.setGridIcons(data);
				this.setGridFunctionIcons(data);
				this.setGroupQuantity(data.parent);
			});
		}
	}

	onCheckedFunctions(event: any): void {
		Object.assign(this.checkedFunctions, event);
		this.checkedFunctions.functions.forEach((f: any) => (f.selected = "true"));
		this.loadAccessibilityShedulerData();
	}

	onPlanning(): void {
		debugger
		let group: any =
			this.selected_entity["level"] === 0
				? this.selected_entity
				: this.selected_entity["parent"];

		let existFunctions: any[] = group.childrens;

		this.checkedFunctions.functions.forEach((f: any) => {
			let isExist: any = existFunctions.find((ef: any) => ef.entityId === f.id);

			if (isExist == undefined) {
				let func: any = {
					type: this.checkedFunctions.type,
					functionId: group.id,
					entityId: f.id
				};

				this.onSubmitCustomData(func).then((data: any) => {
					this.treeGrid.addRow(null, data, "last", group["uid"]);
					this.setGridIcons(data);
					this.setGridFunctionIcons(data);
					this.setGroupQuantity(data.parent);
				});
			}
		});
	}

	loadAccessibilityShedulerData(): void {
		let functions: [] = this.checkedFunctions.functions;
		if (functions.length > 0) {
			let ids: any[] = functions.map(e => {
				return e["id"];
			});

			this.http
				.post(
					StringExt.Format(
						CrewPlannerEndpoints.scheduler,
						this.checkedFunctions.type
					),
					ids
				)
				.subscribe((data: ShedulerModel) => {
					data.resources.forEach(e => {
						e.availabilities.forEach(a => {
							a.color = this.getAvailabilityColor(a.type);
						});
					});

					data.events.forEach((e: any) => {
						e.fontColor = "#fff";
						e.backColor = "#3C93CE";
					});

					this.shedulerAvailabilitiesSource = data;
				});
		}
	}

	private setGroupQuantity(row: any): void {
		let colorStyle: string =
			row.records.length < row.quantity
				? `style="color: #F55753;"`
				: `style="color: #3B4752;"`;

		if (row.functionType === "Crew") {
			this.treeGrid.setCellValue(
				row.uid,
				"functionQuantity",
				`<span ${colorStyle}><span>${row.records.length} / ${row.quantity}</span><i class="fa fa-user ml-2"></i></span>`
			);
		}
		if (row.functionType === "Transport") {
			this.treeGrid.setCellValue(
				row.uid,
				"functionQuantity",
				`<span ${colorStyle}><span>${row.records.length} / ${row.quantity}</span><i class="fa fa-truck ml-2"></i></span>`
			);
		}
	}

	private setGridIcons(row: any): void {
		let trueIcon: string = `<i class="fa fa-check hint-text"></i>`;
		let falseIcon: string = `<i class="fa fa-times hint-text"></i>`;

		this.treeGrid.setCellValue(
			row.uid,
			"visibleToCrewMember",
			row.visibleToCrewMember === true ? trueIcon : falseIcon
		);
		this.treeGrid.setCellValue(
			row.uid,
			"projectLeader",
			row.projectLeader === true ? trueIcon : falseIcon
		);
	}

	private setGridFunctionIcons(row: any): void {
		let typeIcon: string;
		if (row.functionType === "crew") {
		  typeIcon = `<i class="fa fa-user"></i>`;
		}
		if (row.functionType === "transport") {
		  typeIcon = `<i class="fa fa-truck"></i>`;
		}

		this.treeGrid.setCellValue(row.uid, "functionType", typeIcon);
	  }

	private getAvailabilityColor(type: string): string {
		let color: string;
		switch (type) {
			case ShedulerEventAction[ShedulerEventAction.available]:
				color = "#B7E3F0";
				break;
			case ShedulerEventAction[ShedulerEventAction.notAvailable]:
				color = "#D66B5D";
				break;
			case ShedulerEventAction[ShedulerEventAction.unknown]:
				color = "#192D3E";
				break;
			case ShedulerEventAction[ShedulerEventAction.reserved]:
				color = "#74C1D9";
				break;
			case ShedulerEventAction[ShedulerEventAction.invite]:
				color = "#0038FF";
				break;
			case ShedulerEventAction[ShedulerEventAction.appointment]:
				color = "#E7A953";
				break;
		}

		return color;
	}

  // date range
  _startProjValueChange = (from: string, to: string) => {
    if (this.entity[from] > this.entity[to]) {
      this.entity[to] = null;
    }
  };

  _endProjValueChange = (from: string, to: string) => {
    if (this.entity[from] > this.entity[to]) {
      this.entity[from] = null;
    }
  };

	_disabledStartDate: any = (startValue: Date) => {
		if (!startValue || !this.entity.planUntil) {
			return false;
		}
		return (
			new Date(startValue).getTime() >=
			new Date(this.entity.planUntil).getTime()
		);
	};

	_disabledEndDate: any = (endValue: Date) => {
		if (!endValue || !this.entity.planFrom) {
			return false;
		}
		return (
			new Date(endValue).getTime() <= new Date(this.entity.planFrom).getTime()
		);
	};
}
