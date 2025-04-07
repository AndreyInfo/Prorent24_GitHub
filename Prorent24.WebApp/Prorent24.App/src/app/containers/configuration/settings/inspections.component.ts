import { Component, OnInit } from "@angular/core";
import { InspectionModel } from "@models/configuration/settings/inspection.model";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { ConfigSettingsInspectionEndpoints } from "@endpoints";
import { TimeUnit } from "@shared/enums/time-unit.enum";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-inspections",
  templateUrl: "./inspections.component.html"
})
export class InspectionsComponent extends GridAbstract<InspectionModel>
  implements OnInit {
  timeUnit = TimeUnit;

  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService,
    private customDirectory: CustomDirectoryService
  ) {
    super(gridService, InspectionModel, ConfigSettingsInspectionEndpoints);
  }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
