import { Component, OnInit, ViewChild } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";

import { HttpService } from "@core/http.service";
import { VehiclesEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { VehicleModel } from "@models/vehicle/vehicle.model";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { Entity } from "@shared/enums/entity.enum";
import { TreeFoldersComponent } from "@components/tree-folders/tree-folders.component";
import { SearchService } from "@services/search.service";
import { LenghtUnit } from "@shared/enums/lenght-unit.enum";
import { WeightUnit } from "@shared/enums/weight-unit.enum";

@Component({
  selector: "app-vehicle-form",
  templateUrl: "./vehicle-form.component.html"
})
export class VehicleFormComponent implements OnInit {
  @ViewChild(TreeFoldersComponent, { static: true }) foldersModal: TreeFoldersComponent;

  private id: string;
  entityType = Entity.vehicles;
  entity: VehicleModel = new VehicleModel();
  countries: Array<any>;
  users: Array<any> = new Array<any>();

  lenghtUnits: any = LenghtUnit;
  weightUnit: any = WeightUnit;

  constructor(
    private http: HttpService,
    private activateRoute: ActivatedRoute,
    private customDirectory: CustomDirectoryService,
    private search: SearchService,
    private router: Router
  ) {
    this.id = this.activateRoute.snapshot.params.id;
  }

  ngOnInit(): void {
    if (this.id) {
      this.http
        .getT<VehicleModel>(StringExt.Format(VehiclesEndpoints.single, this.id))
        .subscribe(data => {
          Object.assign(this.entity, data);
          this.setUserList();
        });
    }
  }

    onSubmit(form: NgForm): void {

        if (form.value.motDate != null) {
            form.value.motDate = new Date(Date.parse(form.value.motDate)).toDateString();
        }
        if (form.value.insuranceDate != null) {
            form.value.insuranceDate = new Date(Date.parse(form.value.insuranceDate)).toDateString();
        }

    if (form.valid) {
      if (this.id) {
        this.http
          .post(StringExt.Format(VehiclesEndpoints.single, this.id), form.value)
          .subscribe(null, null, () => {
            this.router.navigate(["/vehicles"]);
          });
      } else {
        this.http
          .post(VehiclesEndpoints.root, form.value)
          .subscribe(null, null, () => {
            this.router.navigate(["/vehicles"]);
          });
      }
    }
  }

  showFoldersModal(): void {
    this.foldersModal.showFolders();
  }

  setFolder(folder: any): void {
    this.entity.folderId = folder.id;
    this.entity.folderName = folder.name;
  }

  onChangeUserSearch(event: any): void {
    let search_val: string = event.target.value;
    if (search_val.length > 2 && search_val.length < 6) {
      this.search.users(search_val).subscribe((data: Array<any>) => {
        if (data.length > 0) {
          data.forEach((e: any, index: number) => {
            let u: any = this.users.find(f => f.id === e.id);
            if (u) {
              data.splice(index, 1);
            }
          });
          this.users = [...this.users, ...data];
        }
      });
    }
  }

  private setUserList(): void {
    this.users = [...this.entity.crewMembers, ...this.users];
  }
}
