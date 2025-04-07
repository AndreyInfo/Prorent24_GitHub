import { Component, OnInit, ViewChild, Injector } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Entity } from "@shared/enums/entity.enum";
import { ContactModel } from "@models/contacts/contact.model";
import { FormAbstract } from "@abstractions/form.abstraction";
import { TreeFoldersComponent } from "@components/tree-folders/tree-folders.component";
import { DirectoryService } from "@services/directory.service";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { ContactsEndpoints } from "@endpoints";
import { SocialNetworkType } from "@shared/enums/social-network-type.enum";
import { SocialNetwork } from "@shared/models/social-network.model";
import { PhoneModel } from "@shared/models/phone.model";
import { SearchService } from "@services/search.service";

@Component({
  selector: "app-contact-form-data",
  templateUrl: "./contact-form-data.component.html"
})
export class ContactFormDataComponent extends FormAbstract<ContactModel>
  implements OnInit {
  @ViewChild(TreeFoldersComponent, { static: true }) foldersModal: TreeFoldersComponent;

  entityType = Entity.contacts;

  companyTypes: Array<any> = new Array<any>();
  countries: Array<any>;
  contactPersons: Array<any> = new Array<any>();
  socialNetworks: any = SocialNetworkType;

  socialForm: SocialNetwork = new SocialNetwork();
  phone_Form: PhoneModel = new PhoneModel();
  phone_company_Form: PhoneModel = new PhoneModel();

  users: Array<any> = new Array<any>();

  constructor(
    private injector: Injector,
    private customDirectory: CustomDirectoryService,
    private search: SearchService,
    private directory: DirectoryService
  ) {
    super(injector, Entity.contacts, ContactModel, ContactsEndpoints, true);
  }

  ngOnInit(): void {
    super.ngOnInit();

    if (this.id) {
      this.customDirectory
        .getContactPersons(this.id)
        .subscribe(data => (this.contactPersons = data));
    }

    this.directory.getCountries().subscribe(data => (this.countries = data));
    this.directory
      .getCompanyTypes()
      .subscribe(data => (this.companyTypes = data));

    this.onDataLoadComplete.subscribe((data: ContactModel) => {
        this.users = data.crewMembers;
    });
  }

  setFolder(folder: any): void {
    this.entity.folderId = folder.id;
    this.entity.folderName = folder.name;
  }

  copyAddressValues(to: any, from: any): void {
    Object.assign(to, from);
  }

  addSocialContact(form: NgForm): void {
    if (form.valid) {
      this.entity.socialNetworks.push(form.value);
      this.socialForm = new SocialNetwork();
    }
  }

  removeSocialContact(index: number): void {
    this.entity.socialNetworks.splice(index, 1);
  }

  addPhone(form: NgForm): void {
    if (form.valid) {
      let newPhone: string = form.value.phone;
      this.entity.phones.push(newPhone);
      this.phone_Form = new PhoneModel();
    }
  }

  removePhone(index: number): void {
    this.entity.phones.splice(index, 1);
  }

  addCompanyPhone(form: NgForm): void {
    if (form.valid) {
      let newPhone: string = form.value.phone;
      this.entity.companyPhones.push(newPhone);
      this.phone_company_Form = new PhoneModel();
    }
  }

  removeCompanyPhone(index: number): void {
    this.entity.companyPhones.splice(index, 1);
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
}
