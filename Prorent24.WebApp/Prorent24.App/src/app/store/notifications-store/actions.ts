import { Action } from "@ngrx/store";
import { NotificationModel } from "@models/notifications/notification.model";

export enum ActionTypes {
  GET_LAST_UNREAD_NOTIFICATIONS = "[Notifications] Get last unread list",
  GET_LAST_UNREAD_NOTIFICATIONS_SUCCESS = "[Notifications] Get last unread list success",
  SET_AS_READ = "[Notifications] Get as read",
  SET_AS_READ_SUCCESS = "[Notifications] Get as read success",
  ADD_NOTIFICATION = "[Notifications] Add new notification"
}

export class GetLastUnreadNotifications implements Action {
  readonly type = ActionTypes.GET_LAST_UNREAD_NOTIFICATIONS;
}

export class GetLastUnreadNotificationsSuccess implements Action {
  readonly type = ActionTypes.GET_LAST_UNREAD_NOTIFICATIONS_SUCCESS;
  constructor(public payload: any) {}
}

export class SetAsRead implements Action {
  readonly type = ActionTypes.SET_AS_READ;
  constructor(public payload: any) {}
}

export class SetAsReadSuccess implements Action {
  readonly type = ActionTypes.SET_AS_READ_SUCCESS;
}

export class AddNotification implements Action {
  readonly type = ActionTypes.ADD_NOTIFICATION;
  constructor(public payload: NotificationModel) {}
}

export type Actions =
  | GetLastUnreadNotifications
  | GetLastUnreadNotificationsSuccess
  | SetAsRead
  | SetAsReadSuccess
  | AddNotification;
