import { ViewChild, Output, EventEmitter } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ModalDirective } from "ngx-bootstrap";
import { jqxGridComponent } from "jqwidgets-ng/jqxgrid";

import { GridService } from "@services/grid.service";
import { JqxGridModel } from "@shared/models/jqx-grid.model";
import { IBaseEndPoints } from "@endpoints";
import { BaseModel } from "@models/base.model";
import { GridFilter } from "@shared/models/grid-filter.model";
import { getLocalization } from "@shared/utils/grid-localization";
import { AdditionalGridColumnComponent } from "@components/additional-grid-column/additional-grid-column.component";
import { jqxSplitterComponent } from "jqwidgets-ng/jqxsplitter";
import { Entity } from "@shared/enums/entity.enum";
import { TableEntity } from "@shared/enums/table-entity.enum";
import { GridPermission } from "@shared/models/grid-permission.model";
import { take } from "rxjs/operators";
import { FilterType } from "@shared/enums/filter-type.enum";
import { FilterModel } from "@models/common/filter.model";
import { FilterPeriod } from "@models/common/filter-period.model";
import { PeriodType } from "@shared/enums/period-type.enum";
import { DurationType } from "@shared/enums/duration-type.enum";
import { TimeUnitExtend } from "@shared/enums/time-unit.enum";

export abstract class GridAbstract<T extends BaseModel> {
	@ViewChild("dataGrid", { static: true }) dataGrid: jqxGridComponent;
	@ViewChild("rowModal", { static: true }) rowModal: ModalDirective;
	@ViewChild("mainSplitter", { static: true })
	mainSplitter: jqxSplitterComponent;
	@ViewChild("nestedSplitter", { static: true })
	nestedSplitter: jqxSplitterComponent;
	@ViewChild(AdditionalGridColumnComponent, { static: true })
	additioanlFieldsModal: AdditionalGridColumnComponent;

	@Output() onChanged = new EventEmitter<Array<any>>();
	change(data: any): void {
		this.onChanged.emit(data);
	}

	onSelected = new EventEmitter<any>();

	onStartEditFull = new EventEmitter<any>();
	startEdit(data: any): void {
		this.onStartEditFull.emit(data);
	}

	localization: any = getLocalization("ru");

	tableSource: JqxGridModel = new JqxGridModel();
	source: any = {};

	entity: T;
	entityType: Entity;
	selected_entity: T;
	selected_entity_full: any;
	canAction: boolean;
	selectMode: string = "singlerow";

	widjetSwitchState: boolean = true;

	widjetSwitchPeriod: boolean = false;
	filterPeriod: FilterPeriod = new FilterPeriod();
	periodType: any = PeriodType;
	durationType: any = DurationType;
	timeUnitExtend: any = TimeUnitExtend;
	fromPeriod: Date = new Date();
	toPeriod: Date = new Date();

	filter: Array<GridFilter> = new Array<GridFilter>();

	parentId: any;

	removeArrayMode: boolean = false;

	// splitter
	panelOptions: any = {
		headerSplitter: [] = [{ size: "100%", collapsible: false }],
		mainSplitter: [] = [{ size: "70%", collapsible: false }, { size: "30%" }],
		nestedSplitter: [] = [
			{ size: "100%", collapsible: false },
			{ size: 260, min: 260, max: 350 }
		]
	};

	gridPermission: GridPermission = this.grid.gridPermission;

	// controls
	showInfoWidget: boolean = true;
	showTimeline: boolean = true;

	gridDisplayed: boolean = this.grid.gridDisplayed;

	constructor(
		public grid: GridService,
		private type: new () => T,
		private endPoint: IBaseEndPoints
	) {
		this.grid.endpoint = this.endPoint;
		this.entity = new this.type();
	}


	ngOnInit(): void {
		this.loadData();
	}

	loadData(filter?: Array<GridFilter>): void {
		this.grid.getDataGrid(filter).then((data: JqxGridModel) => {
			this.tableSource = data;
			
			//console.log("p1-loadData - 111-44 " + data.columns[0].text.toString());
			
			this.source = new jqx.dataAdapter(this.tableSource.source);

			this.dataGrid.onBindingcomplete.pipe(take(1)).subscribe(() => {
				if (this.tableSource.source.localdata.length > 0) {
					this.dataGrid.selectrow(0);
				}
			});
		});

		this.dataGrid.onColumnresized.subscribe((e: any) => {
			this.grid.resizeColumn(e, TableEntity[this.entityType]);
		});

		this.dataGrid.onColumnreordered.subscribe((e: any) => {
			this.grid.reorderColumn(
				e,
				TableEntity[this.entityType],
				this.dataGrid.getstate().columns
			);
		});
	}

	loadSubData(parentId: any, filter?: any): void {
		this.parentId = parentId;
		this.grid.getDataGrid(filter, parentId).then((data: JqxGridModel) => {
			this.tableSource = data;
			this.source = new jqx.dataAdapter(this.tableSource.source);
		});
	}

	loadDataByUrl(url: string): void {
		this.grid.getDataGridByUrl(url).then((data: JqxGridModel) => {
			this.tableSource = data;
			this.source = new jqx.dataAdapter(this.tableSource.source);
		});
	}

	onChangedFilterByText(event): void {
		let selectedFilter: Array<any> = new Array<any>();
		if (event.target.value && event.target.value.length > 0) {
			let searchModel = new FilterModel();
			searchModel.filterType = FilterType.searchText;
			searchModel.values = [];
			searchModel.values.push(event.target.value);
			selectedFilter.push(searchModel);
		}

		this.loadData(selectedFilter);
	}

	onChangedFilterByTextVal(val: string): void {
		let selectedFilter: Array<any> = new Array<any>();
		if (val && val.length > 0) {
			let searchModel = new FilterModel();
			searchModel.filterType = FilterType.searchText;
			searchModel.values = [];
			searchModel.values.push(val);
			selectedFilter.push(searchModel);
		}

		this.loadData(selectedFilter);
	}

	onChangedFilterByPeriod(): void {
		let selectedFilter: Array<any> = new Array<any>();
		let periodModel = new FilterModel();
		this.filterPeriod.periodType = !this.widjetSwitchPeriod ? PeriodType.fromToUntil : PeriodType.period;
		periodModel.filterType = FilterType.period;
		periodModel.values = [];
		periodModel.values.push(JSON.stringify(this.filterPeriod));
		selectedFilter.push(periodModel);
		this.loadData(selectedFilter);
	}

	// date range
	_startValueChange = () => {
		if (this.filterPeriod.fromDate > this.filterPeriod.toDate) {
			this.filterPeriod.toDate = null;
		}
	};

	_endValueChange = () => {
		if (this.filterPeriod.fromDate > this.filterPeriod.toDate) {
			this.filterPeriod.fromDate = null;
		}
	};

	_disabledStartDate: any = (startValue: Date) => {
		if (!startValue || !this.filterPeriod.toDate) {
			return false;
		}
		return (
			new Date(startValue).getTime() >=
			new Date(this.filterPeriod.toDate).getTime()
		);
	};

	_disabledEndDate: any = (endValue: Date) => {
		if (!endValue || !this.filterPeriod.fromDate) {
			return false;
		}
		return (
			new Date(endValue).getTime() <=
			new Date(this.filterPeriod.fromDate).getTime()
		);
	};

	onChangedFilter(filters: Array<any>): void {
		this.loadData(filters);
	}

	onAddColumns(columns: any): void {
		this.grid.addGridColumn(columns).then(() => {
			this.loadData();
		});
	}

	onSubmit(form: NgForm, ignoreParentId?: boolean): void {

		if (form.value.deliveryDate != null) {
			form.value.deliveryDate = new Date(Date.parse(form.value.deliveryDate)).toDateString();
			//console.log("p1 - " + form.value.deliveryDate);
		}

		if (form.valid) {
			this.grid
				.upsertRow(form.value, ignoreParentId ? null : this.parentId)
				.then(data => {
					if (form.value.id) {
						let itemIndex: number = this.tableSource.source.localdata.findIndex(
							i => i.id == form.value.id
						);
						//console.log("p1-onSubmit " + JSON.stringify(data));
						this.tableSource.source.localdata[itemIndex] = data;
						if (this.dataGrid) {
							this.dataGrid.updaterow(this.selected_entity.rowIndex, data);
						}
					} else {
						this.tableSource.source.localdata.push(data);
						if (this.dataGrid) {
							this.dataGrid.addrow(null, data);
						}
					}
					this.entity = new this.type();

					this.rowModal.hide();

					if (this.dataGrid) {
						this.change(this.dataGrid.getrows());
					}
					else {
						this.change(data);
					}
				});
		}
	}

	onSubmitCustomData(data: any, ignoreParentId?: boolean): Promise<any> {
		return this.grid
			.upsertRow(data, ignoreParentId ? null : this.parentId)
			.then();
	}

	onRowSelect(event: any, info?: boolean): void {
		if (event.args.row) {
			this.selected_entity = event.args.row;
			this.selected_entity.rowIndex = event.args.rowindex;

			if (this.selected_entity.id) {
				this.canAction = true;
			}

			if (info && !this.removeArrayMode) {
				this.grid
					.getItemInfo(this.selected_entity.id)
					.subscribe(data => (this.selected_entity_full = data));
			}

			this.onSelected.emit(this.selected_entity);
		} else if (Array.isArray(event.args.rowindex)) {
			this.canAction = true;
		}
	}

	onRowUnselect(): void {
		this.selected_entity = null;
		this.canAction = false;
	}

	openEditForm(): void {
		if (this.selected_entity && this.selected_entity.id) {
			this.grid.redirectToEditForm(this.selected_entity.id);
		}
	}

	// actions panel
	onEditRowModal(
		id?: any,
		selectfull?: boolean,
		ignoreParentId?: boolean
	): void {
		if (id) {
			if (selectfull) {
				this.grid
					.getSingleItem(
						this.selected_entity.id,
						ignoreParentId ? null : this.parentId
					)
					.subscribe(
						data => {
							Object.assign(this.entity, data);
						},
						null,
						() => {
							this.startEdit(this.entity);
						}
					);
			} else {
				this.entity = this.selected_entity;
			}
		} else {
			this.entity = new this.type();
		}

		this.rowModal.show();
	}

	onCopyRow(): void {
		this.entity = this.selected_entity;
		this.entity.id = 0;
		this.rowModal.show();
	}

	onDeleteRow(parentId?: any): void {
		let id: number = this.selected_entity.id;

		this.grid.deleteRow(id, parentId).then(_ => {
			if (this.dataGrid) {
				let selectedrowindex: number = this.dataGrid.getselectedrowindex();

				let itemIndex: number = this.tableSource.source.localdata.findIndex(
					i => i.id == id
				);

				this.tableSource.source.localdata.splice(itemIndex, 1);
				this.dataGrid.deleterow(selectedrowindex);
			}
			else {
				this.change({ removed: true, entity: this.selected_entity });
			}
		});
	}

	onDeleteRows(ignoreParentId?: boolean): void {
		let rowIndexes: number[] = this.dataGrid.getselectedrowindexes();

		rowIndexes.forEach((index: number) => {
			let row: any = this.dataGrid.getrowdatabyid(index.toString());

			this.grid
				.deleteRow(row.id, ignoreParentId ? null : this.parentId)
				.then(_ => {
					let itemIndex: number = this.tableSource.source.localdata.findIndex(
						i => i.id == row.id
					);

					this.tableSource.source.localdata.splice(itemIndex, 1);
					this.dataGrid.deleterow(index);

					if (this.selected_entity.id == row.id) {
						this.selected_entity_full = null;
						this.canAction = false;
					}
				});
		});

		this.changeRemoveArrayMode();
	}

	onExportData(name: string, format: string = "xls"): void {
		this.grid.exportData(
			TableEntity[this.entityType],
			this.dataGrid.getstate().columns,
			false
		);
	}

	changeRemoveArrayMode(): void {
		this.removeArrayMode = !this.removeArrayMode;
		this.selectMode = this.removeArrayMode ? "checkbox" : "singlerow";
	}

	showAdditionalFieldsModal(): void {
		let currentColumns: any[] = this.tableSource.columns
			.filter(f => !f.hidden)
			.map(c => {
				return c.datafield;
			});

		this.grid
			.initCurrentGridColumn(this.entityType, currentColumns)
			.then(() => {
				this.additioanlFieldsModal.showModal();
			});
	}

	getRowsByCellValue(cell: string, value: any): any[] {
		let rows: Array<any> = this.dataGrid
			.getrows()
			.filter(f => f[cell] === value);
		return rows;
	}

	toggleWidget(): void {
		this.showInfoWidget = !this.showInfoWidget;
		if (this.showInfoWidget) {
			this.nestedSplitter.expand();
		} else {
			this.nestedSplitter.collapse();
		}
	}

	toggleTimeline(): void {
		this.showTimeline = !this.showTimeline;
		if (this.showTimeline) {
			this.mainSplitter.expand();
		} else {
			this.mainSplitter.collapse();
		}
	}

	cellhovertooltiprenderer = (
		element: any,
		pageX: number,
		pageY: number
	): void => { };

	onChangeWidjetSwitch(event) {
		this.widjetSwitchState = !event;
		this.toggleWidget();
	}

	onChangeWidjetPeriod(event) {
		this.widjetSwitchPeriod = event;
//console.log("p2-onChangeWidjetPeriod " + event);
		this.onChangedFilterByPeriod();
	}
}
