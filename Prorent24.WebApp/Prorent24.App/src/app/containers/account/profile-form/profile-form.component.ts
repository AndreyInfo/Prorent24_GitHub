import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Observable } from "rxjs";

import { Store } from "@ngrx/store";
import { RootStoreState } from "@store";
import { IdentityStoreSelectors, IdentityStoreActions } from "@store";
import { User } from "@models/session/user.model";
import { CrewMemberModel } from "@models/crew/crew-member.model";

import { HttpService } from "@core/http.service";
import { DirectoryService } from "@services/directory.service";
import { StaffEndpoints, AccountEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { ChangePassword } from "@models/session/change-pass.model";

@Component({
  selector: "app-profile-form",
  templateUrl: "./profile-form.component.html"
})
export class ProfileFormComponent implements OnInit {
  user$: Observable<User>;
  userId: string;

  entity: CrewMemberModel = new CrewMemberModel();
  countries: Array<any>;

  isChangePass: boolean;
  changePass: ChangePassword = new ChangePassword();

  acceptFileFormat: string[] = ["image/jpeg", "image/png", "image/gif"];
  imgSizeLimit: number = 40000;

  constructor(
    public store$: Store<RootStoreState.IState>,
    private http: HttpService,
    private directory: DirectoryService
  ) {}

  ngOnInit(): void {
    this.user$ = this.store$.select(IdentityStoreSelectors.selectIdenityUser);

    this.user$.subscribe(u => {
      this.http
        .getT<CrewMemberModel>(StringExt.Format(StaffEndpoints.single, u.id))
        .subscribe(data => {
          this.entity = data;
          this.userId = data.id;
        });
    });

    this.directory.getCountries().subscribe(data => (this.countries = data));
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      form.value.email = form.value.username;
      this.http
        .post(StringExt.Format(StaffEndpoints.single, this.userId), form.value)
        .subscribe();
    }
  }

  changePassword(form: NgForm): void {
    if (form.valid) {
      this.http
        .post(AccountEndpoints.changePassword, form.value)
        .subscribe(null, null, () => {
          this.isChangePass = false;
        });
    }
  }

  onProfileImgChanged(event: any) {
    let selectedFile: any = event.target.files[0];

    let _acceptFormat = this.acceptFileFormat.find(
      (f: string) => f == selectedFile.type
    );

    if (_acceptFormat && selectedFile.size <= this.imgSizeLimit) {
      const formData = new FormData();
      formData.append("file", selectedFile);
      this.http.post(AccountEndpoints.profileImg, formData).subscribe(data => {
        this.store$.dispatch(new IdentityStoreActions.UpdateUserImage(data));
      });
    } else {
      alert("Недопустимый файл для загрузки. Файл не должен превышать 40 кб. Допустимые форматы изображения: jpeg, png, gif.");
    }
  }
}
