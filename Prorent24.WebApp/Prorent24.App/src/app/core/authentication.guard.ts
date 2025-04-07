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
import { map } from "rxjs/operators";

@Injectable()
export class AuthenticatedGuard implements CanActivate {
  constructor(
    private router: Router,
    private store$: Store<RootStoreState.IState>
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    return this.store$
      .select(IdentityStoreSelectors.selectIdenityIsAuthenticated)
      .pipe(
        map(isAuthorize => {
          if (!isAuthorize) {
            this.router.navigateByUrl("/login");
          }
          return isAuthorize;
        })
      );
  }
}
