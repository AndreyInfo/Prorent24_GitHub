import { Component, OnInit, Injector } from "@angular/core";
import { FormAbstract } from "@abstractions/form.abstraction";
import { SearchService } from "@services/search.service";
import { Entity } from "@shared/enums/entity.enum";
import { MaintenanceInspectionModel } from "@models/maintenance/maintenance-inspection.model";
import { MaintenanceInspectionsEndpoints } from "@endpoints";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { InspectionStatus } from "@shared/enums/inspection-status.enum";

@Component({
  selector: "app-maintenance-inspection-form",
  templateUrl: "./maintenance-inspection-form.component.html"
})
export class MaintenanceInspectionFormComponent
  extends FormAbstract<MaintenanceInspectionModel>
  implements OnInit {
  entityType = Entity.maintenance;

  status: any = InspectionStatus;
  inspectionType: Array<any> = new Array<any>();

  constructor(
    private injector: Injector,
    private search: SearchService,
    private customDirectory: CustomDirectoryService
  ) {
    super(
      injector,
      Entity.maintenance,
      MaintenanceInspectionModel,
      MaintenanceInspectionsEndpoints
    );
  }

  ngOnInit(): void {
    super.ngOnInit();

    this.customDirectory
      .getInspectionType()
      .subscribe(data => (this.inspectionType = data));

    this.onSaveComplete.subscribe(() => {
      this.router.navigate(["./maintenance/inspections"]);
    });
  }
}
