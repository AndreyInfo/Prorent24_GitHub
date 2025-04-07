import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

enum ProjectTabsEnum {
	data,
	equipments,
	staff_and_vehicles,
	additional_cost,
	finance,
	sublease,
	shortage,
	planning
}

@Component({
	selector: "app-project-tabs",
	templateUrl: "./project-tabs.component.html"
})
export class ProjectTabsComponent implements OnInit {
	id: any;
	tabs: Array<string>;
	label: string;

	constructor(private route: ActivatedRoute) {
		this.id = this.route.snapshot.params.id;
		this.tabs = [ProjectTabsEnum[ProjectTabsEnum.data]];
	}

	ngOnInit(): void {
		if (this.id) {
			let additional_tabs: Array<string> = [
				ProjectTabsEnum[ProjectTabsEnum.equipments],
				ProjectTabsEnum[ProjectTabsEnum.shortage],
				ProjectTabsEnum[ProjectTabsEnum.staff_and_vehicles],
				ProjectTabsEnum[ProjectTabsEnum.additional_cost],
				ProjectTabsEnum[ProjectTabsEnum.finance],
				//ProjectTabsEnum[ProjectTabsEnum.sublease],
				ProjectTabsEnum[ProjectTabsEnum.planning]
			];

			this.tabs = [...this.tabs, ...additional_tabs];
		}
	}
}
