import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
  Router
} from "@angular/router";

import { Store } from "@ngrx/store";
import { RootStoreState, IdentityStoreSelectors } from "@store";
import { Observable } from "rxjs";

@Injectable()
export class SystemEmptyGuard implements CanActivate {
  constructor(
    private router: Router,
    private store$: Store<RootStoreState.IState>
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    const isEmptyDb$: Observable<boolean> = this.store$.select(
      IdentityStoreSelectors.selectDbIsEmpty
    );

    isEmptyDb$.subscribe((isSystemEmpty: boolean) => {
      if (isSystemEmpty && route.routeConfig.path !== "register") {
        this.router.navigateByUrl("/register");
      }

      if (!isSystemEmpty && route.routeConfig.path === "register") {
        this.router.navigateByUrl("/login");
      }
    });

    return true;
  }
}
