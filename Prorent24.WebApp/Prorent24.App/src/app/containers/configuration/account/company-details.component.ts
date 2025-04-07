import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";

import { HttpService } from "@core/http.service";
import { DirectoryService } from "@services/directory.service";
import { ConfigAccountCompanyDetailsEndpoints } from "@endpoints";
import { CompanyDetailModel } from "@models/configuration/account/company-details.model";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { PhoneModel } from "@shared/models/phone.model";

import { Store } from "@ngrx/store";
import { RootStoreState } from "@store";
import { IdentityStoreSelectors, IdentityStoreActions } from "@store";
import { Observable } from "rxjs";

@Component({
  selector: "app-company-details",
  templateUrl: "./company-details.component.html"
})
export class CompanyDetailsComponent implements OnInit {
  entity: CompanyDetailModel = new CompanyDetailModel();
  countries: Array<any>;
  phone_Form: PhoneModel = new PhoneModel();

  logo$: Observable<string>;

  acceptFileFormat: string[] = ["image/jpeg", "image/png", "image/gif"];
  imgSizeLimit: number = 40000;

  constructor(
    private toggler: PagesToggleService,
    private http: HttpService,
    private directory: DirectoryService,
    public store$: Store<RootStoreState.IState>
  ) {}

  ngOnInit(): void {
    this.directory.getCountries().subscribe(data => (this.countries = data));

    this.http
      .getT<CompanyDetailModel>(ConfigAccountCompanyDetailsEndpoints.root)
      .subscribe(data => Object.assign(this.entity, data));

    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });

    this.logo$ = this.store$.select(IdentityStoreSelectors.selectLogo);
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.http
        .post(ConfigAccountCompanyDetailsEndpoints.root, form.value)
        .subscribe();
    }
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

  onLogoChanged(event: any) {
    let selectedFile: any = event.target.files[0];
    let _acceptFormat = this.acceptFileFormat.find(
      (f: string) => f == selectedFile.type
    );

    if (_acceptFormat && selectedFile.size <= this.imgSizeLimit) {
      const formData = new FormData();
      formData.append("file", selectedFile);
      this.http
        .post(ConfigAccountCompanyDetailsEndpoints.logo, formData)
        .subscribe(data => {
          debugger
          this.store$.dispatch(new IdentityStoreActions.UpdateLogo(data));
        });
    } else {
      alert("Недопустимый файл для загрузки. Файл не должен превышать 40 кб. Допустимые форматы изображения: jpeg, png, gif.");
    }
  }
}
