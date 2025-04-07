import { Component, OnInit } from "@angular/core";
import { SalutationModel } from "@models/configuration/communication/salutation.model";
import { GridService } from "@services/grid.service";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { ConfigCommunicationSalutationsEndpoints } from "@endpoints";

@Component({
  selector: "app-salutation",
  templateUrl: "./salutation.component.html",
  styles: []
})
export class SalutationComponent extends GridAbstract<SalutationModel>
  implements OnInit {
  constructor(public gridService: GridService) {
    super(
      gridService,
      SalutationModel,
      ConfigCommunicationSalutationsEndpoints
    );
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
