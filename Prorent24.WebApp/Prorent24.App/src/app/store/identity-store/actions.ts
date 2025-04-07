import { Action } from "@ngrx/store";

export enum ActionTypes {
  LOGIN = "[Auth] Login",
  LOGIN_SUCCESS = "[Auth] Login Success",
  LOGIN_FAILURE = "[Auth] Login failure",
  SIGNUP = "[Auth] Signup",
  SIGNUP_SUCCESS = "[Auth] Signup Success",
  SIGNUP_FAILURE = "[Auth] Signup Failure",
  LOGOUT = "[Auth] Logout",
  LOGOUT_SUCCESS = "[Auth] Logout Success",
  GET_CHECK = "[Auth] Get Check",
  CHECK_SUCCESS = "[Auth] Check Success",
  UPDATE_USER_IMAGE = "Update user image",
  UPDATE_LOGO = "Update app logo"
}

export class Login implements Action {
  readonly type = ActionTypes.LOGIN;
  constructor(public payload: any) {}
}

export class LoginSuccess implements Action {
  readonly type = ActionTypes.LOGIN_SUCCESS;
  constructor(public payload: any) {}
}

export class LoginFailure implements Action {
  readonly type = ActionTypes.LOGIN_FAILURE;
  constructor(public payload: any) {}
}

export class Signup implements Action {
  readonly type = ActionTypes.SIGNUP;
  constructor(public payload: any) {}
}

export class SignupSuccess implements Action {
  readonly type = ActionTypes.SIGNUP_SUCCESS;
}

export class SignupFailure implements Action {
  readonly type = ActionTypes.SIGNUP_FAILURE;
  constructor(public payload: any) {}
}

export class Logout implements Action {
  readonly type = ActionTypes.LOGOUT;
}

export class LogoutSuccess implements Action {
  readonly type = ActionTypes.LOGOUT_SUCCESS;
}

export class GetCheck implements Action {
  readonly type = ActionTypes.GET_CHECK;
}

export class CheckSuccess implements Action {
  readonly type = ActionTypes.CHECK_SUCCESS;
  constructor(public payload: any) {}
}

export class UpdateUserImage implements Action {
  readonly type = ActionTypes.UPDATE_USER_IMAGE;
  constructor(public payload: string) {}
}

export class UpdateLogo implements Action {
  readonly type = ActionTypes.UPDATE_LOGO;
  constructor(public payload: string) {}
}

export type Actions =
  | Login
  | LoginSuccess
  | LoginFailure
  | Signup
  | SignupSuccess
  | SignupFailure
  | Logout
  | LogoutSuccess
  | GetCheck
  | CheckSuccess
  | UpdateUserImage
  | UpdateLogo;

