import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";

import { HttpService } from "@core/http.service";
import { TimeRegistrationEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { TimeRegistrationModel } from "@models/time-registration/time-registration.model";
import { SearchService } from "@services/search.service";
import { TimeUnit } from "@shared/enums/time-unit.enum";
import { HourRegistrationType } from "@shared/enums/hour-registration.enum";

@Component({
	selector: "app-time-registration-form",
	templateUrl: "./time-registration-form.component.html"
})
export class TimeRegistrationFormComponent implements OnInit {
	private id: string;
	entity: TimeRegistrationModel = new TimeRegistrationModel();
	countries: Array<any>;
	users: Array<any> = new Array<any>();

	timeUnit = TimeUnit;
	hourRegistrations = HourRegistrationType;

	constructor(
		private http: HttpService,
		private activateRoute: ActivatedRoute,
		private search: SearchService,
		private router: Router
	) {
		this.id = this.activateRoute.snapshot.params.id;
	}

	ngOnInit(): void {
		if (this.id) {
			this.http
				.getT<TimeRegistrationModel>(
					StringExt.Format(TimeRegistrationEndpoints.single, this.id)
				)
				.subscribe(data => (this.entity = data));
		}
	}

	onSubmit(form: NgForm): void {

		if (form.value.until != null) {
			form.value.until = (new Date(Date.parse(form.value.until)).toDateString()) + " " + (new Date(Date.parse(form.value.until)).getHours().toString()) + ":" + (new Date(Date.parse(form.value.until)).getMinutes().toString());
		}
		if (form.value.from != null) {
			form.value.from = (new Date(Date.parse(form.value.from)).toDateString()) + " " + (new Date(Date.parse(form.value.from)).getHours().toString()) + ":" + (new Date(Date.parse(form.value.from)).getMinutes().toString());
		}

		if (form.valid) {
			if (this.entity.id) {
				this.http
					.post(
						StringExt.Format(TimeRegistrationEndpoints.single, this.entity.id),
						form.value
					)
					.subscribe(null, null, () => {
						this.router.navigate(["/time-registration"]);
					});
			} else {
				this.http
					.post(TimeRegistrationEndpoints.root, form.value)
					.subscribe(null, null, () => {
						this.router.navigate(["/time-registration"]);
					});
			}
		}
	}

	onChangeUserSearch(event: any): void {
		let search_val: string = event.target.value;
		if (search_val.length > 2 && search_val.length < 6) {
			this.search.users(search_val).subscribe((data: Array<any>) => {
				if (data.length > 0) {
					//let _data = [];
					data.forEach((e: any, index: number) => {
						let u: any = this.users.find(f => f.id === e.id);
						if (u) {
							data.splice(index, 1);
							//_data.push(u);
						}
					});
					this.users = [...this.users, ...data];
					console.log("p2 - onChangeUserSearch - " + data[0].lastName);
					// data.forEach((e: any, index: number) => {
					// 	let u: any = this.users.find(f => f.id === e.id);
					// 	if (u) {
					// 		data.splice(index, 1);
					// 	}
					// });
					// this.users = [...this.users, ...data];
				}
			});
		}
	}

	// date range
	_startValueChange = () => {
		if (this.entity.from > this.entity.until) {
			this.entity.until = null;
		}
	};

	_endValueChange = () => {
		if (this.entity.from > this.entity.until) {
			this.entity.from = null;
		}
	};

	_disabledStartDate: any = (startValue: Date) => {
		if (!startValue || !this.entity.until) {
			return false;
		}
		return (
			new Date(startValue).getTime() >= new Date(this.entity.until).getTime()
		);
	};

	_disabledEndDate: any = (endValue: Date) => {
		if (!endValue || !this.entity.from) {
			return false;
		}
		return new Date(endValue).getTime() <= new Date(this.entity.from).getTime();
	};
}
