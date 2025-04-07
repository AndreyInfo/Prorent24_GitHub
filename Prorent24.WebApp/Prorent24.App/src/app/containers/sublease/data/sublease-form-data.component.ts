import { Component, OnInit, Injector } from "@angular/core";
import { StringExt } from "@shared/utils/string.extension";
import { FormAbstract } from "@abstractions/form.abstraction";
import { ContactsEndpoints } from "@endpoints";
import { Entity } from "@shared/enums/entity.enum";
import { SubleaseModel } from "@models/sublease/sublease.model";
import { SubleaseEndpoints } from "@endpoints";
import { SubleaseStatus } from "@shared/enums/sublease-status.enum";
import { SearchService } from "@services/search.service";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { SubleaseType } from "@shared/enums/sublease-type.enum";
import { ContactModel } from "@models/contacts/contact.model";

@Component({
  selector: "app-sublease-form-data",
  templateUrl: "./sublease-form-data.component.html"
})
export class SubleaseFormDataComponent extends FormAbstract<SubleaseModel>
  implements OnInit {
  entityType = Entity.subhire;

  status: any = SubleaseStatus;
  subleaseType: any = SubleaseType;
  contacts: Array<any> = new Array<any>();
  crewMembers: Array<any> = new Array<any>();

  constructor(
    private injector: Injector,
    private search: SearchService,
    private customDirectory: CustomDirectoryService
  ) {
    super(injector, Entity.equipment, SubleaseModel, SubleaseEndpoints, true);
  }

  ngOnInit(): void {
    super.ngOnInit();

    this.customDirectory
      .getCrewMembers()
      .subscribe(data => (this.crewMembers = data));
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

  onSelectedContact(isLocationContact: boolean): void {
    if (
      isLocationContact
        ? this.entity.locationContactId !== this.entity.locationContact.id
        : this.entity.supplierContactId !== this.entity.supplierContact.id
    ) {
      this.http
        .getT<ContactModel>(
          StringExt.Format(
            ContactsEndpoints.single,
            isLocationContact
              ? this.entity.locationContactId
              : this.entity.supplierContactId
          )
        )
        .subscribe(res => {
          if (isLocationContact) {
            this.entity.locationContact = res;
          } else {
            this.entity.supplierContact = res;
          }
        });
    }
  }
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
}
