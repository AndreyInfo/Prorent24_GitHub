import { IdentityStoreState } from "@store/identity-store";
import { NotificationsStoreState } from "@store/notifications-store";

export interface IState {
  identity: IdentityStoreState.IState;
  notifications: NotificationsStoreState.IState
}
