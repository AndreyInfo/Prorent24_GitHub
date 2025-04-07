import { Actions, ActionTypes } from "./actions";
import { initialState, IState } from "./state";

export function identityReducer(
  state: IState = initialState,
  action: Actions
): IState {
  switch (action.type) {
    case ActionTypes.LOGIN_SUCCESS: {
      return {
        ...state,
        isAuthenticated: true,
        error: null
      };
    }
    case ActionTypes.SIGNUP_SUCCESS: {
      return {
        ...state,
        isAuthenticated: false,
        isEmpty: false,
        error: null
      };
    }
    case ActionTypes.SIGNUP_FAILURE:
    case ActionTypes.LOGIN_FAILURE: {
      return {
        ...state,
        error: action.payload.error
      };
    }
    case ActionTypes.GET_CHECK: {
      return {
        ...state,
        error: null
      };
    }
    case ActionTypes.CHECK_SUCCESS: {
      return {
        ...state,
        isAuthenticated: action.payload.isAuthenticated,
        isEmpty: action.payload.isEmpty,
        user: action.payload.user,
        permissions: action.payload.permission,
        modules: action.payload.modules,
        error: null,
        logo: action.payload.logo ? action.payload.logo : state.logo
      };
    }
    case ActionTypes.LOGOUT: {
      return initialState;
    }
    case ActionTypes.UPDATE_USER_IMAGE: {
      state.user.profileImage = action.payload;
      return {
        ...state
      };
    }
    case ActionTypes.UPDATE_LOGO: {
      state.logo = action.payload;
      return {
        ...state
      };
    }
    default: {
      return state;
    }
  }
}
