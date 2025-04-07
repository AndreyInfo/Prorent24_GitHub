import { ViewChild, Output, EventEmitter, Injector } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ModalDirective } from "ngx-bootstrap";
import { jqxTreeGridComponent } from "jqwidgets-ng/jqxtreegrid";
import { jqxSplitterComponent } from "jqwidgets-ng/jqxsplitter";

import { GridService } from "@services/grid.service";
import { JqxGridModel } from "@shared/models/jqx-grid.model";
import { IBaseEndPoints } from "@endpoints";
import { BaseModel } from "@models/base.model";
import { GridFilter } from "@shared/models/grid-filter.model";
import { getLocalization } from "@shared/utils/grid-localization";
import { Subject } from "rxjs";
import { TableEntity } from "@shared/enums/table-entity.enum";
import { Entity } from "@shared/enums/entity.enum";
import { AdditionalGridColumnComponent } from "@components/additional-grid-column/additional-grid-column.component";
import { GridPermission } from "@shared/models/grid-permission.model";
import { FilterType } from "@shared/enums/filter-type.enum";
import { FilterModel } from "@models/common/filter.model";
import { FilterPeriod } from "@models/common/filter-period.model";
import { PeriodType } from "@shared/enums/period-type.enum";
import { DurationType } from "@shared/enums/duration-type.enum";
import { TimeUnitExtend } from "@shared/enums/time-unit.enum";

export abstract class TreeGridAbstract<T extends BaseModel> {
	protected grid: GridService;

	entitySubmitComplete: Subject<any> = new Subject<any>();

	@ViewChild("treeGrid", { static: true }) treeGrid: jqxTreeGridComponent;
	@ViewChild("rowModal", { static: true }) rowModal: ModalDirective;
	@ViewChild("mainSplitter", { static: true })
	mainSplitter: jqxSplitterComponent;
	@ViewChild("nestedSplitter", { static: true })
	nestedSplitter: jqxSplitterComponent;
	@ViewChild(AdditionalGridColumnComponent, { static: true })
	additioanlFieldsModal: AdditionalGridColumnComponent;

	@Output() onChanged = new EventEmitter<Array<any>>();
	change(data: Array<any>): void {
		this.onChanged.emit(data);
	}

	@Output() onDataLoadComplete = new EventEmitter<any>();
	dataLoadComplete(data: any): void {
		this.onDataLoadComplete.emit(data);
	}

	localization: any = getLocalization("ru");

	tableSource: JqxGridModel = new JqxGridModel();
	source: any = {};

	entity: T;
	entityType: Entity;
	selected_entity: T;
	selected_entity_full: any;
	canAction: boolean;
	selectMode: string = "singleRow";

	filter: Array<GridFilter> = new Array<GridFilter>();
	parentId: any;
	removeArrayMode: boolean = false;

	// splitter
	panelOptions: any = {};

	gridPermission: GridPermission = new GridPermission();

	// controls
	showInfoWidget: boolean = true;
	showTimeline: boolean = true;

	widjetSwitchState: boolean = true;
	widjetChronologiSwitchState: boolean = true;

	widjetSwitchPeriod: boolean = false;
	filterPeriod: FilterPeriod = new FilterPeriod();
	periodType: any = PeriodType;
	durationType: any = DurationType;
	timeUnitExtend: any = TimeUnitExtend;
	fromPeriod: Date = new Date();
	toPeriod: Date = new Date();

	searchText: string;

	constructor(
		private injectorObj: Injector,
		private type: new () => T,
		private endPoint: IBaseEndPoints
	) {
		this.grid = this.injectorObj.get(GridService);
		this.grid.endpoint = this.endPoint;
		this.entity = new this.type();
	}

	ngOnInit(): void {
		this.loadData();
	}

	onChangedFilter(filters: Array<any>): void {
		this.loadData(filters);
	}

	//#region load data
	loadData(filter?: Array<GridFilter>): void {
		this.grid.getDataGrid(filter).then((data: JqxGridModel) => {
			this.tableSource = data;
			this.source = new jqx.dataAdapter(this.tableSource.source);

			this.gridPermission = this.grid.gridPermission;

			this.dataLoadComplete(data);
		});

		this.treeGrid.onColumnResized.subscribe((e: any) => {
			this.grid.resizeColumn(e, TableEntity[this.entityType]);
		});

		this.treeGrid.onColumnReordered.subscribe((e: any) => {
			let columns: any = this.treeGrid.columns();
			this.grid.reorderColumn(
				e,
				TableEntity[this.entityType],
				columns.records,
				true
			);
		});
	}

	loadSubData(parentId: any, filter?: any): void {
		this.parentId = parentId;
		this.grid.getDataGrid(filter, parentId).then((data: JqxGridModel) => {
			this.tableSource = data;
			this.source = new jqx.dataAdapter(this.tableSource.source);
			this.gridPermission = this.grid.gridPermission;

			this.treeGrid.onBindingComplete.subscribe(() => {
				if (this.tableSource.source.localdata.length > 0) {
					this.treeGrid.selectRow(0);
				}

				this.dataLoadComplete(data);
			});
		});

		this.treeGrid.onColumnResized.subscribe((e: any) => {
			this.grid.resizeColumn(e, TableEntity[this.entityType]);
		});

		this.treeGrid.onColumnReordered.subscribe((e: any) => {
			let columns: any = this.treeGrid.columns();
			this.grid.reorderColumn(
				e,
				TableEntity[this.entityType],
				columns.records,
				true
			);
		});
	}
	//#endregion

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

	//#region submit
	onSubmit(form: NgForm, ignoreParentId?: boolean, isLoadData?: boolean,url?: string): void {
		if (form.valid) {
			this.grid
				.upsertRow(form.value, ignoreParentId ? null : this.parentId)
				.then(data => {

					if(isLoadData){
						this.loadDataByUrl(url);
					}
					else{
						if (form.value.id) {
							this.treeGrid.updateRow(this.selected_entity["uid"], data);
						} else {
							this.treeGrid.addRow(null, data, "last");
						}
						
						this.entity = new this.type();
						this.rowModal.hide();
	
						this.change(this.treeGrid.getRows());
						this.entitySubmitComplete.next(data);
					}
				});
		}
	}

	onSubmitCustomData(data: any, ignoreParentId?: boolean): Promise<any> {
		return this.grid
			.upsertRow(data, ignoreParentId ? null : this.parentId)
			.then();
	}
	//#endregion

	//#region events
	onRowSelect(event: any, info?: boolean, onlyChildLevel?: boolean): void {
		if (event.args.row) {
			this.selected_entity = event.args.row;
			this.selected_entity.rowIndex = event.args.rowindex;

			if (this.selected_entity.id) {
				if (!onlyChildLevel) {
					this.canAction = true;
				}
				if (onlyChildLevel && this.selected_entity["level"] > 0) {
					this.canAction = true;
				} else {
					// this.canAction = false;
				}
			}

			if (info) {
				this.grid
					.getItemInfo(this.selected_entity.id)
					.subscribe(data => (this.selected_entity_full = data));
			}
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
	//#endregion

	//#region action panel
	onEditRowModal(
		id?: any,
		selectfull?: boolean,
		ignoreParentId?: boolean,
		onlyChildLevel?: boolean
	): void {
		if (
			!onlyChildLevel ||
			(onlyChildLevel && this.selected_entity["level"] > 0)
		) {
			if (id) {
				if (selectfull) {
					this.grid
						.getSingleItem(
							this.selected_entity.id,
							ignoreParentId ? null : this.parentId
						)
						.subscribe(data => (this.entity = data));
				} else {
					this.entity = this.selected_entity;
				}
			} else {
				this.entity = new this.type();
			}

			this.rowModal.show();
		}
	}

	onDeleteRows(ignoreParentId?: boolean, removeParents?: boolean): void {
		let rows: any[] = this.treeGrid.getCheckedRows();

		rows.forEach((item: any) => {
			if (removeParents) {
				this.grid.deleteRow(item.id).then(_ => {
					this.treeGrid.deleteRow(item["uid"]);
				});
			}
			if (item.level > 0) {
				this.grid
					.deleteRow(item.id, ignoreParentId ? null : this.parentId)
					.then(_ => {
						this.treeGrid.deleteRow(item["uid"]);
					});
			}
		});

		this.changeRemoveArrayMode();
	}

	changeRemoveArrayMode(): void {
		this.removeArrayMode = !this.removeArrayMode;
		this.selectMode = this.removeArrayMode ? "multipleRows" : "singleRow";
	}
	//#endregion

	getRowsByCellValue(cell: string, value: any): any[] {
		let res_rows: Array<any> = new Array<any>();

		let rows: any = this.treeGrid.getRows();
		rows.forEach((row: any) => {
			if (row[cell] === value) {
				res_rows.push(row);
			}
		});

		return res_rows;
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

	onAddColumns(columns: any): void {
		this.grid.addGridColumn(columns).then(() => {
			this.loadData();
		});
	}

	onAddColumnsSubData(patentId,columns: any): void {
		debugger
		this.grid.addGridColumn(columns).then(() => {
			this.loadSubData(patentId);
		});
	}

	//onExportData(format: string = "xls"): void {
	//this.treeGrid.exportSettings({ fileName: this.entityType });
	//this.treeGrid.exportData(format);
	//console.log(this.treeGrid.getView());
	//}

	onExportData(format: string = "xls"): void {
		// this.dataGrid.exportdata(format, name);
		debugger;
		//console.log(this.dataGrid.getstate().columns);
		let columns: any = this.treeGrid.columns();
		this.grid.exportData(TableEntity[this.entityType], columns.records, true);
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

	onChangeWidjetSwitch(event){
		this.widjetSwitchState = !event;
		this.toggleWidget();
	  }

	  onChangeWidjetChronologiSwitch(event){
		this.widjetChronologiSwitchState = !event;
	  }

	  onChangeWidjetPeriod(event) {
		this.widjetSwitchPeriod = event;
		if (event) {
			this.onChangedFilterByPeriod();
		}
	}

	onSaveFile(url: string, name: string): void {
		this.grid.onSaveFile(
			url,
			name
		);
	}
}
