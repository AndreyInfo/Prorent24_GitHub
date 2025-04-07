import { Actions, ActionTypes } from "./actions";
import { initialState, IState } from "./state";

export function notificationsReducer(
  state: IState = initialState,
  action: Actions
): IState {
  switch (action.type) {
    case ActionTypes.GET_LAST_UNREAD_NOTIFICATIONS: {
      return {
        ...state
      };
    }
    case ActionTypes.GET_LAST_UNREAD_NOTIFICATIONS_SUCCESS: {
      return {
        ...state,
        notifications: action.payload
      };
    }
    case ActionTypes.SET_AS_READ: {
      let n: any = state.notifications.find((f: any) => f.id == action.payload);
      if (n) {
        n.isRead = true;
      }

      return {
        ...state
      };
    }
    case ActionTypes.ADD_NOTIFICATION: {
      state.notifications.unshift(action.payload);
      return {
        ...state
      };
    }
    default: {
      return state;
    }
  }
}
