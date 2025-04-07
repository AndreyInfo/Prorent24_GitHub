import { Component, OnInit, Injector } from "@angular/core";
import { FormAbstract } from "@abstractions/form.abstraction";
import { StringExt } from "@shared/utils/string.extension";
import { SearchService } from "@services/search.service";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { Entity } from "@shared/enums/entity.enum";
import { ProjectModel } from "@models/project/project.model";
import { ProjectTime } from "@models/project/project-time.model";
import { ProjectsEndpoints } from "@endpoints";
import { ContactsEndpoints } from "@endpoints";
import { ProjectStatus } from "@shared/enums/project-status.enum";
import { ContactModel } from "@models/contacts/contact.model";
import { NgForm } from "@angular/forms";

@Component({
  selector: "app-project-form-data",
  templateUrl: "./project-form-data.component.html"
})
export class ProjectFormDataComponent extends FormAbstract<ProjectModel>
  implements OnInit {
  entityType = Entity.subhire;

  projStatus: any = ProjectStatus;
  projectType: Array<any> = new Array<any>();
  contacts: Array<any> = new Array<any>();
  crewMembers: Array<any> = new Array<any>();
  users: Array<any> = new Array<any>();
  projectTime: ProjectTime = new ProjectTime();

  usePeriod: ProjectTime = new ProjectTime();
  planningPeriod: ProjectTime = new ProjectTime();

  additionalTimes: Array<ProjectTime> = new Array<ProjectTime>();
  additionalTimeForm: ProjectTime = new ProjectTime();

  constructor(
    private injector: Injector,
    private search: SearchService,
    private customDirectory: CustomDirectoryService
  ) {
    super(injector, Entity.equipment, ProjectModel, ProjectsEndpoints, true);
  }

  ngOnInit(): void {
    super.ngOnInit();
    
    this.search.contact_persons("null").subscribe((data: Array<any>) => {
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

    this.customDirectory
      .getCrewMembers()
      .subscribe(data => (this.crewMembers = data));

    this.customDirectory.getProjectTypes().subscribe(data => {
      this.projectType = data;
    });

    if (this.id) {
      this.onDataLoadComplete.subscribe((data: ProjectModel) => {
        this.users = data.crewMembers;
        this.setTimes();
      });
    } else {
      this.setTimes();
    }
  }

  onChangeContactSearch(event: any): void {
    let search_val: string = event.target.value;
    if (search_val.length > 2 && search_val.length < 6) {
      this.search.contact_persons(search_val).subscribe((data: Array<any>) => {
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

  onSelectedContact(isLocationContact: boolean): void {
    if (
      isLocationContact
        ? this.entity.locationContactPersonId !==
        this.entity.locationContactPerson.id
        : this.entity.clientContactPersonId !==
        this.entity.clientContactPerson.id
    ) {
      let personId: any;

      if (isLocationContact) {
        personId = this.contacts.find(
          (f: any) => f.id === this.entity.locationContactPersonId
        ).contactId;
      } else {
        personId = this.contacts.find(
          (f: any) => f.id === this.entity.clientContactPersonId
        ).contactId;
      }

      this.http
        .getT<ContactModel>(
          StringExt.Format(ContactsEndpoints.single, personId)
        )
        .subscribe(res => {
          if (isLocationContact) {
            this.entity.locationContactPerson = res;
          } else {
            this.entity.clientContactPerson = res;
          }
        });
    }
  }

  submitDataNew(form: NgForm): void {
    form.value.times = [];
    form.value.times.push(this.usePeriod);
    form.value.times.push(this.planningPeriod);
    form.value.times = [...form.value.times, ...this.additionalTimes];
    this.submitData(form, true, true, 1);
  }

  changeProjectType(id: number): void {
    this.entity.color = this.projectType.find(f => f.id === id).color;
  }

  submitTimeData(form: NgForm): void {
    this.entity.times.push(form.value);
  }

  //#region additional time period
  addAdditionalTime(form: NgForm): void {
    if (form.valid) {
      this.additionalTimes.push(form.value);
      this.additionalTimeForm = new ProjectTime();
    }
  }

  addAdditionalTimeRow(): void {
    let time = new ProjectTime();
    time.projectId = this.id;
    time.name = "";
    time.from = new Date();
    time.until = new Date();
    this.additionalTimes.push(time);
  }

  removeAdditionalTime(index: number): void {
    this.additionalTimes.splice(index, 1);
  }
  //#endregion

  // date range
  _startValueChange = (from: string, to: string) => {
    if (this.entity[from] > this.entity[to]) {
      this.entity[to] = null;
    }
  };

  _endValueChange = (from: string, to: string) => {
    if (this.entity[from] > this.entity[to]) {
      this.entity[from] = null;
    }
  };

  _disabledStartDate: any = (startValue: Date, to: string) => {
    if (!startValue || !this.entity[to]) {
      return false;
    }
    return (
      new Date(startValue).getTime() >= new Date(this.entity[to]).getTime()
    );
  };

  _disabledEndDate: any = (endValue: Date, from: string) => {
    if (!endValue || !this.entity[from]) {
      return false;
    }
    return (
      new Date(endValue).getTime() <= new Date(this.entity[from]).getTime()
    );
  };

  openCalendar(id,index){
    let nameId = id+index;
    let element = document.getElementById(nameId).getElementsByTagName('input')[0];
    element.click()
  }

  private setTimes(): void {
    if (this.entity.times && this.entity.times.length > 0) {
      this.additionalTimes = this.entity.times.filter(
        (f: any) => !f.defaultUsagePeriod && !f.defaultPlanPeriod
      );
      this.usePeriod = this.entity.times.find(
        x => x.defaultUsagePeriod === true
      );
      if (!this.usePeriod) {
        this.usePeriod = new ProjectTime();
        this.usePeriod.name = "Период использования";
        this.usePeriod.from = new Date();
        this.usePeriod.until = new Date();
        this.usePeriod.defaultUsagePeriod = true;
      }
      this.planningPeriod = this.entity.times.find(
        x => x.defaultPlanPeriod === true
      );
      if (!this.planningPeriod) {
        this.planningPeriod = new ProjectTime();
        this.planningPeriod.name = "Период планирования";
        this.planningPeriod.defaultPlanPeriod = true;
        this.planningPeriod.from = new Date();
        this.planningPeriod.until = new Date();
      }
    } else {
      this.entity.times = [];
      this.usePeriod.name = "Период использования";
      this.usePeriod.from = new Date();
      this.usePeriod.until = new Date();
      this.usePeriod.defaultUsagePeriod = true;

      this.planningPeriod.name = "Период планирования";
      this.planningPeriod.defaultPlanPeriod = true;
      this.planningPeriod.from = new Date();
      this.planningPeriod.until = new Date();

      this.entity.times.push(this.usePeriod);
      this.entity.times.push(this.planningPeriod);
    }
  }
}
