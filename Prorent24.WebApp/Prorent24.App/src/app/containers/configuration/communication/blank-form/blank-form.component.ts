import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";

import { BlankModel } from "@models/configuration/communication/blank.model";
import { HttpService } from "@core/http.service";
import { PageSizeType } from "@shared/enums/page-size-type.enum";
import { ConfigCommunicationBlanksEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";

@Component({
  selector: "app-blank-form",
  templateUrl: "./blank-form.component.html"
})
export class BlankFormComponent implements OnInit {
  private id: string;
  entity: BlankModel = new BlankModel();

  pageSizeType = PageSizeType;

  constructor(
    private http: HttpService,
    private activateRoute: ActivatedRoute,
    private router: Router
  ) {
    this.id = this.activateRoute.snapshot.params.id;
  }

  ngOnInit(): void {
    if (this.id) {
      this.http
        .getT<BlankModel>(
          StringExt.Format(ConfigCommunicationBlanksEndpoints.single, this.id)
        )
        .subscribe(data => (this.entity = data));
    }
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      if (this.id) {
        this.http
          .post(
            StringExt.Format(
              ConfigCommunicationBlanksEndpoints.single,
              this.id
            ),
            form.value
          )
          .subscribe(null, null, () => {
            this.router.navigate(["/configuration/communication/blanks"]);
          });
      } else {
        this.http
          .post(ConfigCommunicationBlanksEndpoints.root, form.value)
          .subscribe(null, null, () => {
            this.router.navigate(["/configuration/communication/blanks"]);
          });
      }
    }
  }
}
