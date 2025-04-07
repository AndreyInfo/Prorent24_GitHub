import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";

import { HttpService } from "@core/http.service";
import { ConfigSettingsTimeRegistrationEndpoints } from "@endpoints";
import { TimeRegistrationModel } from "@models/configuration/settings/time-registration.model";
import { TimeUnit } from "@shared/enums/time-unit.enum";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-time-registration",
  templateUrl: "./time-registration.component.html"
})
export class TimeRegistrationComponent implements OnInit {
  entity: TimeRegistrationModel = new TimeRegistrationModel();
  timeUnit = TimeUnit;

  constructor(private toggler: PagesToggleService, private http: HttpService) {}

  ngOnInit(): void {
    this.http
      .getT<TimeRegistrationModel>(ConfigSettingsTimeRegistrationEndpoints.root)
      .subscribe(data => (this.entity = data));

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.http
        .post(ConfigSettingsTimeRegistrationEndpoints.root, form.value)
        .subscribe();
    }
  }
}
