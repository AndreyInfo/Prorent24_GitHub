import { Component, OnInit, Injector } from "@angular/core";
import { FormAbstract } from "@abstractions/form.abstraction";
import { RepairModel } from "@models/maintenance/repair.model";
import { MaintenanceRepairEndpoints } from "@endpoints";
import { Entity } from "@shared/enums/entity.enum";
import { SearchService } from "@services/search.service";
import { UsableType } from "@shared/enums/usable-type.enum";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { take } from "rxjs/operators";

@Component({
  selector: "app-maintenance-repair-form",
  templateUrl: "./maintenance-repair-form.component.html"
})
export class MaintenanceRepairFormComponent extends FormAbstract<RepairModel>
  implements OnInit {
  entityType = Entity.maintenance;

  usableType: any = UsableType;
  equipments: Array<any> = new Array<any>();
  contacts: Array<any> = new Array<any>();
  crewMembers: Array<any> = new Array<any>();

  constructor(
    private injector: Injector,
    private search: SearchService,
    private customDirectory: CustomDirectoryService
  ) {
    super(
      injector,
      Entity.maintenance,
      RepairModel,
      MaintenanceRepairEndpoints
    );
  }

  ngOnInit(): void {
    super.ngOnInit();

    this.customDirectory
      .getCrewMembers()
      .subscribe(data => (this.crewMembers = data));

    this.onDataLoadComplete.pipe(take(1)).subscribe((data: RepairModel) => {
      if (data.equipment) this.equipments.push(data.equipment);

      if (data.externalRepair) this.contacts.push(data.externalRepair);
    });
  }

  onChangeEquipmentSearch(event: any): void {
    let search_val: string = event.target.value;
    if (search_val.length > 2 && search_val.length < 6) {
      this.search.equipments(search_val).subscribe((data: Array<any>) => {
        if (data.length > 0) {
          this.equipments = data;
        }
      });
    }
  }

  onChangeContactSearch(event: any): void {
    let search_val: string = event.target.value;
    if (search_val.length > 2 && search_val.length < 6) {
      this.search.contacts(search_val).subscribe((data: Array<any>) => {
        if (data.length > 0) {
          data.forEach((e: any, index: number) => {
            let u: any = this.contacts.find(f => f.id === e.id);
            if (u) {
              data.splice(index, 1);
            }
          });
          this.contacts = [...this.contacts, ...data];
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
