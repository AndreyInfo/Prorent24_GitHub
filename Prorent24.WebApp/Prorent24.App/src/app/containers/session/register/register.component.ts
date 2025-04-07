import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Store } from "@ngrx/store";
import { RootStoreState, IdentityStoreActions, IdentityStoreSelectors } from "@store";
import { RegisterModel } from "@models/session/register.model";
import { Observable } from 'rxjs';

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html"
})
export class RegisterComponent implements OnInit  {
  register: RegisterModel = new RegisterModel();
  logo$: Observable<string>;
  
  constructor(private store$: Store<RootStoreState.IState>) {}

  ngOnInit(): void {
    this.logo$ = this.store$.select(IdentityStoreSelectors.selectLogo);
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.store$.dispatch(new IdentityStoreActions.Signup(form.value));
    }
  }
}
