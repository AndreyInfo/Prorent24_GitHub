import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { CommunicationModel } from "@models/configuration/communication/communication.model";
import { ConfigCommunicationEndpoints } from "@endpoints";
import { HttpService } from "@core/http.service";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-communication",
  templateUrl: "./communication.component.html",
  styles: []
})
export class CommunicationComponent implements OnInit {
  entity: CommunicationModel = new CommunicationModel();

  editorModules = {
    toolbar: [
      [{ header: [1, 2, 3, 4, false] }],
      ["bold", "italic", "underline"],
      ["link", "image"]
    ]
  };

  constructor(private toggler: PagesToggleService, private http: HttpService) {}

  ngOnInit(): void {
    this.http
      .getT<CommunicationModel>(ConfigCommunicationEndpoints.root)
      .subscribe(data => (this.entity = data));

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.http.post(ConfigCommunicationEndpoints.root, form.value).subscribe();
    }
  }
}
