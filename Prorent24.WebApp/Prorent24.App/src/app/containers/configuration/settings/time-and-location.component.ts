import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";

import { HttpService } from "@core/http.service";
import { DirectoryService } from "@services/directory.service";
import { ConfigSettingsTimeAndLocationEndpoints } from "@endpoints";
import { TimeAndLocationModel } from "@models/configuration/settings/time-and-location.model";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-time-and-location",
  templateUrl: "./time-and-location.component.html",
  styles: []
})
export class TimeAndLocationComponent implements OnInit {
  entity: TimeAndLocationModel = new TimeAndLocationModel();
  timeZones: Array<any>;
  countries: Array<any>;
  dateNow: number = Date.now();

  constructor(
    private toggler: PagesToggleService,
    private http: HttpService,
    private directory: DirectoryService
  ) {}

  ngOnInit(): void {
    this.directory.getTimeZone().subscribe(data => (this.timeZones = data));
    this.directory.getCountries().subscribe(data => (this.countries = data));

    this.http
      .getT<TimeAndLocationModel>(ConfigSettingsTimeAndLocationEndpoints.root)
      .subscribe(data => (this.entity = data));

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.http
        .post(ConfigSettingsTimeAndLocationEndpoints.root, form.value)
        .subscribe();
    }
  }
}
