import { Injectable, Inject } from "@angular/core";
import { HttpService } from "@core/http.service";

import { DynamicTable } from "@shared/models/dynamic-table.model";
import { JqxGridModel, Column } from "@shared/models/jqx-grid.model";

import {
	IBaseEndPoints,
	GeneralGridColumnsEndpoints,
	GridConfigEndpoints
} from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { TranslateService } from "@ngx-translate/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { HttpParams } from "@angular/common/http";
import { Entity } from "@shared/enums/entity.enum";
import { GridPermission } from "@shared/models/grid-permission.model";
import { saveAs } from 'file-saver';

import * as moment from "moment";
import { TableEntity } from "@shared/enums/table-entity.enum";

@Injectable()
export class GridService {
	endpoint: IBaseEndPoints;

	gridPermission: GridPermission = new GridPermission();

	dataFilter: string;

	gridDisplayed: boolean = false;

	constructor(
		private http: HttpService,
		@Inject(TranslateService) private translate: TranslateService,
		@Inject(Router) private router: Router
	) { }

	async getDataGrid(filter?: any, parentId?: any): Promise<JqxGridModel> {
		this.dataFilter = filter;
		if (filter) {
			return this.getFilterDataGrid(filter, parentId);
		} else {
			return this.http
				.get(
					parentId
						? StringExt.Format(this.endpoint.root, parentId)
						: this.endpoint.root
				)
				.toPromise()
				.then((data: DynamicTable) => {
					if (data.grid != null) {
						this.gridDisplayed = data.grid.columns.findIndex(x => x.isDisplayName) > -1;
						this.setGridPermission(data.grid);
					} else {
						console.log("error permission check");
					}
					return this.buildDataGrid(data);
				});
		}
	}

	async getFilterDataGrid(filter: any, parentId?: any): Promise<JqxGridModel> {
		let params: any = new HttpParams().set("filters", JSON.stringify(filter));

		return this.http
			.get(
				parentId
					? StringExt.Format(this.endpoint.root, parentId)
					: this.endpoint.root,
				params
			)
			.toPromise()
			.then((data: DynamicTable) => {
				if (data.grid != null) {
					this.setGridPermission(data.grid);
				} else {
					console.log("error permission check");
				}
				return this.buildDataGrid(data);
			});
	}

	async addGridColumn(columns: any): Promise<any> {
		return this.http
			.post(GeneralGridColumnsEndpoints.add_columns, columns)
			.toPromise();
	}

	async setGridColumn(columns: any): Promise<any> {
		return this.http
			.post(GeneralGridColumnsEndpoints.set_columns, columns)
			.toPromise();
	}

	async initCurrentGridColumn(entity: Entity, columns: any[]): Promise<any> {
		let t_entity = TableEntity[entity.toString()];
		return this.http
			.post(
				StringExt.Format(
					GeneralGridColumnsEndpoints.init_current_columns,
					t_entity
				),
				columns
			)
			.toPromise();
	}

	getItemInfo(id: string): Observable<any> {
		return this.http.get(StringExt.Format(this.endpoint.details, id));
	}

	getSingleItem(id: string, parentId?: any): Observable<any> {
		if (parentId) {
			return this.http.get(
				StringExt.Format(this.endpoint.single, parentId, id)
			);
		} else {
			return this.http.get(StringExt.Format(this.endpoint.single, id));
		}
	}

	async upsertRow(model: any, parentId?: any): Promise<any> {
		let url: string;
		if (!parentId) {
			url = model.id
				? StringExt.Format(this.endpoint.single, model.id)
				: this.endpoint.root;
		} else {
			url = model.id
				? StringExt.Format(this.endpoint.single, parentId, model.id)
				: StringExt.Format(this.endpoint.root, parentId);
		}

		return this.http
			.post(url, model)
			.toPromise()
			.then(res => {
				return res;
			});
	}

	async upsertRowByUrl(model: any, url: string): Promise<any> {
		return this.http
			.post(url, model)
			.toPromise()
			.then(res => {
				return res;
			});
	}

	async deleteRow(id: any, parentId?: any): Promise<any> {
		if (parentId) {
			return this.http
				.post(StringExt.Format(this.endpoint.delete, parentId, id))
				.toPromise()
				.then(res => {
					return res;
				});
		} else {
			return this.http
				.post(StringExt.Format(this.endpoint.delete, id))
				.toPromise()
				.then(res => {
					return res;
				});
		}
	}

	redirectToEditForm(id: any): void {
		this.router.navigate([`${this.router.url}/edit/${id}`]);
	}

	async getDataGridByUrl(url: string): Promise<JqxGridModel> {
		return this.http
			.get(url)
			.toPromise()
			.then((data: DynamicTable) => {
				return this.buildDataGrid(data);
			});
	}

	resizeColumn(event: any, entity: Entity): void {
		let dataField: any = event.args.datafield
			? event.args.datafield
			: event.args.dataField;
		let width: any = event.args.newwidth
			? event.args.newwidth
			: event.args.newWidth;

		this.http
			.post(
				StringExt.Format(
					GridConfigEndpoints.resize_column,
					entity,
					dataField,
					width
				)
			)
			.subscribe();
	}

	reorderColumn(
		event: any,
		entity: Entity,
		columns?: any,
		treeGrid?: boolean
	): void {
		let dataField: string = event.args.datafield
			? event.args.datafield
			: event.args.dataField;

		let index: number =
			event.args.newindex || event.args.newindex == 0
				? event.args.newindex
				: event.args.newIndex;

		let column_indexes: Array<any> = new Array<any>();

		if (treeGrid) {
			columns.forEach((f: any, index: number) => {
				column_indexes.push({
					column: f.datafield,
					index: index,
					width: f.width,
					hidden: f.hidden
				});
			});
		} else {
			Object.keys(columns).forEach((f: any) => {
				column_indexes.push({
					column: f,
					index: columns[f].index,
					width: columns[f].width,
					hidden: columns[f].hidden
				});
			});
		}

		this.http
			.post(
				StringExt.Format(
					GridConfigEndpoints.reorder_column,
					entity,
					dataField,
					index
				),
				column_indexes
			)
			.subscribe();
	}

	exportData(entity: Entity, columns?: any, treeGrid?: boolean): any {
		let column_indexes: Array<any> = new Array<any>();

		if (treeGrid) {
			columns.forEach((f: any, index: number) => {
				column_indexes.push({
					column: f.datafield,
					hidden: f.hidden
				});
			});
		} else {
			Object.keys(columns).forEach((f: any) => {
				column_indexes.push({
					column: f,
					hidden: columns[f].hidden
				});
			});
		}

		let columns_forExport = column_indexes
			.filter(x => x.hidden == false)
			.map(x => x.column);

		let params: any = new HttpParams().set(
			"columns",
			JSON.stringify(columns_forExport)
		);

		if (this.dataFilter) {
			params = params.set("filters", JSON.stringify(this.dataFilter));
		}

		this.http.postArrayBuffer(this.endpoint.export, params)
			.subscribe((data: Blob) => {
				var blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;' })
				saveAs(blob, "equipment.xlsx")
			});

		// call method - could check in db
	}


	onSaveFile(url: string, name: string): any {
		this.http.uploadFileByUrl(url)
			.subscribe((data: Blob) => {
				saveAs(data, name);
				return true;
			});
	}

	getCellValueTranslate(value: any): any {
		return this.translate.instant(`entity.${value.toLowerCase()}`);
	}

	private setGridPermission(permission: any): void {
		this.gridPermission.edit = permission.edit;
		this.gridPermission.delete = permission.delete;
	}

	private buildDataGrid(data: DynamicTable): JqxGridModel {
		let gridModel: JqxGridModel = new JqxGridModel();

		data.grid.columns.forEach(element => {
			let column_type: string;

			switch (element.type) {
				case "boolean":
					column_type = "checkbox";
					break;
			}

			let _newColumn: Column = {
				text: this.translate.instant(`entity.${element.key.toLowerCase()}`),
				datafield: element.key,
				hidden: !element.isDisplayName,
				columntype: column_type,
				cellsrenderer: this.cellsRenderer(element.key, element.type),
				editable: false
			};

			if (element.width) {
				_newColumn.width = element.width;
			}

			gridModel.columns.push(_newColumn);

			gridModel.source.datafields.push({
				name: element.key,
				type: element.type
			});
		});

		if (data.grid.hierarchy) {
			gridModel.source.hierarchy = data.grid.hierarchy;
		}

		gridModel.source.datatype = "json";
		gridModel.source.localdata = data.grid.data;
		gridModel.source.dataGroups = data.grid.dataGroups;

		return gridModel;
	}

	private cellsRenderer(key: any, type: string): any {
		switch (key) {
			case "color":
				return this.colorCellsRenderer();
		}

		switch (type) {
			case "date":
				return this.dateCellRenderer();
			case "enum":
				return this.enumCellsRenderer();
			case "boolean":
				return this.booleanCellRenderer();
		}

		return null;
	}

	private colorCellsRenderer(): any {
		let cellsrenderer: any = function (
			row,
			columnfield,
			value,
			defaulthtml,
			columnproperties
		) {
			return (
				'<div style="margin: 8px 4px; float: ' +
				columnproperties.cellsalign +
				";background-color: " +
				value +
				';height: 16px;width: 16px;"></div>'
			);
		};
		return cellsrenderer;
	}

	private enumCellsRenderer(): any {
		let cellsrenderer: any = function (
			context,
			row,
			columnfield,
			value,
			defaulthtml,
			columnproperties
		) {
			if (value) {
				//TODO:entity. .toLowerCase() SERGEY
				return (
					'<div style="margin: 8px 4px; float: ' +
					columnproperties.cellsalign +
					';height: 16px;width: 16px;">' +
					context.translate.instant(`${value}`) +
					"</div>"
				);
			}
		};
		return cellsrenderer.bind("context", this);
	}

	private dateCellRenderer(): any {
		let cellsDateRenderer: any = function (
			row,
			columnfield,
			value,
			defaulthtml,
			columnproperties
		) {
			if (value) {
				if (moment(value).isValid()) {
					var stillUtc = moment.utc(value).toDate();
					value = moment(stillUtc)
						.local()
						.format("DD.MM.YYYY HH:mm");
				}
				else {
					let date = new Date(value);
					value = moment(date)
						.format("DD.MM.YYYY HH:mm");
				}

				return `<div class="ml-1" style="margin-top: 8px;">${value}</div>`;
			} else {
				return;
			}
		};

		return cellsDateRenderer;
	}

	private booleanCellRenderer(): any {
		let cellsBooleanRenderer: any = function (
			row,
			columnfield,
			value,
			defaulthtml,
			columnproperties
		) {
			if (value) {
				return `<div class="jqx-grid-cell-middle-align mt-2"><i class="fa fa-check"></i></div>`;
			} else {
				return `<div class="jqx-grid-cell-middle-align mt-2"><i class="fa fa-times"></i></div>`;
			}
		};

		return cellsBooleanRenderer;
	}
}
