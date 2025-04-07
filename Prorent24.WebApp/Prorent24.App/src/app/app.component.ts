import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { Store } from "@ngrx/store";
import { RootStoreState, IdentityStoreActions } from "@store";
import { HubService } from "@services/hub.service";

@Component({
  selector: "app-root",
  template: `
    <router-outlet></router-outlet>
  `
})
export class AppComponent {
  title = "Prorent24";

  constructor(
    private translate: TranslateService,
    private store$: Store<RootStoreState.IState>,
    private hub: HubService
  ) {
    this.store$.dispatch(new IdentityStoreActions.GetCheck());

    this.translate.setDefaultLang("ru");

    this.hub.startConnection();
    this.hub.addTransferDataListener();
  }

  useLanguage(language: string): void {
    this.translate.use(language);
  }
}
