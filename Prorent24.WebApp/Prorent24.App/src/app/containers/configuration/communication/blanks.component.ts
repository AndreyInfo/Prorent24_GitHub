import { Component, OnInit } from "@angular/core";
import { BlankModel } from "@models/configuration/communication/blank.model";
import { GridService } from "@services/grid.service";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { ConfigCommunicationBlanksEndpoints } from "@endpoints";

@Component({
  selector: "app-blanks",
  templateUrl: "./blanks.component.html"
})
export class BlanksComponent extends GridAbstract<BlankModel>
  implements OnInit {
  constructor(public gridService: GridService) {
    super(gridService, BlankModel, ConfigCommunicationBlanksEndpoints);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
