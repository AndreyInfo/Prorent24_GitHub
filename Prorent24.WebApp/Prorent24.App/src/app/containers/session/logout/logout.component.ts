import { Component } from "@angular/core";
import { Store } from "@ngrx/store";
import { RootStoreState, IdentityStoreActions } from "@store";

@Component({
  selector: "app-logout",
  templateUrl: "./logout.component.html"
})
export class LogoutComponent {
  constructor(private store$: Store<RootStoreState.IState>) {
    this.store$.dispatch(new IdentityStoreActions.Logout());
  }
}
