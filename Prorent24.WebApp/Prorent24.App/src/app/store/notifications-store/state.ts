import { createEntityAdapter, EntityAdapter, EntityState } from "@ngrx/entity";
import { NotificationModel } from "@models/notifications/notification.model";

export const adapter: EntityAdapter<NotificationModel> = createEntityAdapter<
  NotificationModel
>();

export interface IState extends EntityState<NotificationModel> {
  notifications: Array<any>;
}

export const initialState: IState = adapter.getInitialState({
  notifications: []
});
