import {
    createFeatureSelector,
    createSelector,
    MemoizedSelector
  } from "@ngrx/store";
  
  import { IState } from "./state";

  const getLastUnreadNotifications: any = (state: IState ): any => state.notifications.filter((f: any) => f.isRead === false).slice(0, 5);

  export const selectNotificationsState: MemoizedSelector<
  object,
  IState
> = createFeatureSelector<IState>("notifications");

export const selectLastUnreadNotifications: MemoizedSelector<object, any> = createSelector(
    selectNotificationsState,
    getLastUnreadNotifications
  );