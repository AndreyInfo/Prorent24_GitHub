import { Component, OnInit } from "@angular/core";
import { HttpService } from "@core/http.service";
import { AccountEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { NgForm } from "@angular/forms";
import { Store } from "@ngrx/store";
import {
  RootStoreState,
  IdentityStoreActions,
  IdentityStoreSelectors
} from "@store";
import { LoginModel } from "@models/session/login.model";
import { Observable } from "rxjs";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html"
})
export class LoginComponent implements OnInit {
  email: string;
  sentEmail: boolean = false;
  notSentEmail: boolean = false;
  login: LoginModel = new LoginModel();
  error$: Observable<any>;
  logo$: Observable<string>;

  formForgotPassword: boolean = false;

  constructor(
    private store$: Store<RootStoreState.IState>,
    private http: HttpService
  ) {}

  ngOnInit(): void {
    this.error$ = this.store$.select(IdentityStoreSelectors.selectError);
    this.logo$ = this.store$.select(IdentityStoreSelectors.selectLogo);
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.store$.dispatch(new IdentityStoreActions.Login(this.login));
    }
  }

  onRemindPassword(form: NgForm): void {
    if (form.valid) {
      this.http
        .post(StringExt.Format(AccountEndpoints.forgotPassword, this.email))
        .subscribe(
          _ => {
            this.sentEmail = true;
            this.notSentEmail = false;
          },
          error => {
            this.sentEmail = false;
            this.notSentEmail = true;
          }
        );
    }
  }
}
