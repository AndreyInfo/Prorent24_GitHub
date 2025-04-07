import { Component, OnInit, ViewChild, Injector } from "@angular/core";
import { NgForm } from "@angular/forms";
import { CrewMemberModel } from "@models/crew/crew-member.model";
import { DirectoryService } from "@services/directory.service";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { StaffEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { Entity } from "@shared/enums/entity.enum";
import { TreeFoldersComponent } from "@components/tree-folders/tree-folders.component";
import { FormAbstract } from "@abstractions/form.abstraction";
import { Store } from "@ngrx/store";
import { RootStoreState, IdentityStoreSelectors } from "@store";
import { SocialNetworkType } from "@shared/enums/social-network-type.enum";
import { SocialNetwork } from "@shared/models/social-network.model";
import { Console } from 'console';

@Component({
  selector: "app-staff-form",
  templateUrl: "./staff-form.component.html"
})
export class StaffFormComponent extends FormAbstract<CrewMemberModel>
  implements OnInit {
  @ViewChild(TreeFoldersComponent, { static: true }) foldersModal: TreeFoldersComponent;

  isCurrentUser: boolean;
  rates: Array<any>;
  countries: Array<any>;
  roles: Array<any>;
  socialNetworks: any = SocialNetworkType;

  socialForm: SocialNetwork = new SocialNetwork();

  constructor(
    public injector: Injector,
    public store$: Store<RootStoreState.IState>,
    private directory: DirectoryService,
    private customDirectory: CustomDirectoryService
  ) {
    super(injector, Entity.crewMembers, CrewMemberModel, StaffEndpoints, true);
  }

  ngOnInit(): void {
    super.ngOnInit();

    if (this.id) {
      this.customDirectory
        .getCrewMemberRates(this.id)
        .subscribe(data => (this.rates = data));
    }

    this.directory.getCountries().subscribe(data => (this.countries = data));
    this.customDirectory.getUserRoles().subscribe(data => (this.roles = data));

    this.store$
      .select(IdentityStoreSelectors.selectIdenityUser)
      .subscribe((user: any) => {
        this.isCurrentUser = user.id === this.id ? true : false;
      });
  }

  sendLoginInformation(id: any): void {
    if (id) {
      this.http
        .post(StringExt.Format(StaffEndpoints.single, id) + "/send_login_info")
        .subscribe(_ => {
          this.notification.create("success", "Данные отправлены", {
            Position: "top-right",
            Style: "flip",
            Duration: 3000
          });
        });
    }
  }

  setFolder(folder: any): void {
    this.entity.folderId = folder.id;
    this.entity.folderName = folder.name;
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

  changeSymbol(value: any): void {
    this.entity.email = StringExt.changeSymbolFromRusToEn(value);
  }

  submitDataOverride(
    form: NgForm,
    redirectToParent?: boolean,
    goToEditAfter?: boolean,
    levelNodeBack?: number
  ): void {

      //console.log("p1birthdate - " + form.value.birthdate);
      //console.log("p1passportDate - " + form.value.passportDate);
      //console.log("p.parse - " + Date.parse(form.value.birthdate));

      if (form.value.birthdate != null) {
          form.value.birthdate = new Date(Date.parse(form.value.birthdate)).toDateString();
      }
      if (form.value.passportDate != null) {
          form.value.passportDate = new Date(Date.parse(form.value.passportDate)).toDateString();
      }
      

      form.value.socialNetworks = this.entity.socialNetworks;
      this.submitData(form, redirectToParent, goToEditAfter, levelNodeBack);

      //console.log("p2birthdate - " + form.value.birthdate);
      //console.log("p2passportDate - " + form.value.passportDate);
  }
}
