import { Injectable } from "@angular/core";
import { Actions, Effect, ofType } from "@ngrx/effects";
import { Action } from "@ngrx/store";
import { Observable, of, throwError } from "rxjs";
import * as idenityActions from "./actions";
import { switchMap, map, tap, catchError } from "rxjs/operators";
import { Router } from "@angular/router";

import { HttpService } from "@core/http.service";
import { AccountEndpoints } from "@endpoints";
import { CookieStorage } from "@core/cookie.storage";

@Injectable()
export class IdentityStoreEffects {
  constructor(
    private actions$: Actions,
    private httpService: HttpService,
    private cookies: CookieStorage,
    private router: Router
  ) {}

  @Effect()
  Login$: Observable<Action> = this.actions$.pipe(
    ofType<idenityActions.Login>(idenityActions.ActionTypes.LOGIN),
    map(action => action.payload),
    switchMap(payload => {
      return this.httpService.post(AccountEndpoints.login, payload).pipe(
        map(user => {
          return new idenityActions.LoginSuccess(user);
        }),
        catchError(error => {
          return of(new idenityActions.LoginFailure({ error }));
        })
      );
    })
  );

  @Effect({ dispatch: false })
  LoginSuccess$: Observable<any> = this.actions$.pipe(
    ofType(idenityActions.ActionTypes.LOGIN_SUCCESS),
    tap(() => {
      this.router.navigateByUrl("/");
    })
  );

  @Effect()
  Signup$: Observable<Action> = this.actions$.pipe(
    ofType<idenityActions.Signup>(idenityActions.ActionTypes.SIGNUP),
    map(action => action.payload),
    switchMap(payload => {
      return this.httpService.post(AccountEndpoints.register, payload).pipe(
        map(() => {
          return new idenityActions.SignupSuccess();
        }),
        catchError(error => {
          return of(new idenityActions.SignupFailure({ error }));
        })
      );
    })
  );

  @Effect({ dispatch: false })
  SignupSuccess$: Observable<any> = this.actions$.pipe(
    ofType(idenityActions.ActionTypes.SIGNUP_SUCCESS),
    tap(() => {
      this.router.navigateByUrl("/login");
    })
  );

  @Effect()
  Logout$: Observable<Action> = this.actions$.pipe(
    ofType(idenityActions.ActionTypes.LOGOUT),
    tap(() => {
      this.httpService
        .post(AccountEndpoints.logout)
        .toPromise()
        .then(() => {
          this.cookies.removeAll();
          this.router.navigateByUrl("/login");
        });
    }),
    map(() => {
      return new idenityActions.LogoutSuccess();
    })
  );

  @Effect()
  Check$: Observable<Action> = this.actions$.pipe(
    ofType<idenityActions.GetCheck>(idenityActions.ActionTypes.GET_CHECK),
    switchMap(() => {
      return this.httpService.get(AccountEndpoints.check).pipe(
        map(user => {
          return new idenityActions.CheckSuccess(user);
        })
      );
    })
  );

  @Effect({ dispatch: false })
  CheckSuccess$: Observable<any> = this.actions$.pipe(
    ofType(idenityActions.ActionTypes.CHECK_SUCCESS)
  );
}
