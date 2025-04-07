import { Injectable } from "@angular/core";
import { Actions, Effect, ofType } from "@ngrx/effects";
import { Action } from "@ngrx/store";
import { Observable } from "rxjs";
import * as notificationsActions from "./actions";
import { switchMap, map } from "rxjs/operators";
import { NotificationsService } from '@services/notifications.service';

@Injectable()
export class NotificationsStoreEffects {
  constructor(
    private actions$: Actions,
    private notificationsService: NotificationsService
  ) {}

  @Effect()
  GetUnredList$: Observable<Action> = this.actions$.pipe(
    ofType<notificationsActions.GetLastUnreadNotifications>(notificationsActions.ActionTypes.GET_LAST_UNREAD_NOTIFICATIONS),
    switchMap(() => {
      return this.notificationsService.getLastNotifications().pipe(
        map(data => {
          return new notificationsActions.GetLastUnreadNotificationsSuccess(data);
        })
      );
    })
  );

  @Effect()
  setAsRead$: Observable<Action> = this.actions$.pipe(
    ofType<notificationsActions.SetAsRead>(notificationsActions.ActionTypes.SET_AS_READ),
    switchMap((action) => {
      return this.notificationsService.setAsRead(action.payload).pipe(
        map(() => {
          return new notificationsActions.SetAsReadSuccess();
        })
      );
    })
  );

  @Effect({ dispatch: false })
  GetUnredListSuccess$: Observable<any> = this.actions$.pipe(
    ofType(notificationsActions.ActionTypes.GET_LAST_UNREAD_NOTIFICATIONS_SUCCESS)
  );
}
