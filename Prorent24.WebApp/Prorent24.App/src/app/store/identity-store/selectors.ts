import {
  createFeatureSelector,
  createSelector,
  MemoizedSelector
} from "@ngrx/store";

import { IState } from "./state";
import { User } from "@models/session/user.model";

const getError: any = (state: IState): any => state.error;
const getUser: any = (state: IState): any => state.user;
const getPermissions: any = (state: IState ): any => state.permissions;
const getModules: any = (state: IState): any => state.modules;
const getIsAuthenticated: any = (state: IState): any => state.isAuthenticated;
const getIsEmptyDb: any = (state: IState): any => state.isEmpty;
const getLogo: any = (state: IState): any => state.logo;

export const selectIdentityState: MemoizedSelector<
  object,
  IState
> = createFeatureSelector<IState>("identity");

export const selectIdenityUser: MemoizedSelector<object, User> = createSelector(
  selectIdentityState,
  getUser
);

export const selectModules: MemoizedSelector<object, any> = createSelector(
  selectIdentityState,
  getModules
);

export const selectPermissions: MemoizedSelector<object, any> = createSelector(
  selectIdentityState,
  getPermissions
);

export const selectIdenityIsAuthenticated: MemoizedSelector<
  object,
  boolean
> = createSelector(
  selectIdentityState,
  getIsAuthenticated
);

export const selectLogo: MemoizedSelector<object, any> = createSelector(
  selectIdentityState,
  getLogo
);

export const selectDbIsEmpty: MemoizedSelector<
  object,
  boolean
> = createSelector(
  selectIdentityState,
  getIsEmptyDb
);

export const selectError: MemoizedSelector<object, any> = createSelector(
  selectIdentityState,
  getError
);
